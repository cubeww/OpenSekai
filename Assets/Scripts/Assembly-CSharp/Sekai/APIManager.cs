using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
	public class APIManager : MonoBehaviour
	{
		private static APIManager instance;
		private readonly Dictionary<string, APICoreParam> executingApis = new Dictionary<string, APICoreParam>();

		public static APIManager Instance
		{
			get
			{
				if (instance == null)
				{
					var go = GameObject.Find("APIManager") ?? new GameObject("APIManager");
					instance = go.GetComponent<APIManager>() ?? go.AddComponent<APIManager>();
				}

				return instance;
			}
		}

		public string SessionToken { get; set; }

		public APICoreParam GetExecutingAPI(string apiId)
		{
			if (string.IsNullOrEmpty(apiId))
			{
				return null;
			}

			if (!executingApis.TryGetValue(apiId, out var api))
			{
				api = new APICoreParam();
				executingApis[apiId] = api;
			}

			return api;
		}

		public void CallRetry<A, B>(APICore<A, B> apiCore)
		{
			// TODO(original): replay failed API through original APIManager queue.
		}
	}
}
