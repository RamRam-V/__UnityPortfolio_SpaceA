using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerProtocol
{
    [Serializable]
    public class UserCharacterInfo
    {
        public string nickname;
        public CharacterType type;
    }

    [Serializable]
    public class RegisterInfo
    {
        public string token;
        public UserCharacterInfo info;
    }

    public enum CharacterType
    {
        ECO,
        LEON,
        AMELI
    }

    public enum CreationStep
    {
        IDLE,
        CUSTOMIZE,
        SET_NICKNAME
    }
}
