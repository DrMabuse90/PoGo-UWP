// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: EquippedBadge.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Data.Player {

  /// <summary>Holder for reflection information generated from EquippedBadge.proto</summary>
  public static partial class EquippedBadgeReflection {

    #region Descriptor
    /// <summary>File descriptor for EquippedBadge.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static EquippedBadgeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNFcXVpcHBlZEJhZGdlLnByb3RvEhZQT0dPUHJvdG9zLkRhdGEuUGxheWVy",
            "GiBQT0dPUHJvdG9zL0VudW1zL0JhZGdlVHlwZS5wcm90byJ/Cg1FcXVpcHBl",
            "ZEJhZGdlEi8KCmJhZGdlX3R5cGUYASABKA4yGy5QT0dPUHJvdG9zLkVudW1z",
            "LkJhZGdlVHlwZRINCgVsZXZlbBgCIAEoBRIuCiZuZXh0X2VxdWlwX2NoYW5n",
            "ZV9hbGxvd2VkX3RpbWVzdGFtcF9tcxgDIAEoA2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::POGOProtos.Enums.BadgeTypeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Data.Player.EquippedBadge), global::POGOProtos.Data.Player.EquippedBadge.Parser, new[]{ "BadgeType", "Level", "NextEquipChangeAllowedTimestampMs" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class EquippedBadge : pb::IMessage<EquippedBadge> {
    private static readonly pb::MessageParser<EquippedBadge> _parser = new pb::MessageParser<EquippedBadge>(() => new EquippedBadge());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<EquippedBadge> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::POGOProtos.Data.Player.EquippedBadgeReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EquippedBadge() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EquippedBadge(EquippedBadge other) : this() {
      badgeType_ = other.badgeType_;
      level_ = other.level_;
      nextEquipChangeAllowedTimestampMs_ = other.nextEquipChangeAllowedTimestampMs_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public EquippedBadge Clone() {
      return new EquippedBadge(this);
    }

    /// <summary>Field number for the "badge_type" field.</summary>
    public const int BadgeTypeFieldNumber = 1;
    private global::POGOProtos.Enums.BadgeType badgeType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::POGOProtos.Enums.BadgeType BadgeType {
      get { return badgeType_; }
      set {
        badgeType_ = value;
      }
    }

    /// <summary>Field number for the "level" field.</summary>
    public const int LevelFieldNumber = 2;
    private int level_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Level {
      get { return level_; }
      set {
        level_ = value;
      }
    }

    /// <summary>Field number for the "next_equip_change_allowed_timestamp_ms" field.</summary>
    public const int NextEquipChangeAllowedTimestampMsFieldNumber = 3;
    private long nextEquipChangeAllowedTimestampMs_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long NextEquipChangeAllowedTimestampMs {
      get { return nextEquipChangeAllowedTimestampMs_; }
      set {
        nextEquipChangeAllowedTimestampMs_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as EquippedBadge);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(EquippedBadge other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (BadgeType != other.BadgeType) return false;
      if (Level != other.Level) return false;
      if (NextEquipChangeAllowedTimestampMs != other.NextEquipChangeAllowedTimestampMs) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (BadgeType != 0) hash ^= BadgeType.GetHashCode();
      if (Level != 0) hash ^= Level.GetHashCode();
      if (NextEquipChangeAllowedTimestampMs != 0L) hash ^= NextEquipChangeAllowedTimestampMs.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (BadgeType != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) BadgeType);
      }
      if (Level != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Level);
      }
      if (NextEquipChangeAllowedTimestampMs != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(NextEquipChangeAllowedTimestampMs);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (BadgeType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) BadgeType);
      }
      if (Level != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Level);
      }
      if (NextEquipChangeAllowedTimestampMs != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(NextEquipChangeAllowedTimestampMs);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(EquippedBadge other) {
      if (other == null) {
        return;
      }
      if (other.BadgeType != 0) {
        BadgeType = other.BadgeType;
      }
      if (other.Level != 0) {
        Level = other.Level;
      }
      if (other.NextEquipChangeAllowedTimestampMs != 0L) {
        NextEquipChangeAllowedTimestampMs = other.NextEquipChangeAllowedTimestampMs;
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
            badgeType_ = (global::POGOProtos.Enums.BadgeType) input.ReadEnum();
            break;
          }
          case 16: {
            Level = input.ReadInt32();
            break;
          }
          case 24: {
            NextEquipChangeAllowedTimestampMs = input.ReadInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
