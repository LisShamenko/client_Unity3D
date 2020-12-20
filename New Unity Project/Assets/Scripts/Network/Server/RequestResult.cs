using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.Network.Server
{
    //
    [Serializable]
    public class RequestResult<ResponseObject> where ResponseObject : class
    {
        /// <summary>Сообщение об ошибке.</summary>
        public string Message;
        /// <summary>Статус ответа: fail, success</summary>
        public string Status;
        /// <summary>Объект результата.</summary>
        public ResponseObject Result;
    }
}
