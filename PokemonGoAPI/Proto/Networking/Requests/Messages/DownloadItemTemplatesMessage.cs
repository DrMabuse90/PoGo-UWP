// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: DownloadItemTemplatesMessage.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Networking.Requests.Messages {

  /// <summary>Holder for reflection information generated from DownloadItemTemplatesMessage.proto</summary>
  public static partial class DownloadItemTemplatesMessageReflection {

    #region Descriptor
    /// <summary>File descriptor for DownloadItemTemplatesMessage.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static DownloadItemTemplatesMessageReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiJEb3dubG9hZEl0ZW1UZW1wbGF0ZXNNZXNzYWdlLnByb3RvEidQT0dPUHJv",
            "dG9zLk5ldHdvcmtpbmcuUmVxdWVzdHMuTWVzc2FnZXMiXQocRG93bmxvYWRJ",
            "dGVtVGVtcGxhdGVzTWVzc2FnZRIQCghwYWdpbmF0ZRgBIAEoCBITCgtwYWdl",
            "X29mZnNldBgCIAEoBRIWCg5wYWdlX3RpbWVzdGFtcBgDIAEoBGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Requests.Messages.DownloadItemTemplatesMessage), global::POGOProtos.Networking.Requests.Messages.DownloadItemTemplatesMessage.Parser, new[]{ "Paginate", "PageOffset", "PageTimestamp" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class DownloadItemTemplatesMessage : pb::IMessage<DownloadItemTemplatesMessage> {
    private static readonly pb::MessageParser<DownloadItemTemplatesMessage> _parser = new pb::MessageParser<DownloadItemTemplatesMessage>(() => new DownloadItemTemplatesMessage());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<DownloadItemTemplatesMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::POGOProtos.Networking.Requests.Messages.DownloadItemTemplatesMessageReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DownloadItemTemplatesMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DownloadItemTemplatesMessage(DownloadItemTemplatesMessage other) : this() {
      paginate_ = other.paginate_;
      pageOffset_ = other.pageOffset_;
      pageTimestamp_ = other.pageTimestamp_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DownloadItemTemplatesMessage Clone() {
      return new DownloadItemTemplatesMessage(this);
    }

    /// <summary>Field number for the "paginate" field.</summary>
    public const int PaginateFieldNumber = 1;
    private bool paginate_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Paginate {
      get { return paginate_; }
      set {
        paginate_ = value;
      }
    }

    /// <summary>Field number for the "page_offset" field.</summary>
    public const int PageOffsetFieldNumber = 2;
    private int pageOffset_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int PageOffset {
      get { return pageOffset_; }
      set {
        pageOffset_ = value;
      }
    }

    /// <summary>Field number for the "page_timestamp" field.</summary>
    public const int PageTimestampFieldNumber = 3;
    private ulong pageTimestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong PageTimestamp {
      get { return pageTimestamp_; }
      set {
        pageTimestamp_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as DownloadItemTemplatesMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(DownloadItemTemplatesMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Paginate != other.Paginate) return false;
      if (PageOffset != other.PageOffset) return false;
      if (PageTimestamp != other.PageTimestamp) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Paginate != false) hash ^= Paginate.GetHashCode();
      if (PageOffset != 0) hash ^= PageOffset.GetHashCode();
      if (PageTimestamp != 0UL) hash ^= PageTimestamp.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Paginate != false) {
        output.WriteRawTag(8);
        output.WriteBool(Paginate);
      }
      if (PageOffset != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(PageOffset);
      }
      if (PageTimestamp != 0UL) {
        output.WriteRawTag(24);
        output.WriteUInt64(PageTimestamp);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Paginate != false) {
        size += 1 + 1;
      }
      if (PageOffset != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(PageOffset);
      }
      if (PageTimestamp != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(PageTimestamp);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(DownloadItemTemplatesMessage other) {
      if (other == null) {
        return;
      }
      if (other.Paginate != false) {
        Paginate = other.Paginate;
      }
      if (other.PageOffset != 0) {
        PageOffset = other.PageOffset;
      }
      if (other.PageTimestamp != 0UL) {
        PageTimestamp = other.PageTimestamp;
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
            Paginate = input.ReadBool();
            break;
          }
          case 16: {
            PageOffset = input.ReadInt32();
            break;
          }
          case 24: {
            PageTimestamp = input.ReadUInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
