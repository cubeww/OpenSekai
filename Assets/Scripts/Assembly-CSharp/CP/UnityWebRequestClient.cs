using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using Sekai;
using UnityEngine;
using UnityEngine.Networking;

namespace CP
{
	public class UnityWebRequestClient : IDisposable
	{
		[CompilerGenerated]
		private sealed class _003CWaitForResponse_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UnityWebRequestClient _003C_003E4__this;

			public Action<UnityWebRequestClient> onProgress;

			private float _003CtimeoutSec_003E5__2;

			private float _003CbeforeProgress_003E5__3;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			[DebuggerHidden]
			public _003CWaitForResponse_003Ed__30(int _003C_003E1__state)
			{
				throw null;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				throw null;
			}

			private bool MoveNext()
			{
				throw null;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw null;
			}
		}

		[CompilerGenerated]
		private sealed class _003CWaitForResponseForAssetBundle_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UnityWebRequestClient _003C_003E4__this;

			public Action<UnityWebRequestClient, float> onProgress;

			private float _003CtimeoutSec_003E5__2;

			private float _003CbeforeProgress_003E5__3;

			private float _003Cprogress_003E5__4;

			private AssetBundleDownloadHandler _003CdownloadHandler_003E5__5;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			[DebuggerHidden]
			public _003CWaitForResponseForAssetBundle_003Ed__31(int _003C_003E1__state)
			{
				throw null;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				throw null;
			}

			private bool MoveNext()
			{
				throw null;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw null;
			}
		}

		private const float TIMEOUT_SECOND = 60f;

		private UnityWebRequest www;

		public HttpResult ResultInfo
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public string ResponseText
		{
			get
			{
				throw null;
			}
		}

		public Texture2D ResponseTexture
		{
			get
			{
				throw null;
			}
		}

		public byte[] ResponseBytes
		{
			get
			{
				throw null;
			}
		}

		public Dictionary<string, string> ResponseHeaders
		{
			get
			{
				throw null;
			}
		}

		public float Progress
		{
			get
			{
				throw null;
			}
		}

		public void AddRequestHeader(string key, string value)
		{
			throw null;
		}

		public void SetRequestHeaders(Dictionary<string, string> headers)
		{
			throw null;
		}

		public void Get(string url, Dictionary<string, string> headers = null)
		{
			throw null;
		}

		public void Get(string url, DownloadHandler downloadHandler)
		{
			throw null;
		}

		public void Get(string url, DownloadHandler downloadHandler, Dictionary<string, string> headers = null)
		{
			throw null;
		}

		public void Post(string url, byte[] requestBody, Dictionary<string, string> headers = null)
		{
			throw null;
		}

		public void Put(string url, byte[] requestBody, Dictionary<string, string> headers = null)
		{
			throw null;
		}

		public void Patch(string url, byte[] requestBody, Dictionary<string, string> headers = null)
		{
			throw null;
		}

		public void Delete(string url, byte[] requestBody, Dictionary<string, string> headers = null)
		{
			throw null;
		}

		public void SetUpRequest(string url, string method, UploadHandler uploadHandler, Dictionary<string, string> headers = null)
		{
			throw null;
		}

		public void SetUpRequest(string url, string method, UploadHandler uploadHandler, DownloadHandler downloadHandler, Dictionary<string, string> headers = null)
		{
			throw null;
		}

		public void SetRequestBody(byte[] requestBody)
		{
			throw null;
		}

		public void SetCertificateHandler(CertificateHandler handler)
		{
			throw null;
		}

		public void SendRequest()
		{
			throw null;
		}

		[IteratorStateMachine(typeof(_003CWaitForResponse_003Ed__30))]
		public IEnumerator WaitForResponse(Action<UnityWebRequestClient> onProgress = null)
		{
			throw null;
		}

		[IteratorStateMachine(typeof(_003CWaitForResponseForAssetBundle_003Ed__31))]
		public IEnumerator WaitForResponseForAssetBundle(Action<UnityWebRequestClient, float> onProgress = null)
		{
			throw null;
		}

		public void WaitForResponseSync(int timeOutSec)
		{
			throw null;
		}

		private void ParseStatusCode()
		{
			throw null;
		}

		public void Reset()
		{
			throw null;
		}

		[Skip]
		public void Close()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public void CloseDownloadHandler()
		{
			throw null;
		}

		public UnityWebRequestClient()
		{
			}
	}
}
