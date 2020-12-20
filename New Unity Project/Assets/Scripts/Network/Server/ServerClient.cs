using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;

namespace NewUnityProject.Network.Server
{
	// 
	public class ServerClient 
	{

		#region internal types

		// 
		public class RequestState { }

		#endregion





		/// <summary></summary>
		public static Uri ServerURL { get; set; }
		// 
		private static RequestHeaders _emptyHeaders = new RequestHeaders("", "");



		// 
		public static HandlerItemReady ExecuteGetRequest(HandlerPoolBase handlerPool, string action)
		{
			return ExecuteGetRequest(handlerPool, _emptyHeaders, action);
		}
		// 
		public static HandlerItemReady ExecuteGetRequest(
			HandlerPoolBase handlerPool, RequestHeaders headers, string action)
		{
			HandlerItemReady handlerReady;
			handlerPool.PullHandler(out handlerReady);

			// 
			HttpWebRequest httpWebRequest = GetClientGet(action, headers);
			CallServerAsync(httpWebRequest, handlerReady.Handler);			
			return handlerReady;
		}

		// 
		public static HandlerItemReady ExecutePostRequest<RequestObject>(HandlerPoolBase handlerPool, string action, RequestObject requestObject) where RequestObject : class
		{
			return ExecutePostRequest<RequestObject>(handlerPool, action, _emptyHeaders, requestObject);
		}
		// 
		public static HandlerItemReady ExecutePostRequest<RequestObject>(
			HandlerPoolBase handlerPool, string action, RequestHeaders headers,
			RequestObject requestObject) where RequestObject : class
		{
			HandlerItemReady handlerReady;
			handlerPool.PullHandler(out handlerReady);

			// 
			string dataStr = JsonConvert.SerializeObject(requestObject);
			HttpWebRequest httpWebRequest = GetClientPost(action, headers, dataStr);
			CallServerAsync(httpWebRequest, handlerReady.Handler);
			return handlerReady;
		}



		// 
		public static RequestResult<ResponseObject> ReceiveRequestResult<ResponseObject>(
			HandlerItemReady handlerReady) where ResponseObject : class
		{
			RequestResult<ResponseObject> dataAfter = 
				JsonConvert.DeserializeObject<RequestResult<ResponseObject>>(
					handlerReady.Handler.Data);

			handlerReady.PushBack();
			return dataAfter;
		}



		// 
        private static HttpWebRequest GetClientGet(string action, RequestHeaders headers)
		{
			Uri requestUri = new Uri(ServerURL + action);
            HttpWebRequest httpWebRequest = WebRequest.Create(requestUri) as HttpWebRequest;
			httpWebRequest.Method = "GET";

			// 
			httpWebRequest.Headers.Add("UserId", headers.UserId);
			httpWebRequest.Headers.Add("Token", headers.Token);
			return httpWebRequest;
		}		
		// 
        private static HttpWebRequest GetClientPost(string action, RequestHeaders headers, string dataStr)
		{
			Uri requestUri = new Uri(ServerURL + action);
            HttpWebRequest httpWebRequest = WebRequest.Create(requestUri) as HttpWebRequest;
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/json";

			// 
			httpWebRequest.Headers.Add("UserId", headers.UserId);
			httpWebRequest.Headers.Add("Token", headers.Token);

            // 
			SetContent(httpWebRequest, dataStr);
			return httpWebRequest;
		}



		//
		private static void SetContent(HttpWebRequest httpWebRequest, string dataStr)
		{
            byte[] bytes = Encoding.UTF8.GetBytes(dataStr);
			httpWebRequest.ContentLength = bytes.Length;
			using (Stream stream = httpWebRequest.GetRequestStream())
			{
                stream.Write(bytes, 0, bytes.Length);
			}
		}



		// 
		private static void CallServerAsync(HttpWebRequest httpWebRequest, ResponseHandler handler)
		{
			handler.Start();

			// 
			Send(httpWebRequest, 
				delegate(HttpWebResponse response)
				{
					// 
					string resContent = "";
					try 
					{
						StreamReader streamReader = new StreamReader(response.GetResponseStream());
						resContent = streamReader.ReadToEnd();
					}
					catch (Exception ex)
					{
						handler.Exception = ex;
					}

	                // 
					switch (response.StatusCode)
					{
						case HttpStatusCode.OK:
							{
								handler.Data = resContent;
								handler.Complete();
							}
							break;
						case HttpStatusCode.Unauthorized:
							{
								handler.SetUnauthorizedState();
							}
							break;
						default:
							{
								handler.Stop();
							}
							break;
					}
				});
		}
		// 
        private static void Send(HttpWebRequest client, Action<HttpWebResponse> callback)
        {
            ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.ServerCertificateValidationCallback = (
				RemoteCertificateValidationCallback)Delegate.Combine(
					ServicePointManager.ServerCertificateValidationCallback, 
					(RemoteCertificateValidationCallback)((object a, X509Certificate b, X509Chain c, SslPolicyErrors d) => true));

            // 
            client.BeginGetResponse(
                delegate (IAsyncResult result) 
                {
					HttpWebResponse response = client.EndGetResponse(result) as HttpWebResponse;
                    callback(response);
                }, 
                new RequestState());
        }

	}
}