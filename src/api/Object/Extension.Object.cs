using Google.Protobuf;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Cryptography;
using System.Security.Cryptography;
using System.Linq;

namespace NeoFS.API.v2.Object
{
    public partial class Object
    {
        public const int ChunkSize = 3 * (1 << 20);

        public static ObjectID CalculateID(Object obj)
        {
            if (obj is null || obj.Header is null)
                throw new System.InvalidOperationException("cant calculate object id: invalid object");
            return new ObjectID
            {
                Value = obj.Header.Sha256()
            };
        }

        public bool VerifyID()
        {
            return CalculateID(this) == ObjectId;
        }

        public void CalCulateAndSetID()
        {
            ObjectId = CalculateID(this);
        }

        public static Checksum CalculatePayloadChecksum(ByteString payload)
        {
            if (payload is null || payload.Length == 0)
                throw new System.InvalidOperationException("cant payload checksum: invalid payload");
            return payload.Sha256Checksum();
        }

        public bool VerifyPayloadChecksum()
        {
            return CalculatePayloadChecksum(Payload).Equals(Header?.PayloadHash);
        }

        public void CalculateAndSetPayloadCheckSum()
        {
            if (Header is null)
                throw new System.InvalidOperationException("can't set payload checksum without header");
            Header.PayloadHash = CalculatePayloadChecksum(Payload);
        }

        public static Signature CalculateIDSignature(ECDsa key, ObjectID oid)
        {
            return oid.SignMessagePart(key);
        }

        public bool VerifyIDSignature()
        {
            return ObjectId.VerifyMessagePart(Signature);
        }

        public void CalculateAndSetIDSignature(ECDsa key)
        {
            if (ObjectId is null)
                throw new System.InvalidOperationException("can't calculate id signature without id");
            Signature = CalculateIDSignature(key, ObjectId);
        }

        public void SetVerificationFields(ECDsa key)
        {
            CalculateAndSetPayloadCheckSum();
            CalCulateAndSetID();
            CalculateAndSetIDSignature(key);
        }

        public bool CheckVerificationFields()
        {
            if (!VerifyIDSignature()) return false;
            if (!VerifyID()) return false;
            if (!VerifyPayloadChecksum()) return false;
            return true;
        }
    }
}
