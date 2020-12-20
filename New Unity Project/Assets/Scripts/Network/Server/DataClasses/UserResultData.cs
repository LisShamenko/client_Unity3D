using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server.DataClasses
{
    //
    [Serializable]
    public class UserResultData
    {
        public int Result;
        public string Name;

        // 
        public UserResultData(int result, string name)
        {
            Result = result;
            Name = name;
        }
    }
}
