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

        public ObjectID CalculateAndGetID
        {
            get
            {
                if (objectId_ is null)
                {
                    objectId_ = new ObjectID
                    {
                        Value = Header.Sha256()
                    };
                }
                return objectId_;
            }
        }

        private ObjectID CalculateID()
        {
            return new ObjectID
            {
                Value = Header.Sha256()
            };
        }

        public bool VerifyID()
        {
            return CalculateID() == ObjectId;
        }

        public Checksum CalculatePayloadChecksum()
        {
            if (Payload is null || Payload.Length == 0)
                throw new System.InvalidOperationException("cant payload checksum: invalid payload");
            return Payload.Sha256Checksum();
        }

        public bool VerifyPayloadChecksum()
        {
            return CalculatePayloadChecksum().Equals(Header?.PayloadHash);
        }

        public Signature CalculateIDSignature(ECDsa key)
        {
            return ObjectId.SignMessagePart(key);
        }

        public bool VerifyIDSignature()
        {
            return ObjectId.VerifyMessagePart(Signature);
        }

        public void SetVerificationFields(ECDsa key)
        {
            Header.PayloadHash = CalculatePayloadChecksum();
            ObjectId = CalculateID();
            Signature = CalculateIDSignature(key);
        }

        public bool CheckVerificationFields()
        {
            if (!VerifyIDSignature()) return false;
            if (!VerifyID()) return false;
            if (!VerifyPayloadChecksum()) return false;
            return true;
        }

        public Object Parent()
        {
            var splitHeader = Header?.Split;
            if (splitHeader is null) return null;
            var parentSig = splitHeader.ParentSignature;
            var parentHeader = splitHeader.ParentHeader;
            if (parentSig is null || parentHeader is null)
                return null;
            Object obj = new Object
            {
                Header = parentHeader,
                Signature = parentSig,
            };
            return obj;
        }
    }
}
