// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ClaimCodenameResponse.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Networking.Responses {

  /// <summary>Holder for reflection information generated from ClaimCodenameResponse.proto</summary>
  public static partial class ClaimCodenameResponseReflection {

    #region Descriptor
    /// <summary>File descriptor for ClaimCodenameResponse.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ClaimCodenameResponseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChtDbGFpbUNvZGVuYW1lUmVzcG9uc2UucHJvdG8SH1BPR09Qcm90b3MuTmV0",
            "d29ya2luZy5SZXNwb25zZXMaIFBPR09Qcm90b3MvRGF0YS9QbGF5ZXJEYXRh",
            "LnByb3RvIuUCChVDbGFpbUNvZGVuYW1lUmVzcG9uc2USEAoIY29kZW5hbWUY",
            "ASABKAkSFAoMdXNlcl9tZXNzYWdlGAIgASgJEhUKDWlzX2Fzc2lnbmFibGUY",
            "AyABKAgSTQoGc3RhdHVzGAQgASgOMj0uUE9HT1Byb3Rvcy5OZXR3b3JraW5n",
            "LlJlc3BvbnNlcy5DbGFpbUNvZGVuYW1lUmVzcG9uc2UuU3RhdHVzEjMKDnVw",
            "ZGF0ZWRfcGxheWVyGAUgASgLMhsuUE9HT1Byb3Rvcy5EYXRhLlBsYXllckRh",
            "dGEiiAEKBlN0YXR1cxIJCgVVTlNFVBAAEgsKB1NVQ0NFU1MQARIaChZDT0RF",
            "TkFNRV9OT1RfQVZBSUxBQkxFEAISFgoSQ09ERU5BTUVfTk9UX1ZBTElEEAMS",
            "EQoNQ1VSUkVOVF9PV05FUhAEEh8KG0NPREVOQU1FX0NIQU5HRV9OT1RfQUxM",
            "T1dFRBAFYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::POGOProtos.Data.PlayerDataReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Responses.ClaimCodenameResponse), global::POGOProtos.Networking.Responses.ClaimCodenameResponse.Parser, new[]{ "Codename", "UserMessage", "IsAssignable", "Status", "UpdatedPlayer" }, null, new[]{ typeof(global::POGOProtos.Networking.Responses.ClaimCodenameResponse.Types.Status) }, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ClaimCodenameResponse : pb::IMessage<ClaimCodenameResponse> {
    private static readonly pb::MessageParser<ClaimCodenameResponse> _parser = new pb::MessageParser<ClaimCodenameResponse>(() => new ClaimCodenameResponse());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ClaimCodenameResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::POGOProtos.Networking.Responses.ClaimCodenameResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ClaimCodenameResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ClaimCodenameResponse(ClaimCodenameResponse other) : this() {
      codename_ = other.codename_;
      userMessage_ = other.userMessage_;
      isAssignable_ = other.isAssignable_;
      status_ = other.status_;
      UpdatedPlayer = other.updatedPlayer_ != null ? other.UpdatedPlayer.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ClaimCodenameResponse Clone() {
      return new ClaimCodenameResponse(this);
    }

    /// <summary>Field number for the "codename" field.</summary>
    public const int CodenameFieldNumber = 1;
    private string codename_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Codename {
      get { return codename_; }
      set {
        codename_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "user_message" field.</summary>
    public const int UserMessageFieldNumber = 2;
    private string userMessage_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string UserMessage {
      get { return userMessage_; }
      set {
        userMessage_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "is_assignable" field.</summary>
    public const int IsAssignableFieldNumber = 3;
    private bool isAssignable_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsAssignable {
      get { return isAssignable_; }
      set {
        isAssignable_ = value;
      }
    }

    /// <summary>Field number for the "status" field.</summary>
    public const int StatusFieldNumber = 4;
    private global::POGOProtos.Networking.Responses.ClaimCodenameResponse.Types.Status status_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::POGOProtos.Networking.Responses.ClaimCodenameResponse.Types.Status Status {
      get { return status_; }
      set {
        status_ = value;
      }
    }

    /// <summary>Field number for the "updated_player" field.</summary>
    public const int UpdatedPlayerFieldNumber = 5;
    private global::POGOProtos.Data.PlayerData updatedPlayer_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::POGOProtos.Data.PlayerData UpdatedPlayer {
      get { return updatedPlayer_; }
      set {
        updatedPlayer_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ClaimCodenameResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ClaimCodenameResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Codename != other.Codename) return false;
      if (UserMessage != other.UserMessage) return false;
      if (IsAssignable != other.IsAssignable) return false;
      if (Status != other.Status) return false;
      if (!object.Equals(UpdatedPlayer, other.UpdatedPlayer)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Codename.Length != 0) hash ^= Codename.GetHashCode();
      if (UserMessage.Length != 0) hash ^= UserMessage.GetHashCode();
      if (IsAssignable != false) hash ^= IsAssignable.GetHashCode();
      if (Status != 0) hash ^= Status.GetHashCode();
      if (updatedPlayer_ != null) hash ^= UpdatedPlayer.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Codename.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Codename);
      }
      if (UserMessage.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(UserMessage);
      }
      if (IsAssignable != false) {
        output.WriteRawTag(24);
        output.WriteBool(IsAssignable);
      }
      if (Status != 0) {
        output.WriteRawTag(32);
        output.WriteEnum((int) Status);
      }
      if (updatedPlayer_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(UpdatedPlayer);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Codename.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Codename);
      }
      if (UserMessage.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserMessage);
      }
      if (IsAssignable != false) {
        size += 1 + 1;
      }
      if (Status != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Status);
      }
      if (updatedPlayer_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(UpdatedPlayer);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ClaimCodenameResponse other) {
      if (other == null) {
        return;
      }
      if (other.Codename.Length != 0) {
        Codename = other.Codename;
      }
      if (other.UserMessage.Length != 0) {
        UserMessage = other.UserMessage;
      }
      if (other.IsAssignable != false) {
        IsAssignable = other.IsAssignable;
      }
      if (other.Status != 0) {
        Status = other.Status;
      }
      if (other.updatedPlayer_ != null) {
        if (updatedPlayer_ == null) {
          updatedPlayer_ = new global::POGOProtos.Data.PlayerData();
        }
        UpdatedPlayer.MergeFrom(other.UpdatedPlayer);
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
          case 10: {
            Codename = input.ReadString();
            break;
          }
          case 18: {
            UserMessage = input.ReadString();
            break;
          }
          case 24: {
            IsAssignable = input.ReadBool();
            break;
          }
          case 32: {
            status_ = (global::POGOProtos.Networking.Responses.ClaimCodenameResponse.Types.Status) input.ReadEnum();
            break;
          }
          case 42: {
            if (updatedPlayer_ == null) {
              updatedPlayer_ = new global::POGOProtos.Data.PlayerData();
            }
            input.ReadMessage(updatedPlayer_);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the ClaimCodenameResponse message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum Status {
        [pbr::OriginalName("UNSET")] Unset = 0,
        [pbr::OriginalName("SUCCESS")] Success = 1,
        [pbr::OriginalName("CODENAME_NOT_AVAILABLE")] CodenameNotAvailable = 2,
        [pbr::OriginalName("CODENAME_NOT_VALID")] CodenameNotValid = 3,
        [pbr::OriginalName("CURRENT_OWNER")] CurrentOwner = 4,
        [pbr::OriginalName("CODENAME_CHANGE_NOT_ALLOWED")] CodenameChangeNotAllowed = 5,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
