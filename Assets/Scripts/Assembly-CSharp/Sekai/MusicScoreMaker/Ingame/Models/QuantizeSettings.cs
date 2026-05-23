using System;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	[Serializable]
	public class QuantizeSettings
	{
		public enum QuantizeType
		{
			None = 0,
			Quarter = 1,
			Eighth = 2,
			Sixteenth = 3,
			ThirtySecond = 4,
			Triplet = 5,
			EighthTriplet = 6,
			DottedEighth = 7,
			DottedSixteenth = 8
		}

		[SerializeField]
		private QuantizeType _currentQuantizeType;

		[SerializeField]
		[Range(0f, 1f)]
		private float _quantizeStrength;

		public QuantizeType CurrentQuantizeType
		{
			get
			{
				return _currentQuantizeType;
			}
			set
			{
				_currentQuantizeType = value;
			}
		}

		public float QuantizeStrength
		{
			get
			{
				return _quantizeStrength;
			}
			set
			{
				_quantizeStrength = Mathf.Clamp01(value);
			}
		}

		public float GetQuantizeDivision(float timeSignature)
		{
			switch (_currentQuantizeType)
			{
				case QuantizeType.Quarter:
					return 1f / timeSignature;
				case QuantizeType.Eighth:
					return 1f / (timeSignature * 2f);
				case QuantizeType.Sixteenth:
					return 1f / (timeSignature * 4f);
				case QuantizeType.ThirtySecond:
					return 1f / (timeSignature * 8f);
				case QuantizeType.Triplet:
				case QuantizeType.DottedSixteenth:
					return 1f / (timeSignature * 3f);
				case QuantizeType.EighthTriplet:
					return 1f / (timeSignature * 6f);
				case QuantizeType.DottedEighth:
					return 1f / (timeSignature * 1.5f);
				default:
					return 0f;
			}
		}

		public QuantizeSettings()
		{
			_quantizeStrength = 1f;
		}
	}
}
