using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace UI.GUI
{
	public class ScreenView<TViewModel> : ScreenViewBase
		where TViewModel: ScreenViewModel, new()
	{
		protected TViewModel ViewModel;

		protected DisposableList DisposableList;

		public override void Initialized(ScreenViewModel model)
		{
			ViewModel = (TViewModel)model;
			
			base.Initialized(model);
		}

		public override void Hide()
		{
			DisposableList.Dispose();
		}
	}
}
