using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.UI
{
	// 
	public class UIPanel : MonoBehaviour
	{
		public void Hide()
		{
			gameObject.SetActive(false);
		}
		public void Show()
		{
			gameObject.SetActive(true);
		}

	}
}
