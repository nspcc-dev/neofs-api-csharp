// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: container/service.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace NeoFS.API.v2.Container {
  /// <summary>
  /// `ContainerService` provides API to interact with `Container` smart contract
  /// in NeoFS sidechain via other NeoFS nodes. All of those actions can be done
  /// equivalently by directly issuing transactions and RPC calls to sidechain
  /// nodes.
  /// </summary>
  public static partial class ContainerService
  {
    static readonly string __ServiceName = "neo.fs.v2.container.ContainerService";

    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.PutRequest> __Marshaller_neo_fs_v2_container_PutRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.PutRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.PutResponse> __Marshaller_neo_fs_v2_container_PutResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.PutResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.DeleteRequest> __Marshaller_neo_fs_v2_container_DeleteRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.DeleteRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.DeleteResponse> __Marshaller_neo_fs_v2_container_DeleteResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.DeleteResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.GetRequest> __Marshaller_neo_fs_v2_container_GetRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.GetRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.GetResponse> __Marshaller_neo_fs_v2_container_GetResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.GetResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.ListRequest> __Marshaller_neo_fs_v2_container_ListRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.ListRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.ListResponse> __Marshaller_neo_fs_v2_container_ListResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.ListResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.SetExtendedACLRequest> __Marshaller_neo_fs_v2_container_SetExtendedACLRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.SetExtendedACLRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.SetExtendedACLResponse> __Marshaller_neo_fs_v2_container_SetExtendedACLResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.SetExtendedACLResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.GetExtendedACLRequest> __Marshaller_neo_fs_v2_container_GetExtendedACLRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.GetExtendedACLRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.v2.Container.GetExtendedACLResponse> __Marshaller_neo_fs_v2_container_GetExtendedACLResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.v2.Container.GetExtendedACLResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::NeoFS.API.v2.Container.PutRequest, global::NeoFS.API.v2.Container.PutResponse> __Method_Put = new grpc::Method<global::NeoFS.API.v2.Container.PutRequest, global::NeoFS.API.v2.Container.PutResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Put",
        __Marshaller_neo_fs_v2_container_PutRequest,
        __Marshaller_neo_fs_v2_container_PutResponse);

    static readonly grpc::Method<global::NeoFS.API.v2.Container.DeleteRequest, global::NeoFS.API.v2.Container.DeleteResponse> __Method_Delete = new grpc::Method<global::NeoFS.API.v2.Container.DeleteRequest, global::NeoFS.API.v2.Container.DeleteResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Delete",
        __Marshaller_neo_fs_v2_container_DeleteRequest,
        __Marshaller_neo_fs_v2_container_DeleteResponse);

    static readonly grpc::Method<global::NeoFS.API.v2.Container.GetRequest, global::NeoFS.API.v2.Container.GetResponse> __Method_Get = new grpc::Method<global::NeoFS.API.v2.Container.GetRequest, global::NeoFS.API.v2.Container.GetResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_neo_fs_v2_container_GetRequest,
        __Marshaller_neo_fs_v2_container_GetResponse);

    static readonly grpc::Method<global::NeoFS.API.v2.Container.ListRequest, global::NeoFS.API.v2.Container.ListResponse> __Method_List = new grpc::Method<global::NeoFS.API.v2.Container.ListRequest, global::NeoFS.API.v2.Container.ListResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "List",
        __Marshaller_neo_fs_v2_container_ListRequest,
        __Marshaller_neo_fs_v2_container_ListResponse);

    static readonly grpc::Method<global::NeoFS.API.v2.Container.SetExtendedACLRequest, global::NeoFS.API.v2.Container.SetExtendedACLResponse> __Method_SetExtendedACL = new grpc::Method<global::NeoFS.API.v2.Container.SetExtendedACLRequest, global::NeoFS.API.v2.Container.SetExtendedACLResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SetExtendedACL",
        __Marshaller_neo_fs_v2_container_SetExtendedACLRequest,
        __Marshaller_neo_fs_v2_container_SetExtendedACLResponse);

    static readonly grpc::Method<global::NeoFS.API.v2.Container.GetExtendedACLRequest, global::NeoFS.API.v2.Container.GetExtendedACLResponse> __Method_GetExtendedACL = new grpc::Method<global::NeoFS.API.v2.Container.GetExtendedACLRequest, global::NeoFS.API.v2.Container.GetExtendedACLResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetExtendedACL",
        __Marshaller_neo_fs_v2_container_GetExtendedACLRequest,
        __Marshaller_neo_fs_v2_container_GetExtendedACLResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::NeoFS.API.v2.Container.ServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ContainerService</summary>
    [grpc::BindServiceMethod(typeof(ContainerService), "BindService")]
    public abstract partial class ContainerServiceBase
    {
      /// <summary>
      /// `Put` invokes `Container` smart contract's `Put` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::NeoFS.API.v2.Container.PutResponse> Put(global::NeoFS.API.v2.Container.PutRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// `Delete` invokes `Container` smart contract's `Delete` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::NeoFS.API.v2.Container.DeleteResponse> Delete(global::NeoFS.API.v2.Container.DeleteRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Returns container structure from `Container` smart contract storage.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::NeoFS.API.v2.Container.GetResponse> Get(global::NeoFS.API.v2.Container.GetRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Returns all owner's containers from 'Container` smart contract' storage.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::NeoFS.API.v2.Container.ListResponse> List(global::NeoFS.API.v2.Container.ListRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Invokes 'SetEACL' method of 'Container` smart contract and returns response
      /// immediately. After one more block in sidechain, Extended ACL changes are
      /// added into smart contract storage.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::NeoFS.API.v2.Container.SetExtendedACLResponse> SetExtendedACL(global::NeoFS.API.v2.Container.SetExtendedACLRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Returns Extended ACL table and signature from `Container` smart contract
      /// storage.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::NeoFS.API.v2.Container.GetExtendedACLResponse> GetExtendedACL(global::NeoFS.API.v2.Container.GetExtendedACLRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for ContainerService</summary>
    public partial class ContainerServiceClient : grpc::ClientBase<ContainerServiceClient>
    {
      /// <summary>Creates a new client for ContainerService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ContainerServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ContainerService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ContainerServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ContainerServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ContainerServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// `Put` invokes `Container` smart contract's `Put` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.PutResponse Put(global::NeoFS.API.v2.Container.PutRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Put(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// `Put` invokes `Container` smart contract's `Put` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.PutResponse Put(global::NeoFS.API.v2.Container.PutRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Put, null, options, request);
      }
      /// <summary>
      /// `Put` invokes `Container` smart contract's `Put` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.PutResponse> PutAsync(global::NeoFS.API.v2.Container.PutRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PutAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// `Put` invokes `Container` smart contract's `Put` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.PutResponse> PutAsync(global::NeoFS.API.v2.Container.PutRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Put, null, options, request);
      }
      /// <summary>
      /// `Delete` invokes `Container` smart contract's `Delete` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.DeleteResponse Delete(global::NeoFS.API.v2.Container.DeleteRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Delete(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// `Delete` invokes `Container` smart contract's `Delete` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.DeleteResponse Delete(global::NeoFS.API.v2.Container.DeleteRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
      }
      /// <summary>
      /// `Delete` invokes `Container` smart contract's `Delete` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.DeleteResponse> DeleteAsync(global::NeoFS.API.v2.Container.DeleteRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// `Delete` invokes `Container` smart contract's `Delete` method and returns
      /// response immediately. After a new block is issued in sidechain, request is
      /// verified by Inner Ring nodes. After one more block in sidechain, container
      /// is added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.DeleteResponse> DeleteAsync(global::NeoFS.API.v2.Container.DeleteRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
      }
      /// <summary>
      /// Returns container structure from `Container` smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.GetResponse Get(global::NeoFS.API.v2.Container.GetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Get(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Returns container structure from `Container` smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.GetResponse Get(global::NeoFS.API.v2.Container.GetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
      }
      /// <summary>
      /// Returns container structure from `Container` smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.GetResponse> GetAsync(global::NeoFS.API.v2.Container.GetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Returns container structure from `Container` smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.GetResponse> GetAsync(global::NeoFS.API.v2.Container.GetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
      }
      /// <summary>
      /// Returns all owner's containers from 'Container` smart contract' storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.ListResponse List(global::NeoFS.API.v2.Container.ListRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return List(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Returns all owner's containers from 'Container` smart contract' storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.ListResponse List(global::NeoFS.API.v2.Container.ListRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
      }
      /// <summary>
      /// Returns all owner's containers from 'Container` smart contract' storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.ListResponse> ListAsync(global::NeoFS.API.v2.Container.ListRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ListAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Returns all owner's containers from 'Container` smart contract' storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.ListResponse> ListAsync(global::NeoFS.API.v2.Container.ListRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
      }
      /// <summary>
      /// Invokes 'SetEACL' method of 'Container` smart contract and returns response
      /// immediately. After one more block in sidechain, Extended ACL changes are
      /// added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.SetExtendedACLResponse SetExtendedACL(global::NeoFS.API.v2.Container.SetExtendedACLRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SetExtendedACL(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Invokes 'SetEACL' method of 'Container` smart contract and returns response
      /// immediately. After one more block in sidechain, Extended ACL changes are
      /// added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.SetExtendedACLResponse SetExtendedACL(global::NeoFS.API.v2.Container.SetExtendedACLRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SetExtendedACL, null, options, request);
      }
      /// <summary>
      /// Invokes 'SetEACL' method of 'Container` smart contract and returns response
      /// immediately. After one more block in sidechain, Extended ACL changes are
      /// added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.SetExtendedACLResponse> SetExtendedACLAsync(global::NeoFS.API.v2.Container.SetExtendedACLRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SetExtendedACLAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Invokes 'SetEACL' method of 'Container` smart contract and returns response
      /// immediately. After one more block in sidechain, Extended ACL changes are
      /// added into smart contract storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.SetExtendedACLResponse> SetExtendedACLAsync(global::NeoFS.API.v2.Container.SetExtendedACLRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SetExtendedACL, null, options, request);
      }
      /// <summary>
      /// Returns Extended ACL table and signature from `Container` smart contract
      /// storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.GetExtendedACLResponse GetExtendedACL(global::NeoFS.API.v2.Container.GetExtendedACLRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetExtendedACL(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Returns Extended ACL table and signature from `Container` smart contract
      /// storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.v2.Container.GetExtendedACLResponse GetExtendedACL(global::NeoFS.API.v2.Container.GetExtendedACLRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetExtendedACL, null, options, request);
      }
      /// <summary>
      /// Returns Extended ACL table and signature from `Container` smart contract
      /// storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.GetExtendedACLResponse> GetExtendedACLAsync(global::NeoFS.API.v2.Container.GetExtendedACLRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetExtendedACLAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Returns Extended ACL table and signature from `Container` smart contract
      /// storage.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.v2.Container.GetExtendedACLResponse> GetExtendedACLAsync(global::NeoFS.API.v2.Container.GetExtendedACLRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetExtendedACL, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ContainerServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ContainerServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ContainerServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Put, serviceImpl.Put)
          .AddMethod(__Method_Delete, serviceImpl.Delete)
          .AddMethod(__Method_Get, serviceImpl.Get)
          .AddMethod(__Method_List, serviceImpl.List)
          .AddMethod(__Method_SetExtendedACL, serviceImpl.SetExtendedACL)
          .AddMethod(__Method_GetExtendedACL, serviceImpl.GetExtendedACL).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ContainerServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Put, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::NeoFS.API.v2.Container.PutRequest, global::NeoFS.API.v2.Container.PutResponse>(serviceImpl.Put));
      serviceBinder.AddMethod(__Method_Delete, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::NeoFS.API.v2.Container.DeleteRequest, global::NeoFS.API.v2.Container.DeleteResponse>(serviceImpl.Delete));
      serviceBinder.AddMethod(__Method_Get, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::NeoFS.API.v2.Container.GetRequest, global::NeoFS.API.v2.Container.GetResponse>(serviceImpl.Get));
      serviceBinder.AddMethod(__Method_List, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::NeoFS.API.v2.Container.ListRequest, global::NeoFS.API.v2.Container.ListResponse>(serviceImpl.List));
      serviceBinder.AddMethod(__Method_SetExtendedACL, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::NeoFS.API.v2.Container.SetExtendedACLRequest, global::NeoFS.API.v2.Container.SetExtendedACLResponse>(serviceImpl.SetExtendedACL));
      serviceBinder.AddMethod(__Method_GetExtendedACL, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::NeoFS.API.v2.Container.GetExtendedACLRequest, global::NeoFS.API.v2.Container.GetExtendedACLResponse>(serviceImpl.GetExtendedACL));
    }

  }
}
#endregion
