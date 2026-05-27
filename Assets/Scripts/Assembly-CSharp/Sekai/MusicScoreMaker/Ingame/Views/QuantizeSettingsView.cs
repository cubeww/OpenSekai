using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class QuantizeSettingsView : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CWaitForDropdownCloseAndOpenDialog_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			public QuantizeSettingsView _003C_003E4__this;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				_003C_003Et__builder.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private static readonly int[] PresetDivisions;

		private const int DefaultDivision = 16;

		private const int DefaultDropdownIndex = 3;

		[SerializeField]
		private CustomDropdown _quantizeDivisionDropdown;

		private int _customDivision;

		private bool _isCustomSelected;

		private static int CustomIndex
		{
			get
			{
				return PresetDivisions.Length;
			}
		}

		private static string GetPresetDivisionLabel(int division)
		{
			return WordingManager.GetFormat("WORD_QUANTIZE_FORMAT", division);
		}

		private void OnDestroy()
		{
			Dispose();
		}

		public void Setup()
		{
			InitializeQuantizeDivisionDropdown();
			UpdateUI();
			SetupEventDispatcher();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<IncrementQuantizeDivisionEvent>(IncrementQuantizeDivision);
			dispatcher.Remove<DecrementQuantizeDivisionEvent>(DecrementQuantizeDivision);
			dispatcher.Register<IncrementQuantizeDivisionEvent>(IncrementQuantizeDivision);
			dispatcher.Register<DecrementQuantizeDivisionEvent>(DecrementQuantizeDivision);
			if (_quantizeDivisionDropdown != null)
			{
				_quantizeDivisionDropdown.onValueChanged.RemoveListener(OnQuantizeDivisionChanged);
				_quantizeDivisionDropdown.onValueChanged.AddListener(OnQuantizeDivisionChanged);
			}
		}

		public void Dispose()
		{
			if (_quantizeDivisionDropdown != null)
			{
				_quantizeDivisionDropdown.onValueChanged.RemoveAllListeners();
				_quantizeDivisionDropdown.ClearItemClickCallbacks();
			}
			DisposeEventDispatcher();
		}

		private void DisposeEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<IncrementQuantizeDivisionEvent>(IncrementQuantizeDivision);
			dispatcher.Remove<DecrementQuantizeDivisionEvent>(DecrementQuantizeDivision);
		}

		private void InitializeQuantizeDivisionDropdown()
		{
			if (_quantizeDivisionDropdown == null)
			{
				return;
			}

			_quantizeDivisionDropdown.ClearOptions();
			List<TMPro.TMP_Dropdown.OptionData> options = new List<TMPro.TMP_Dropdown.OptionData>(PresetDivisions.Length + 1);
			for (int i = 0; i < PresetDivisions.Length; i++)
			{
				options.Add(new TMPro.TMP_Dropdown.OptionData(GetPresetDivisionLabel(PresetDivisions[i])));
			}
			options.Add(new TMPro.TMP_Dropdown.OptionData(WordingManager.Get("WORD_CUSTOM")));
			_quantizeDivisionDropdown.AddOptions(options);
			_quantizeDivisionDropdown.ClearItemClickCallbacks();
			_quantizeDivisionDropdown.SetOnItemClick(CustomIndex, OnCustomItemClicked);
			_quantizeDivisionDropdown.value = DefaultDropdownIndex;
			_customDivision = DefaultDivision;
			_isCustomSelected = false;
		}

		private void UpdateUI()
		{
			if (_quantizeDivisionDropdown == null)
			{
				return;
			}

			int division = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetQuantizeDivisionEvent, int>(new GetQuantizeDivisionEvent());
			if (division <= 0)
			{
				division = DefaultDivision;
			}
			int dropdownIndex = GetDropdownIndexFromDivision(division);
			_isCustomSelected = dropdownIndex == CustomIndex;
			if (_isCustomSelected)
			{
				_customDivision = division;
			}
			_quantizeDivisionDropdown.value = dropdownIndex;
		}

		private int GetDropdownIndexFromDivision(int division)
		{
			for (int i = 0; i < PresetDivisions.Length; i++)
			{
				if (PresetDivisions[i] == division)
				{
					return i;
				}
			}
			return CustomIndex;
		}

		private void OnQuantizeDivisionChanged(int value)
		{
			if (value < 0 || value == CustomIndex || value >= PresetDivisions.Length)
			{
				return;
			}

			_isCustomSelected = false;
			MusicScoreMakerEventDispatcher.Instance.Publish(new SetQuantizeDivisionEvent
			{
				Division = PresetDivisions[value]
			});
			UpdateUI();
		}

		private void OnCustomItemClicked()
		{
			if (_isCustomSelected && _quantizeDivisionDropdown != null)
			{
				_quantizeDivisionDropdown.Hide();
			}
			WaitForDropdownCloseAndOpenDialog().Forget();
		}

		private async UniTaskVoid WaitForDropdownCloseAndOpenDialog()
		{
			await UniTask.WaitUntil(() => _quantizeDivisionDropdown == null || !_quantizeDivisionDropdown.IsExpanded);
			OpenCustomQuantizeDialog();
		}

		private void OpenCustomQuantizeDialog()
		{
			int currentDivision = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetQuantizeDivisionEvent, int>(new GetQuantizeDivisionEvent());
			if (currentDivision <= 0)
			{
				currentDivision = _customDivision > 0 ? _customDivision : DefaultDivision;
			}

			MusicScoreMakerCustomQuantizeDialog dialog = null;
			dialog = ScreenManager.Instance.Show2ButtonDialog<MusicScoreMakerCustomQuantizeDialog>(
				DialogType.MusicScoreMakerCustomQuantizeDialog,
				null,
				"WORD_DECIDE",
				"WORD_CANCEL",
				() => OnCustomQuantizeDialogOK(dialog),
				OnCustomQuantizeDialogCancel,
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				true);
			dialog?.Setup(currentDivision);
		}

		private void OnCustomQuantizeDialogOK(MusicScoreMakerCustomQuantizeDialog dialog)
		{
			if (dialog == null)
			{
				return;
			}
			_customDivision = dialog.GetInputDivision();
			MusicScoreMakerEventDispatcher.Instance.Publish(new SetQuantizeDivisionEvent
			{
				Division = _customDivision
			});
			UpdateUI();
		}

		private void OnCustomQuantizeDialogCancel()
		{
			UpdateUI();
		}

		private void IncrementQuantizeDivision(IncrementQuantizeDivisionEvent e)
		{
			if (_quantizeDivisionDropdown == null)
			{
				return;
			}
			int value = _quantizeDivisionDropdown.value;
			if (value < CustomIndex)
			{
				_quantizeDivisionDropdown.value = value + 1;
			}
		}

		private void DecrementQuantizeDivision(DecrementQuantizeDivisionEvent e)
		{
			if (_quantizeDivisionDropdown == null)
			{
				return;
			}
			int value = _quantizeDivisionDropdown.value;
			if (value > 0)
			{
				_quantizeDivisionDropdown.value = value - 1;
			}
		}

		public QuantizeSettingsView()
		{
		}

		static QuantizeSettingsView()
		{
			PresetDivisions = new[] { 4, 8, 12, 16, 24, 32, 48, 64, 96, 128, 1920 };
		}
	}
}
