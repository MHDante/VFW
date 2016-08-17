using System;
using UnityEngine;
using Vexe.Runtime.Types;

namespace VFWExamples
{
	public class ButtonExample : BaseBehaviour
	{
		[Button("Log1", "1", "toolbarButton"),
		 Button(0, "Log2", "2"),
		 Button(1, "Log3", "3", "miniButtonRight")]
		public int value;

		[PerItem, Button("Add")]
		public Component[] array;

		public ButtonStruct test;

		private void Log1(int x)
		{ Log("1: " + x); }

		private void Log2(int x)
		{ Log("2: " + x); }

		private void Log3(int x)
		{ Log("3: " + x); }

		private void Add(Component element, int index)
		{
			array[index] = GetComponent<Transform>();
		}

		[Serializable]
		public struct ButtonStruct
		{
			[PerItem, Button("LogGO", "Log"),
					  Button("ResetPos", "Reset")]
			public GameObject[] gos;

			private void ResetPos(GameObject go)
			{
				if (go != null)
					go.transform.position = Vector3.zero;
			}

			private void LogGO(GameObject go)
			{
				sLog(go);
			}
		}
	}
}
