using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

namespace Sekai
{
	public class AssetBundleDownloadHandler : DownloadHandlerScript
	{
		public enum WriteStatus
		{
			None = 0,
			Writting = 1,
			Succcess = 2,
			Error = 3
		}

		[CompilerGenerated]
		private sealed class _003CWaitComplete_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public AssetBundleDownloadHandler _003C_003E4__this;

			private float _003Ctime_003E5__2;

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
			public _003CWaitComplete_003Ed__15(int _003C_003E1__state)
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

		public AssetBundleManager.DownloadResultCode ResultCode;

		private FileStream fs;

		private int offset;

		private int length;

		private WriteStatus status;

		public float Progress
		{
			get
			{
				throw null;
			}
		}

		public WriteStatus Status
		{
			get
			{
				throw null;
			}
		}

		public AssetBundleDownloadHandler(string path, byte[] buffer, int fileSize)
		{
			throw null;
		}

		protected override bool ReceiveData(byte[] data, int dataLength)
		{
			throw null;
		}

		protected override void CompleteContent()
		{
			throw null;
		}

		protected override void ReceiveContentLength(int contentLength)
		{
			throw null;
		}

		protected override float GetProgress()
		{
			throw null;
		}

		[IteratorStateMachine(typeof(_003CWaitComplete_003Ed__15))]
		public IEnumerator WaitComplete()
		{
			throw null;
		}

		public void Close()
		{
			throw null;
		}

		public void Abort()
		{
			throw null;
		}
	}
}
