// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: accounting/service.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace NeoFS.API.Accounting {
  /// <summary>
  /// Accounting is a service that provides access for accounting balance
  /// information
  /// </summary>
  public static partial class Accounting
  {
    static readonly string __ServiceName = "accounting.Accounting";

    static readonly grpc::Marshaller<global::NeoFS.API.Accounting.BalanceRequest> __Marshaller_accounting_BalanceRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Accounting.BalanceRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NeoFS.API.Accounting.BalanceResponse> __Marshaller_accounting_BalanceResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NeoFS.API.Accounting.BalanceResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::NeoFS.API.Accounting.BalanceRequest, global::NeoFS.API.Accounting.BalanceResponse> __Method_Balance = new grpc::Method<global::NeoFS.API.Accounting.BalanceRequest, global::NeoFS.API.Accounting.BalanceResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Balance",
        __Marshaller_accounting_BalanceRequest,
        __Marshaller_accounting_BalanceResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::NeoFS.API.Accounting.ServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for Accounting</summary>
    public partial class AccountingClient : grpc::ClientBase<AccountingClient>
    {
      /// <summary>Creates a new client for Accounting</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public AccountingClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Accounting that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public AccountingClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected AccountingClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected AccountingClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Balance returns current balance status of the NeoFS user
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.Accounting.BalanceResponse Balance(global::NeoFS.API.Accounting.BalanceRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Balance(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Balance returns current balance status of the NeoFS user
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::NeoFS.API.Accounting.BalanceResponse Balance(global::NeoFS.API.Accounting.BalanceRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Balance, null, options, request);
      }
      /// <summary>
      /// Balance returns current balance status of the NeoFS user
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.Accounting.BalanceResponse> BalanceAsync(global::NeoFS.API.Accounting.BalanceRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return BalanceAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Balance returns current balance status of the NeoFS user
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::NeoFS.API.Accounting.BalanceResponse> BalanceAsync(global::NeoFS.API.Accounting.BalanceRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Balance, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override AccountingClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new AccountingClient(configuration);
      }
    }

  }
}
#endregion
