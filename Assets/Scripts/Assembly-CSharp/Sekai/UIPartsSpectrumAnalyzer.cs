using System;
using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsSpectrumAnalyzer : MonoBehaviour
	{
		[Serializable]
		public class SpectrumView
		{
			[SerializeField]
			private CustomImage barImage;

			[SerializeField]
			private int bufferIndex;

			public CustomImage BarImage
			{
				get
				{
					throw null;
				}
				set
				{
					throw null;
				}
			}

			public int BufferIndex
			{
				get
				{
					throw null;
				}
				set
				{
					throw null;
				}
			}

			public SpectrumView()
			{
				throw null;
			}
		}

		[SerializeField]
		private List<SpectrumView> views;

		private bool _useInGameBGM;

		private const float BASE_LEVEL_THRESHOLD = 0.02f;

		private float[] buffers;

		private Vector3 zeroScale;

		private Vector3 tempScale;

		private Vector3 pausedScale;

		private void Awake()
		{
			throw null;
		}

		private void OnDisable()
		{
			throw null;
		}

		private void Update()
		{
			throw null;
		}

		private void Reset()
		{
			throw null;
		}

		private void Pause()
		{
			throw null;
		}

		private void SetupSpectrumView(Vector3 scale)
		{
			throw null;
		}

		public void SetUseInGameBGM(bool useInGameBGM)
		{
			throw null;
		}

		public void Show()
		{
			throw null;
		}

		public void Hide()
		{
			throw null;
		}

		public UIPartsSpectrumAnalyzer()
		{
			throw null;
		}
	}
}
