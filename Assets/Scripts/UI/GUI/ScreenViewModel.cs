using System;
using Utils;

namespace UI.GUI
{
	public abstract class ScreenViewModel : IDisposable
	{
		protected readonly DisposableList DisposableList = new();
		
		public void Dispose()
		{
			DisposableList.Dispose();
		}
	}
}