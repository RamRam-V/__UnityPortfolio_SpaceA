using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerProtocol
{
    [Serializable]
    public class PlayerTransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Quaternion camRot;
    }

    [Serializable]
    public class TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    [Serializable]
    public class PlayerAnimationData
    {
        public bool isMoving;
        public bool isJump;
        public bool isSprint;
        public bool isGrounded;
    }
    [Serializable]
    public class PlayerData
    {
        public UserCharacterInfo info;
        public PlayerTransformData transformData;
        public PlayerAnimationData animationData;
    }

}
