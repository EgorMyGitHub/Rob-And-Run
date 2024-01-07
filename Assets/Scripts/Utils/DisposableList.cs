using System;
using System.Collections.Generic;

namespace Utils
{
	public class DisposableList : List<IDisposable>, IDisposable
	{
		public void Dispose()
		{
			ForEach(i => i.Dispose());
		}
	}
}
