using System;
using Utils;

namespace UI.GUI
{
	public abstract class ScreenViewModel : IDisposable
	{
		protected readonly DisposableList DisposableList;
		
		public void Dispose()
		{
			DisposableList.Dispose();
		}
	}
}