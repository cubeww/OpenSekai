using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker
{
	public sealed class MusicScoreMakerMusicScoreIdSearchDialog : Common2ButtonDialog, IDisposable
	{
		[SerializeField]
		private CustomInputFieldTextMesh _musicScoreIdInputField;

		private Action<MusicScoreData> _onFoundMusicScoreData;

		private string _musicScoreId;

		private int _musicScoreIdMaxLength;

		private bool _isExecutingAPI;

		public static MusicScoreMakerMusicScoreIdSearchDialog Show([NotNull] Action<MusicScoreData> onSearchMusicScoreDataCallback)
		{
			MusicScoreMakerMusicScoreIdSearchDialog dialog = null;
			try
			{
				dialog = ScreenManager.Instance?.Show2ButtonDialog<MusicScoreMakerMusicScoreIdSearchDialog>(
					DialogType.MusicScoreMakerMusicScoreIdSearchDialog,
					(Action)null,
					(Action)null,
					DisplayLayerType.Layer_Dialog,
					DialogSize.Manual);
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"MusicScoreMakerMusicScoreIdSearchDialog.Show fallback: {ex.Message}");
			}

			if (dialog == null)
			{
				GameObject dialogObject = new GameObject(nameof(MusicScoreMakerMusicScoreIdSearchDialog));
				dialog = dialogObject.AddComponent<MusicScoreMakerMusicScoreIdSearchDialog>();
			}

			dialog.Setup(onSearchMusicScoreDataCallback);
			return dialog;
		}

		private void Setup([NotNull] Action<MusicScoreData> onSearchMusicScoreDataCallback)
		{
			_onFoundMusicScoreData = onSearchMusicScoreDataCallback;
			// TODO(original): restore ClientConfig.MusicScoreMaker.MusicScoreIdMaxLength.
			_musicScoreIdMaxLength = 20;

			if (_musicScoreIdInputField == null)
			{
				return;
			}

			_musicScoreIdInputField.characterLimit = _musicScoreIdMaxLength;
			_musicScoreIdInputField.onValueChanged.RemoveListener(OnValueChangedMusicScoreId);
			_musicScoreIdInputField.onValueChanged.AddListener(OnValueChangedMusicScoreId);
			_musicScoreIdInputField.OnEndEditSuccess = OnEndEditMusicScoreIdSuccess;
			_musicScoreId = _musicScoreIdInputField.text ?? string.Empty;
		}

		private void OnEndEditMusicScoreIdSuccess(string musicScoreId)
		{
			_musicScoreId = musicScoreId;
		}

		private void OnValueChangedMusicScoreId(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				_musicScoreId = string.Empty;
				return;
			}

			char[] filtered = new char[Math.Min(value.Length, _musicScoreIdMaxLength)];
			int count = 0;
			foreach (char character in value)
			{
				if (count >= _musicScoreIdMaxLength)
				{
					break;
				}
				if (IsAsciiAlphaNumeric(character))
				{
					filtered[count++] = character;
				}
			}

			string normalized = new string(filtered, 0, count);
			_musicScoreId = normalized;
			if (_musicScoreIdInputField != null && !string.Equals(_musicScoreIdInputField.text, normalized, StringComparison.Ordinal))
			{
				_musicScoreIdInputField.SetTextWithoutNotify(normalized);
			}
		}

		protected override void OnClickOK()
		{
			if (_isExecutingAPI)
			{
				return;
			}

			if (IsValidMusicScoreId())
			{
				ExecuteFind().Forget();
			}
			else
			{
				ShowNotFound();
			}
		}

		public override void Close()
		{
			_onFoundMusicScoreData = null;
			base.Close();
		}

		private bool IsValidMusicScoreId()
		{
			if (string.IsNullOrEmpty(_musicScoreId) || _musicScoreId.Length > _musicScoreIdMaxLength)
			{
				return false;
			}

			foreach (char character in _musicScoreId)
			{
				if (!IsAsciiAlphaNumeric(character))
				{
					return false;
				}
			}
			return true;
		}

		private static bool IsAsciiAlphaNumeric(char character)
		{
			return character >= '0' && character <= '9'
				|| character >= 'A' && character <= 'Z'
				|| character >= 'a' && character <= 'z'
				|| character == '_'
				|| character == '-';
		}

		private async UniTask ExecuteFind()
		{
			_isExecutingAPI = true;
			try
			{
				await UniTask.Yield();
				// TODO(original): restore Sekai.Service.CustomMusicScorePublishedSearchByIdService
				// and MusicScoreDataFactory.ConvertToMusicScoreData API conversion.
				ShowNotFound();
			}
			finally
			{
				_isExecutingAPI = false;
			}
		}

		private void ShowNotFound()
		{
			Debug.LogWarning($"Music score not found: {_musicScoreId}");
			try
			{
				ScreenManager.Instance?.ShowSubWindowDialog<SubWindowDialog>("Music score not found.");
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"MusicScoreMakerMusicScoreIdSearchDialog.ShowNotFound fallback: {ex.Message}");
			}
		}

		public void Dispose()
		{
			_onFoundMusicScoreData = null;
		}

		public MusicScoreMakerMusicScoreIdSearchDialog()
		{
			_musicScoreId = string.Empty;
		}
	}
}
