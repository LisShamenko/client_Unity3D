using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server.DataClasses
{
    //
    [Serializable]
    public class GameResultsData
    {
        public UserResultData[] Results;
    }
}
