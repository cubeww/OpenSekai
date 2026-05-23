using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	[Serializable]
	public class CommonMultiButtonDialogObjects
	{
		[SerializeField]
		private string key;

		[SerializeField]
		private CustomButton dialogButton;

		[SerializeField]
		private CustomTextMesh dialogButtonLabelMesh;

		public string Key => key;
		public CustomButton DialogButton => dialogButton;
		public CustomTextMesh DialogButtonLabelMesh => dialogButtonLabelMesh;
	}
}
