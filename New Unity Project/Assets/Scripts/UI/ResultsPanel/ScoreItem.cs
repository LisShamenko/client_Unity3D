using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewUnityProject.UI.Results
{
	// 
	public class ScoreItem : MonoBehaviour
	{
		// 
		[SerializeField] private Text _name;
		// 
		[SerializeField] private Text _scores;

		//
		public void SetScores(string name, string scores)
		{
			_name.text = name;
			_scores.text = scores;
		}
	}
}