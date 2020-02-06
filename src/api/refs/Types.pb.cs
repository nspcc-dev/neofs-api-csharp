// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: refs/types.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace NeoFS.API.Refs {

  /// <summary>Holder for reflection information generated from refs/types.proto</summary>
  public static partial class TypesReflection {

    #region Descriptor
    /// <summary>File descriptor for refs/types.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TypesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChByZWZzL3R5cGVzLnByb3RvEgRyZWZzGi1naXRodWIuY29tL2dvZ28vcHJv",
            "dG9idWYvZ29nb3Byb3RvL2dvZ28ucHJvdG8iRwoHQWRkcmVzcxIiCghPYmpl",
            "Y3RJRBgBIAEoDEIQ2t4fCE9iamVjdElEyN4fABIYCgNDSUQYAiABKAxCC9re",
            "HwNDSUTI3h8AQkJaI2dpdGh1Yi5jb20vbnNwY2MtZGV2L25lb2ZzLWFwaS9y",
            "ZWZzqgIOTmVvRlMuQVBJLlJlZnPY4h4BgOIeANjhHgBiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Gogoproto.GogoReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::NeoFS.API.Refs.Address), global::NeoFS.API.Refs.Address.Parser, new[]{ "ObjectID", "CID" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Address of object (container id + object id)
  /// </summary>
  public sealed partial class Address : pb::IMessage<Address> {
    private static readonly pb::MessageParser<Address> _parser = new pb::MessageParser<Address>(() => new Address());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Address> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NeoFS.API.Refs.TypesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Address() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Address(Address other) : this() {
      objectID_ = other.objectID_;
      cID_ = other.cID_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Address Clone() {
      return new Address(this);
    }

    /// <summary>Field number for the "ObjectID" field.</summary>
    public const int ObjectIDFieldNumber = 1;
    private pb::ByteString objectID_ = pb::ByteString.Empty;
    /// <summary>
    /// ObjectID is an object identifier, valid UUIDv4 represented in bytes
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString ObjectID {
      get { return objectID_; }
      set {
        objectID_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "CID" field.</summary>
    public const int CIDFieldNumber = 2;
    private pb::ByteString cID_ = pb::ByteString.Empty;
    /// <summary>
    /// CID is container identifier
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString CID {
      get { return cID_; }
      set {
        cID_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Address);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Address other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ObjectID != other.ObjectID) return false;
      if (CID != other.CID) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ObjectID.Length != 0) hash ^= ObjectID.GetHashCode();
      if (CID.Length != 0) hash ^= CID.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (ObjectID.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(ObjectID);
      }
      if (CID.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(CID);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ObjectID.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(ObjectID);
      }
      if (CID.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(CID);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Address other) {
      if (other == null) {
        return;
      }
      if (other.ObjectID.Length != 0) {
        ObjectID = other.ObjectID;
      }
      if (other.CID.Length != 0) {
        CID = other.CID;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            ObjectID = input.ReadBytes();
            break;
          }
          case 18: {
            CID = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code