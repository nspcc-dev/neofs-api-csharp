using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.Service;
using NeoFS.Crypto;

namespace NeoFS.API.v2.Session
{
    public sealed partial class CreateRequest : IMeta, IVerify { }

    
