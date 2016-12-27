// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: CameraTarget.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Enums {

  /// <summary>Holder for reflection information generated from CameraTarget.proto</summary>
  public static partial class CameraTargetReflection {

    #region Descriptor
    /// <summary>File descriptor for CameraTarget.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CameraTargetReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJDYW1lcmFUYXJnZXQucHJvdG8SEFBPR09Qcm90b3MuRW51bXMq/AMKDENh",
            "bWVyYVRhcmdldBIXChNDQU1fVEFSR0VUX0FUVEFDS0VSEAASHAoYQ0FNX1RB",
            "UkdFVF9BVFRBQ0tFUl9FREdFEAESHgoaQ0FNX1RBUkdFVF9BVFRBQ0tFUl9H",
            "Uk9VTkQQAhIXChNDQU1fVEFSR0VUX0RFRkVOREVSEAMSHAoYQ0FNX1RBUkdF",
            "VF9ERUZFTkRFUl9FREdFEAQSHgoaQ0FNX1RBUkdFVF9ERUZFTkRFUl9HUk9V",
            "TkQQBRIgChxDQU1fVEFSR0VUX0FUVEFDS0VSX0RFRkVOREVSEAYSJQohQ0FN",
            "X1RBUkdFVF9BVFRBQ0tFUl9ERUZFTkRFUl9FREdFEAcSIAocQ0FNX1RBUkdF",
            "VF9ERUZFTkRFUl9BVFRBQ0tFUhAIEiUKIUNBTV9UQVJHRVRfREVGRU5ERVJf",
            "QVRUQUNLRVJfRURHRRAJEicKI0NBTV9UQVJHRVRfQVRUQUNLRVJfREVGRU5E",
            "RVJfTUlSUk9SEAsSKQolQ0FNX1RBUkdFVF9TSE9VTERFUl9BVFRBQ0tFUl9E",
            "RUZFTkRFUhAMEjAKLENBTV9UQVJHRVRfU0hPVUxERVJfQVRUQUNLRVJfREVG",
            "RU5ERVJfTUlSUk9SEA0SJgoiQ0FNX1RBUkdFVF9BVFRBQ0tFUl9ERUZFTkRF",
            "Ul9XT1JMRBAOYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::POGOProtos.Enums.CameraTarget), }, null));
    }
    #endregion

  }
  #region Enums
  public enum CameraTarget {
    [pbr::OriginalName("CAM_TARGET_ATTACKER")] CamTargetAttacker = 0,
    [pbr::OriginalName("CAM_TARGET_ATTACKER_EDGE")] CamTargetAttackerEdge = 1,
    [pbr::OriginalName("CAM_TARGET_ATTACKER_GROUND")] CamTargetAttackerGround = 2,
    [pbr::OriginalName("CAM_TARGET_DEFENDER")] CamTargetDefender = 3,
    [pbr::OriginalName("CAM_TARGET_DEFENDER_EDGE")] CamTargetDefenderEdge = 4,
    [pbr::OriginalName("CAM_TARGET_DEFENDER_GROUND")] CamTargetDefenderGround = 5,
    [pbr::OriginalName("CAM_TARGET_ATTACKER_DEFENDER")] CamTargetAttackerDefender = 6,
    [pbr::OriginalName("CAM_TARGET_ATTACKER_DEFENDER_EDGE")] CamTargetAttackerDefenderEdge = 7,
    [pbr::OriginalName("CAM_TARGET_DEFENDER_ATTACKER")] CamTargetDefenderAttacker = 8,
    [pbr::OriginalName("CAM_TARGET_DEFENDER_ATTACKER_EDGE")] CamTargetDefenderAttackerEdge = 9,
    [pbr::OriginalName("CAM_TARGET_ATTACKER_DEFENDER_MIRROR")] CamTargetAttackerDefenderMirror = 11,
    [pbr::OriginalName("CAM_TARGET_SHOULDER_ATTACKER_DEFENDER")] CamTargetShoulderAttackerDefender = 12,
    [pbr::OriginalName("CAM_TARGET_SHOULDER_ATTACKER_DEFENDER_MIRROR")] CamTargetShoulderAttackerDefenderMirror = 13,
    [pbr::OriginalName("CAM_TARGET_ATTACKER_DEFENDER_WORLD")] CamTargetAttackerDefenderWorld = 14,
  }

  #endregion

}

#endregion Designer generated code
