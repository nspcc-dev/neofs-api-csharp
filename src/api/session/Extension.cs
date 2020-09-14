using namespace NeoFS.API.v2.Session
{

}
public static class ServiceExtension
{
    public static bool IsSame(this Token tkn, Token old)
    {
        return tkn != null
            && old != null
            && tkn.FirstEpoch == old.FirstEpoch
            && tkn.LastEpoch == old.LastEpoch
            && !tkn.OwnerID.IsEmpty
            && !old.OwnerID.IsEmpty
            && tkn.OwnerID.Equals(old.OwnerID)
            && tkn.PublicKeys.Count > 0
            && tkn.ObjectID.Count > 0
            && old.ObjectID.Count > 0
            && tkn.ObjectID.Equals(old.ObjectID);
    }

    public static byte[] GetBytes(this Token tkn)
    {
        var num = 0;
        if (tkn.ObjectID.Count > 0)
        {
            num = tkn.ObjectID[0].Length;
        }


        var len = 16 * tkn.ObjectID.Count * num;
        var buf = new MemoryStream(len);

        buf.Write(BitConverter.GetBytes(tkn.FirstEpoch));
        buf.Write(BitConverter.GetBytes(tkn.LastEpoch));

        for (int i = 0; i < tkn.ObjectID.Count; i++)
        {
            buf.Write(tkn.ObjectID[i].ToByteArray());
        }

        return buf.ToArray();
    }

    public static void Sign(this Token tkn, ECDsa key)
    {
        { // sign token header
            if (tkn.Header == null)
            {
                tkn.Header = new VerificationHeader
                {
                    PublicKey = null,
                    KeySignature = null,
                };
            }

            var signature = tkn.Header.PublicKey.SignMessage(key);
            tkn.Header.KeySignature = ByteString.CopyFrom(signature);
        }

        // sign message
        tkn.Signature = ByteString.CopyFrom(tkn.GetBytes().SignMessage(key));
    }

    public static CreateRequest PrepareInit(this Token tkn, uint ttl, ECDsa key, bool debug = false)
    {
        var req = new CreateRequest { Init = tkn };

        req.SetTTL(ttl);
        req.SignHeader(key, debug);

        return req;
    }

    public static CreateRequest PrepareSigned(this Token tkn, uint ttl, ECDsa key, bool debug = false)
    {
        var req = new CreateRequest { Signed = tkn };

        req.SetTTL(ttl);
        req.SignHeader(key, debug);

        return req;
    }

    public static async Task<Token> EstablishSession(this Channel chan, Guid oid, uint ttl, ECDsa key, bool debug = false)
    {
        return await chan.EstablishSession(oid.Bytes(), ttl, key, debug);
    }

    public static async Task<Token> EstablishSession(this Channel chan, byte[] oid, uint ttl, ECDsa key, bool debug = false)
    {
        var tkn = new Session.SessionClient(chan).Create();

        // Prepare Session and Token:
        var publey = ByteString.CopyFrom(key.Peer());
        var owner = ByteString.CopyFrom(key.Address());
        var empty = new byte[0];

        var token = new Token
        {
            OwnerID = owner,
            LastEpoch = ulong.MaxValue,
            FirstEpoch = ulong.MinValue,

            // empty TokenID
            ID = ByteString.CopyFrom(new byte[16]),

            // initialize verification header
            Header = new VerificationHeader
            {
                PublicKey = ByteString.CopyFrom(empty),
                KeySignature = ByteString.CopyFrom(empty),
            },
        };

        // Set Owner ID:
        token.PublicKeys.Add(publey);

        // Set Object ID:
        token.ObjectID.Add(ByteString.CopyFrom(oid));

        // Send token to node
        await tkn.RequestStream.WriteAsync(token.PrepareInit(ttl, key, debug));

        // Wait to complete request
        await tkn.ResponseStream.MoveNext();

        // Receive session token
        var response = tkn.ResponseStream.Current;

        if (!response.Unsigned.IsSame(token))
        {
            throw new Exception("wrong token received");
        }


        // Sign received token
        token = response.Unsigned;
        token.Sign(key);

        // Send signed token
        await tkn.RequestStream.WriteAsync(token.PrepareSigned(ttl, key, debug)); ;

        // Wait to complete request
        await tkn.ResponseStream.MoveNext();
        await tkn.RequestStream.CompleteAsync();

        // Store received token:
        return tkn.ResponseStream.Current.Result;
    }
}
}