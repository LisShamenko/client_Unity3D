using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewUnityProject.UI.Auth
{
	public class AuthPanel : UIPanel
	{

		// 
		[SerializeField] private Text _message;
		// 
		[SerializeField] private InputField _userName;
		// 
		[SerializeField] private InputField _userPassword;

		

		// 
		public void ResolveDependencies(
			UserRegisterDelegate userRegisterCallback,
			UserLoginDelegate userLoginCallback)
		{

			// 
			SetUserRegisterEvent(userRegisterCallback);
			SetUserLoginEvent(userLoginCallback);
		}



		// 
		public void SetMessage(string message)
		{
			_message.text = message;
		}

		// 
		public void OnButtonQuitClick()
		{
			Application.Quit();
		}
		// 
		public void OnLoginButtonClick()
		{
			if (string.IsNullOrEmpty(_userName.text) || 
				string.IsNullOrEmpty(_userPassword.text)) 
				return;

			// 
			UserLoginCall(_userName.text, _userPassword.text);
		}
		// 
		public void OnRegisterButtonClick()
		{
			if (string.IsNullOrEmpty(_userName.text) || 
				string.IsNullOrEmpty(_userPassword.text)) 
				return;

			// 
			UserRegisterCall(_userName.text, _userPassword.text);
		}







        #region events


        // 
        public delegate void UserRegisterDelegate(string name, string password);
		public delegate void UserLoginDelegate(string name, string password);


        //
        private event UserRegisterDelegate UserRegisterEvent;
        //
        private event UserLoginDelegate UserLoginEvent;


		// 		
		public void SetUserRegisterEvent(UserRegisterDelegate setDelegate)
		{
			UserRegisterEvent = setDelegate;
		}
		// 		
		public void SetUserLoginEvent(UserLoginDelegate setDelegate)
		{
			UserLoginEvent = setDelegate;
		}


        //
        private void UserRegisterCall(string name, string password)
        {
            if (UserRegisterEvent != null)
                UserRegisterEvent(name, password);
        }
        //
        private void UserLoginCall(string name, string password)
        {
            if (UserLoginEvent != null)
                UserLoginEvent(name, password);
        }


        #endregion
	}
}