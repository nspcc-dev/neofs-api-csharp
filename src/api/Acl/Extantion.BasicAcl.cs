
namespace NeoFS.API.v2.Acl
{
    public enum BasicAcl : uint
    {
        PublicBasicRule = 0x1FFFFFFF,
        PrivateBasicRule = 0x18888888,
        ReadOnlyBasicRule = 0x1FFF88FF,
    }

}