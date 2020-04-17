// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: acl/types.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace NeoFS.API.Acl {

  /// <summary>Holder for reflection information generated from acl/types.proto</summary>
  public static partial class TypesReflection {

    #region Descriptor
    /// <summary>File descriptor for acl/types.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TypesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9hY2wvdHlwZXMucHJvdG8SA2FjbBotZ2l0aHViLmNvbS9nb2dvL3Byb3Rv",
            "YnVmL2dvZ29wcm90by9nb2dvLnByb3RvKkMKBlRhcmdldBILCgdVbmtub3du",
            "EAASCAoEVXNlchABEgoKBlN5c3RlbRACEgoKBk90aGVycxADEgoKBlB1Yktl",
            "eRAEQjtaJWdpdGh1Yi5jb20vbnNwY2MtZGV2L25lb2ZzLWFwaS1nby9hY2yq",
            "Ag1OZW9GUy5BUEkuQWNs2OIeAWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Gogoproto.GogoReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::NeoFS.API.Acl.Target), }, null, null));
    }
    #endregion

  }
  #region Enums
  /// <summary>
  /// Target of the access control rule in access control list.
  /// </summary>
  public enum Target {
    /// <summary>
    /// Unknown target, default value.
    /// </summary>
    [pbr::OriginalName("Unknown")] Unknown = 0,
    /// <summary>
    /// User target rule is applied if sender is the owner of the container.
    /// </summary>
    [pbr::OriginalName("User")] User = 1,
    /// <summary>
    /// System target rule is applied if sender is the storage node within the
    /// container or inner ring node.
    /// </summary>
    [pbr::OriginalName("System")] System = 2,
    /// <summary>
    /// Others target rule is applied if sender is not user or system target.
    /// </summary>
    [pbr::OriginalName("Others")] Others = 3,
    /// <summary>
    /// PubKey target rule is applied if sender has public key provided in
    /// extended ACL.
    /// </summary>
    [pbr::OriginalName("PubKey")] PubKey = 4,
  }

  #endregion

}

#endregion Designer generated code