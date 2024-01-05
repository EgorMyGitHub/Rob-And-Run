using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
