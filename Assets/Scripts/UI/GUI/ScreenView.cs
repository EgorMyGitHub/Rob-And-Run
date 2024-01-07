using Utils;

namespace UI.GUI
{
	public class ScreenView<TViewModel> : ScreenViewBase
		where TViewModel: ScreenViewModel, new()
	{
		protected TViewModel ViewModel;

		protected DisposableList DisposableList;

		public override void Initialize(ScreenViewModel model)
		{
			ViewModel = (TViewModel)model;
			
			base.Initialize(model);
		}

		public override void Hide()
		{
			DisposableList.Dispose();
		}
	}
}
