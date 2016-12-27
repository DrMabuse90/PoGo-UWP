// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: UseItemXpBoostResponse.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Networking.Responses {

  /// <summary>Holder for reflection information generated from UseItemXpBoostResponse.proto</summary>
  public static partial class UseItemXpBoostResponseReflection {

    #region Descriptor
    /// <summary>File descriptor for UseItemXpBoostResponse.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static UseItemXpBoostResponseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChxVc2VJdGVtWHBCb29zdFJlc3BvbnNlLnByb3RvEh9QT0dPUHJvdG9zLk5l",
            "dHdvcmtpbmcuUmVzcG9uc2VzGidQT0dPUHJvdG9zL0ludmVudG9yeS9BcHBs",
            "aWVkSXRlbXMucHJvdG8ivgIKFlVzZUl0ZW1YcEJvb3N0UmVzcG9uc2USTgoG",
            "cmVzdWx0GAEgASgOMj4uUE9HT1Byb3Rvcy5OZXR3b3JraW5nLlJlc3BvbnNl",
            "cy5Vc2VJdGVtWHBCb29zdFJlc3BvbnNlLlJlc3VsdBI5Cg1hcHBsaWVkX2l0",
            "ZW1zGAIgASgLMiIuUE9HT1Byb3Rvcy5JbnZlbnRvcnkuQXBwbGllZEl0ZW1z",
            "IpgBCgZSZXN1bHQSCQoFVU5TRVQQABILCgdTVUNDRVNTEAESGwoXRVJST1Jf",
            "SU5WQUxJRF9JVEVNX1RZUEUQAhIhCh1FUlJPUl9YUF9CT09TVF9BTFJFQURZ",
            "X0FDVElWRRADEhwKGEVSUk9SX05PX0lURU1TX1JFTUFJTklORxAEEhgKFEVS",
            "Uk9SX0xPQ0FUSU9OX1VOU0VUEAViBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::POGOProtos.Inventory.AppliedItemsReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Responses.UseItemXpBoostResponse), global::POGOProtos.Networking.Responses.UseItemXpBoostResponse.Parser, new[]{ "Result", "AppliedItems" }, null, new[]{ typeof(global::POGOProtos.Networking.Responses.UseItemXpBoostResponse.Types.Result) }, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class UseItemXpBoostResponse : pb::IMessage<UseItemXpBoostResponse> {
    private static readonly pb::MessageParser<UseItemXpBoostResponse> _parser = new pb::MessageParser<UseItemXpBoostResponse>(() => new UseItemXpBoostResponse());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UseItemXpBoostResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::POGOProtos.Networking.Responses.UseItemXpBoostResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UseItemXpBoostResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UseItemXpBoostResponse(UseItemXpBoostResponse other) : this() {
      result_ = other.result_;
      AppliedItems = other.appliedItems_ != null ? other.AppliedItems.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UseItemXpBoostResponse Clone() {
      return new UseItemXpBoostResponse(this);
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 1;
    private global::POGOProtos.Networking.Responses.UseItemXpBoostResponse.Types.Result result_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::POGOProtos.Networking.Responses.UseItemXpBoostResponse.Types.Result Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "applied_items" field.</summary>
    public const int AppliedItemsFieldNumber = 2;
    private global::POGOProtos.Inventory.AppliedItems appliedItems_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::POGOProtos.Inventory.AppliedItems AppliedItems {
      get { return appliedItems_; }
      set {
        appliedItems_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UseItemXpBoostResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UseItemXpBoostResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Result != other.Result) return false;
      if (!object.Equals(AppliedItems, other.AppliedItems)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Result != 0) hash ^= Result.GetHashCode();
      if (appliedItems_ != null) hash ^= AppliedItems.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Result != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Result);
      }
      if (appliedItems_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(AppliedItems);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Result);
      }
      if (appliedItems_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AppliedItems);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UseItemXpBoostResponse other) {
      if (other == null) {
        return;
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
      if (other.appliedItems_ != null) {
        if (appliedItems_ == null) {
          appliedItems_ = new global::POGOProtos.Inventory.AppliedItems();
        }
        AppliedItems.MergeFrom(other.AppliedItems);
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
            result_ = (global::POGOProtos.Networking.Responses.UseItemXpBoostResponse.Types.Result) input.ReadEnum();
            break;
          }
          case 18: {
            if (appliedItems_ == null) {
              appliedItems_ = new global::POGOProtos.Inventory.AppliedItems();
            }
            input.ReadMessage(appliedItems_);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the UseItemXpBoostResponse message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum Result {
        [pbr::OriginalName("UNSET")] Unset = 0,
        [pbr::OriginalName("SUCCESS")] Success = 1,
        [pbr::OriginalName("ERROR_INVALID_ITEM_TYPE")] ErrorInvalidItemType = 2,
        [pbr::OriginalName("ERROR_XP_BOOST_ALREADY_ACTIVE")] ErrorXpBoostAlreadyActive = 3,
        [pbr::OriginalName("ERROR_NO_ITEMS_REMAINING")] ErrorNoItemsRemaining = 4,
        [pbr::OriginalName("ERROR_LOCATION_UNSET")] ErrorLocationUnset = 5,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
