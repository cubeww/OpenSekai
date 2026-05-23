using System;
using System.IO;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreSaveDataManagerDialog : Common1ButtonDialog
	{
		[SerializeField]
		private RectTransform _scoreListContent;

		[SerializeField]
		private GameObject _scoreListItemPrefab;

		[SerializeField]
		private CustomButton _saveButton;

		[SerializeField]
		private CustomTextMesh _saveButtonText;

		[SerializeField]
		private CustomButton _loadButton;

		[SerializeField]
		private CustomButton _deleteButton;

		[SerializeField]
		private CustomInputFieldTextMesh _fileNameInputField;

		private (string name, DateTime lastWriteTime)[] _scoreNames;

		private string _selectedFile;

		private void DeleteAllMusicScore()
		{
			// TODO(original): restore the exact original save-file glob before enabling bulk deletion.
			string[] files = Directory.GetFiles(Application.persistentDataPath, "*.musicScore", SearchOption.TopDirectoryOnly);
			foreach (string file in files)
			{
				File.Delete(file);
			}
		}

		public void Setup(string lastFileName = "")
		{
			if (_fileNameInputField != null)
			{
				_fileNameInputField.text = lastFileName ?? string.Empty;
				_fileNameInputField.onValueChanged.RemoveAllListeners();
				_fileNameInputField.onValueChanged.AddListener(value =>
				{
					_selectedFile = value;
					UpdateSaveButtonState();
				});
			}
			_selectedFile = lastFileName ?? string.Empty;
			RefreshList();
			if (_saveButton != null)
			{
				_saveButton.onClick.RemoveAllListeners();
				_saveButton.onClick.AddListener(OnSaveClicked);
			}
			if (_loadButton != null)
			{
				_loadButton.onClick.RemoveAllListeners();
				_loadButton.onClick.AddListener(OnLoadClicked);
			}
			if (_deleteButton != null)
			{
				_deleteButton.onClick.RemoveAllListeners();
				_deleteButton.onClick.AddListener(OnDeleteClicked);
			}
			UpdateSaveButtonState();
		}

		private void RefreshList()
		{
			if (_scoreListContent == null)
			{
				return;
			}
			for (int i = _scoreListContent.childCount - 1; i >= 0; i--)
			{
				Destroy(_scoreListContent.GetChild(i).gameObject);
			}

			_scoreNames = MusicScoreMakerEventDispatcher.Instance.PublishFirst<LoadMusicScoreNamesEvent, (string name, DateTime lastWriteTime)[]>(new LoadMusicScoreNamesEvent()) ?? Array.Empty<(string name, DateTime lastWriteTime)>();
			int selectedIndex = -1;
			for (int i = 0; i < _scoreNames.Length; i++)
			{
				if (_scoreListItemPrefab == null)
				{
					break;
				}

				int index = i;
				GameObject item = Instantiate(_scoreListItemPrefab, _scoreListContent);
				item.SetActive(true);
				CustomTextMesh text = item.GetComponentInChildren<CustomTextMesh>(true);
				if (text != null)
				{
					text.SetText($"{_scoreNames[i].name}\n{_scoreNames[i].lastWriteTime:g}");
				}
				Button button = item.GetComponent<Button>();
				if (button != null)
				{
					button.onClick.RemoveAllListeners();
					button.onClick.AddListener(() => OnSelectScore(index));
					button.enabled = _selectedFile != _scoreNames[i].name;
				}
				if (_selectedFile == _scoreNames[i].name)
				{
					selectedIndex = i;
				}
			}

			if (selectedIndex >= 0)
			{
				UpdateList(selectedIndex);
			}
			else
			{
				UpdateSaveButtonState();
			}
		}

		private void OnSelectScore(int index)
		{
			if (_scoreNames == null || index < 0 || index >= _scoreNames.Length)
			{
				return;
			}
			_selectedFile = _scoreNames[index].name;
			if (_fileNameInputField != null)
			{
				_fileNameInputField.text = _selectedFile;
			}
			UpdateList(index);
		}

		private void UpdateList(int index)
		{
			if (_scoreListContent != null)
			{
				for (int i = 0; i < _scoreListContent.childCount; i++)
				{
					Button button = _scoreListContent.GetChild(i).GetComponent<Button>();
					if (button != null)
					{
						button.enabled = i != index;
					}
				}
			}
			UpdateSaveButtonState();
		}

		private void OnSaveClicked()
		{
			string fileName = _fileNameInputField != null ? _fileNameInputField.text : _selectedFile;
			if (string.IsNullOrEmpty(fileName))
			{
				return;
			}
			_selectedFile = fileName;
			MusicScoreMakerEventDispatcher.Instance.Publish(new SaveMusicScoreEvent
			{
				FileName = fileName
			});
			RefreshList();
		}

		private void OnLoadClicked()
		{
			if (string.IsNullOrEmpty(_selectedFile))
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.Publish(new LoadMusicScoreEvent
			{
				FileName = _selectedFile
			});
		}

		private void OnDeleteClicked()
		{
			if (string.IsNullOrEmpty(_selectedFile))
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.Publish(new DeleteMusicScoreEvent
			{
				FileName = _selectedFile
			});
			_selectedFile = string.Empty;
			if (_fileNameInputField != null)
			{
				_fileNameInputField.text = string.Empty;
			}
			RefreshList();
		}

		public MusicScoreSaveDataManagerDialog()
		{
		}

		private void UpdateSaveButtonState()
		{
			bool canSave = !string.IsNullOrEmpty(_selectedFile) && !_selectedFile.StartsWith("AutoSave", StringComparison.OrdinalIgnoreCase);
			if (_saveButton != null)
			{
				_saveButton.enabled = canSave;
				_saveButton.interactable = canSave;
			}
			if (_saveButtonText != null)
			{
				bool overwrite = Array.Exists(_scoreNames ?? Array.Empty<(string name, DateTime lastWriteTime)>(), score => score.name == _selectedFile);
				_saveButtonText.SetText(overwrite ? "Overwrite" : "Save");
			}
		}
	}
}
