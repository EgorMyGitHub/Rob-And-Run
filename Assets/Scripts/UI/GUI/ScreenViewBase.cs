using UnityEngine;

namespace UI.GUI
{
	public abstract class ScreenViewBase : MonoBehaviour
	{
		[field: SerializeField]
		public ScreenType ScreenType {get; private set; }
		
		public virtual void Initialize(ScreenViewModel model) { }

		public virtual void Show() { }
		public virtual void Hide() { }
	}
}