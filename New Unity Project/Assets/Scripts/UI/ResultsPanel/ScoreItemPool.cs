using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewUnityProject.UI.Results
{
	// 
	public class ScoreItemPool : MonoBehaviour 
	{
		// 
		private Queue<ScoreItem> _queue;
		//
		private List<ScoreItem> _freeItems;
		// 
		[SerializeField] private ScoreItem _scoreItemPrefab;
		// 
		[SerializeField] private Transform _scoresParent;



		// 
		public void ResolveDependencies(ScoreItem scoreItemPrefab, Transform scoresParent)
		{
			_scoreItemPrefab = scoreItemPrefab;
			_scoresParent = scoresParent;
			_queue = new Queue<ScoreItem>();
			_freeItems = new List<ScoreItem>();
		}



		// 
		public void PushAll()
		{
			for (int i = 0; i < _freeItems.Count; i++)
			{
				PushInternal(_freeItems[i]);
			}
			_freeItems.Clear();
		}
		// 
		public void Push(ScoreItem item)
		{
			PushInternal(item);
			_freeItems.Remove(item);
		}
		//
		private void PushInternal(ScoreItem item)
		{
			item.transform.SetParent(null);
			item.gameObject.SetActive(false);
			item.transform.position = new Vector3(-100.0f, -100.0f, -100.0f);
			_queue.Enqueue(item);
		}

		// 
		public ScoreItem Pop()
		{
			ScoreItem item;	
			if (_queue.Count <= 0)
			{
				item = GameObject.Instantiate<ScoreItem>(_scoreItemPrefab);
			}
			else
			{
				item = _queue.Dequeue();
				item.gameObject.SetActive(true);
			}

			// 
			item.transform.SetParent(_scoresParent);
			_freeItems.Add(item);
			return item;
		}
	}
}