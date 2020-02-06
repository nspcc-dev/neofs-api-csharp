// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: object/service.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace NeoFS.API.Object {
  /// <summary>
  /// Object service provides API for manipulating with the object.
  /// </summary>
  public static partial class Service
  {
    static readonly string __ServiceName = "object.Service";

    static readonly grpc::Marshaller<global::NeoFS.API.Object.GetRequest> __Marshaller_object_GetRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.GetRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.GetResponse> __Marshaller_object_GetResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.GetResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.PutRequest> __Marshaller_object_PutRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.PutRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.PutResponse> __Marshaller_object_PutResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.PutResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.DeleteRequest> __Marshaller_object_DeleteRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.DeleteRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.DeleteResponse> __Marshaller_object_DeleteResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.DeleteResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.HeadRequest> __Marshaller_object_HeadRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.HeadRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.HeadResponse> __Marshaller_object_HeadResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.HeadResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.SearchRequest> __Marshaller_object_SearchRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.SearchRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.SearchResponse> __Marshaller_object_SearchResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.SearchResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.GetRangeRequest> __Marshaller_object_GetRangeRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.GetRangeRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.GetRangeResponse> __Marshaller_object_GetRangeResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.GetRangeResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.GetRangeHashRequest> __Marshaller_object_GetRangeHashRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.GetRangeHashRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Object.GetRangeHashResponse> __Marshaller_object_GetRangeHashResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Object.GetRangeHashResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::NeoFS.API.Object.GetRequest, global::NeoFS.API.Object.GetResponse> __Method_Get = new grpc::Method<global::NeoFS.API.Object.GetRequest, global::NeoFS.API.Object.GetResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "Get",
        __Marshaller_object_GetRequest,
        __Marshaller_object_GetResponse);

    static readonly grpc::Method<global::NeoFS.API.Object.PutRequest, global::NeoFS.API.Object.PutResponse> __Method_Put = new grpc::Method<global::NeoFS.API.Object.PutRequest, global::NeoFS.API.Object.PutResponse>(
        grpc::MethodType.ClientStreaming,
        __ServiceName,
        "Put",
        __Marshaller_object_PutRequest,
        __Marshaller_object_PutResponse);

    static readonly grpc::Method<global::NeoFS.API.Object.DeleteRequest, global::NeoFS.API.Object.DeleteResponse> __Method_Delete = new grpc::Method<global::NeoFS.API.Object.DeleteRequest, global::NeoFS.API.Object.DeleteResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Delete",
        __Marshaller_object_DeleteRequest,
        __Marshaller_object_DeleteResponse);

    static readonly grpc::Method<global::NeoFS.API.Object.HeadRequest, global::NeoFS.API.Object.HeadResponse> __Method_Head = new grpc::Method<global::NeoFS.API.Object.HeadRequest, global::NeoFS.API.Object.HeadResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Head",
        __Marshaller_object_HeadRequest,
        __Marshaller_object_HeadResponse);

    static readonly grpc::Method<global::NeoFS.API.Object.SearchRequest, global::NeoFS.API.Object.SearchResponse> __Method_Search = new grpc::Method<global::NeoFS.API.Object.SearchRequest, global::NeoFS.API.Object.SearchResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "Search",
        __Marshaller_object_SearchRequest,
        __Marshaller_object_SearchResponse);

    static readonly grpc::Method<global::NeoFS.API.Object.GetRangeRequest, global::NeoFS.API.Object.GetRangeResponse> __Method_GetRange = new grpc::Method<global::NeoFS.API.Object.GetRangeRequest, global::NeoFS.API.Object.GetRangeResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "GetRange",
        __Marshaller_object_GetRangeRequest,
        __Marshaller_object_GetRangeResponse);

    static readonly grpc::Method<global::NeoFS.API.Object.GetRangeHashRequest, global::NeoFS.API.Object.GetRangeHashResponse> __Method_GetRangeHash = new grpc::Method<global::NeoFS.API.Object.GetRangeHashRequest, global::NeoFS.API.Object.GetRangeHashResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetRangeHash",
        __Marshaller_object_GetRangeHashRequest,
        __Marshaller_object_GetRangeHashResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::NeoFS.API.Object.ServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for Service</summary>
    public partial class ServiceClient : grpc::ClientBase<ServiceClient>
    {
      /// <summary>Creates a new client for Service</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Service that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Get the object from container. Response uses gRPC stream. First response
      /// message carry object of requested address. Chunk messages are parts of
      /// the object's payload if it is needed. All messages except first carry
      /// chunks. Requested object can be restored by concatenation of object
      /// message payload and all chunks keeping receiving order.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::NeoFS.API.Object.GetResponse> Get(global::NeoFS.API.Object.GetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Get(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Get the object from container. Response uses gRPC stream. First response
      /// message carry object of requested address. Chunk messages are parts of
      /// the object's payload if it is needed. All messages except first carry
      /// chunks. Requested object can be restored by concatenation of object
      /// message payload and all chunks keeping receiving order.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::NeoFS.API.Object.GetResponse> Get(global::NeoFS.API.Object.GetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_Get, null, options, request);
      }
      /// <summary>
      /// Put the object into container. Request uses gRPC stream. First message
      /// SHOULD BE type of PutHeader. Container id and Owner id of object SHOULD
      /// BE set. Session token SHOULD BE obtained before put operation (see
      /// session package). Chunk messages considered by server as part of object
      /// payload. All messages except first SHOULD BE chunks. Chunk messages
      /// SHOULD BE sent in direct order of fragmentation.
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncClientStreamingCall<global::NeoFS.API.Object.PutRequest, global::NeoFS.API.Object.PutResponse> Put(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Put(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Put the object into container. Request uses gRPC stream. First message
      /// SHOULD BE type of PutHeader. Container id and Owner id of object SHOULD
      /// BE set. Session token SHOULD BE obtained before put operation (see
      /// session package). Chunk messages considered by server as part of object
      /// payload. All messages except first SHOULD BE chunks. Chunk messages
      /// SHOULD BE sent in direct order of fragmentation.
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncClientStreamingCall<global::NeoFS.API.Object.PutRequest, global::NeoFS.API.Object.PutResponse> Put(grpc::CallOptions options)
      {
        return CallInvoker.AsyncClientStreamingCall(__Method_Put, null, options);
      }
      /// <summary>
      /// Delete the object from a container
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.Object.DeleteResponse Delete(global::NeoFS.API.Object.DeleteRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Delete(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Delete the object from a container
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.Object.DeleteResponse Delete(global::NeoFS.API.Object.DeleteRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
      }
      /// <summary>
      /// Delete the object from a container
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.Object.DeleteResponse> DeleteAsync(global::NeoFS.API.Object.DeleteRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Delete the object from a container
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.Object.DeleteResponse> DeleteAsync(global::NeoFS.API.Object.DeleteRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
      }
      /// <summary>
      /// Head returns the object without data payload. Object in the
      /// response has system header only. If full headers flag is set, extended
      /// headers are also present.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.Object.HeadResponse Head(global::NeoFS.API.Object.HeadRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Head(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Head returns the object without data payload. Object in the
      /// response has system header only. If full headers flag is set, extended
      /// headers are also present.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.Object.HeadResponse Head(global::NeoFS.API.Object.HeadRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Head, null, options, request);
      }
      /// <summary>
      /// Head returns the object without data payload. Object in the
      /// response has system header only. If full headers flag is set, extended
      /// headers are also present.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.Object.HeadResponse> HeadAsync(global::NeoFS.API.Object.HeadRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return HeadAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Head returns the object without data payload. Object in the
      /// response has system header only. If full headers flag is set, extended
      /// headers are also present.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.Object.HeadResponse> HeadAsync(global::NeoFS.API.Object.HeadRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Head, null, options, request);
      }
      /// <summary>
      /// Search objects in container. Version of query language format SHOULD BE
      /// set to 1. Search query represented in serialized format (see query
      /// package).
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::NeoFS.API.Object.SearchResponse> Search(global::NeoFS.API.Object.SearchRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Search(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Search objects in container. Version of query language format SHOULD BE
      /// set to 1. Search query represented in serialized format (see query
      /// package).
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::NeoFS.API.Object.SearchResponse> Search(global::NeoFS.API.Object.SearchRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_Search, null, options, request);
      }
      /// <summary>
      /// GetRange of data payload. Range is a pair (offset, length).
      /// Requested range can be restored by concatenation of all chunks
      /// keeping receiving order.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::NeoFS.API.Object.GetRangeResponse> GetRange(global::NeoFS.API.Object.GetRangeRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetRange(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// GetRange of data payload. Range is a pair (offset, length).
      /// Requested range can be restored by concatenation of all chunks
      /// keeping receiving order.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::NeoFS.API.Object.GetRangeResponse> GetRange(global::NeoFS.API.Object.GetRangeRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_GetRange, null, options, request);
      }
      /// <summary>
      /// GetRangeHash returns homomorphic hash of object payload range after XOR
      /// operation. Ranges are set of pairs (offset, length). Hashes order in
      /// response corresponds to ranges order in request. Homomorphic hash is
      /// calculated for XORed data.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.Object.GetRangeHashResponse GetRangeHash(global::NeoFS.API.Object.GetRangeHashRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetRangeHash(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// GetRangeHash returns homomorphic hash of object payload range after XOR
      /// operation. Ranges are set of pairs (offset, length). Hashes order in
      /// response corresponds to ranges order in request. Homomorphic hash is
      /// calculated for XORed data.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.Object.GetRangeHashResponse GetRangeHash(global::NeoFS.API.Object.GetRangeHashRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetRangeHash, null, options, request);
      }
      /// <summary>
      /// GetRangeHash returns homomorphic hash of object payload range after XOR
      /// operation. Ranges are set of pairs (offset, length). Hashes order in
      /// response corresponds to ranges order in request. Homomorphic hash is
      /// calculated for XORed data.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.Object.GetRangeHashResponse> GetRangeHashAsync(global::NeoFS.API.Object.GetRangeHashRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetRangeHashAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// GetRangeHash returns homomorphic hash of object payload range after XOR
      /// operation. Ranges are set of pairs (offset, length). Hashes order in
      /// response corresponds to ranges order in request. Homomorphic hash is
      /// calculated for XORed data.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.Object.GetRangeHashResponse> GetRangeHashAsync(global::NeoFS.API.Object.GetRangeHashRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetRangeHash, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ServiceClient(configuration);
      }
    }

  }
}
#endregion