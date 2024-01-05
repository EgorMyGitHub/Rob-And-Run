using UnityEngine;

namespace UI.GUI
{
	public abstract class ScreenViewBase : MonoBehaviour
	{
		public virtual void Initialized(ScreenViewModel model)
		{
			Show();
		}

		public virtual void Show() { }
		public virtual void Hide() { }
	}
}