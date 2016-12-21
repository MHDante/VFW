using UnityEngine;
using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public class ReadOnlyDrawer : CompositeDrawer<object, ReadOnlyAttribute>
	{
		public override void OnUpperGUI()
		{
			GUI.enabled = false;
		}

		public override void OnLowerGUI()
		{
			GUI.enabled = true;
		}
	}
}
