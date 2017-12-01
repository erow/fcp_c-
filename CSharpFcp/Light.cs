// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: light.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from light.proto</summary>
public static partial class LightReflection {

  #region Descriptor
  /// <summary>File descriptor for light.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static LightReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CgtsaWdodC5wcm90byItCgxMaWdodE1lc3NhZ2USDgoGc3RhdHVzGAEgASgF",
          "Eg0KBWRlbGF5GAIgASgFYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::LightMessage), global::LightMessage.Parser, new[]{ "Status", "Delay" }, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class LightMessage : pb::IMessage<LightMessage> {
  private static readonly pb::MessageParser<LightMessage> _parser = new pb::MessageParser<LightMessage>(() => new LightMessage());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<LightMessage> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::LightReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public LightMessage() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public LightMessage(LightMessage other) : this() {
    status_ = other.status_;
    delay_ = other.delay_;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public LightMessage Clone() {
    return new LightMessage(this);
  }

  /// <summary>Field number for the "status" field.</summary>
  public const int StatusFieldNumber = 1;
  private int status_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Status {
    get { return status_; }
    set {
      status_ = value;
    }
  }

  /// <summary>Field number for the "delay" field.</summary>
  public const int DelayFieldNumber = 2;
  private int delay_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Delay {
    get { return delay_; }
    set {
      delay_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as LightMessage);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(LightMessage other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Status != other.Status) return false;
    if (Delay != other.Delay) return false;
    return true;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Status != 0) hash ^= Status.GetHashCode();
    if (Delay != 0) hash ^= Delay.GetHashCode();
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Status != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Status);
    }
    if (Delay != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(Delay);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Status != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Status);
    }
    if (Delay != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Delay);
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(LightMessage other) {
    if (other == null) {
      return;
    }
    if (other.Status != 0) {
      Status = other.Status;
    }
    if (other.Delay != 0) {
      Delay = other.Delay;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
        case 8: {
          Status = input.ReadInt32();
          break;
        }
        case 16: {
          Delay = input.ReadInt32();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code