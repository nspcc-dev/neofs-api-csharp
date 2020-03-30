using System;
using NeoFS.API.Service;

namespace NeoFS.API.State
{
    public sealed partial class DumpRequest : IMeta, IVerify { }
    public sealed partial class DumpVarsRequest : IMeta, IVerify { }
    public sealed partial class NetmapRequest : IMeta, IVerify { }
    public sealed partial class HealthRequest: IMeta, IVerify { }
}
