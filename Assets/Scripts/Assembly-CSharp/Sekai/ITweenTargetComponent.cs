using UnityEngine;

namespace Sekai
{
	public interface ITweenTargetComponent
	{
		Vector3 LocalPosition { get; set; }

		Vector3 LocalRotation { get; set; }

		Vector3 LocalScale { get; set; }

		Color Color { get; set; }

		float Alpha { get; set; }

		bool IsExist { get; }

		void Initialize(GameObject targetObject);
	}
}
