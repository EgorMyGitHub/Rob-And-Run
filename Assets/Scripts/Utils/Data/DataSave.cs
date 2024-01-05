using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Data
{
	public static class DataSave
	{
		private static Dictionary<Type, object> data = new();

		public static void Save<T>(T obj)
		{
			var type = typeof(T);

			if (!data.ContainsKey(type))
				data.Add(type, null);

			data[type] = obj;
		}

		public static T Load<T>()
		{
			var type = typeof(T);

			if (!data.ContainsKey(type))
				throw new KeyNotFoundException($"Dont exists data: {type}");

			return (T)data[type];
		}
	}
}
