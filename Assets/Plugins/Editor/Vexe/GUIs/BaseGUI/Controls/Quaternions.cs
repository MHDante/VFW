using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public Quaternion QuaternionField(Quaternion value)
		{
			return QuaternionField(string.Empty, value);
		}

		public Quaternion QuaternionField(string label, Quaternion value)
		{
			return QuaternionField(label, value, null);
		}

		public Quaternion QuaternionField(string label, string tooltip, Quaternion value)
		{
			return QuaternionField(label, tooltip, value, null);
		}

		public Quaternion QuaternionField(string label, Quaternion value, Layout option)
		{
			return QuaternionField(label, string.Empty, value, option);
		}

		public Quaternion QuaternionField(string label, string tooltip, Quaternion value, Layout option)
		{
			return QuaternionField(GetContent(label, tooltip), value, option);
		}

		public Quaternion QuaternionField(GUIContent content, Quaternion value, Layout option)
		{
			if (hasReachedMinScreenWidth &&
				content != null && !string.IsNullOrEmpty(content.text))
			{
				using (Vertical())
				{
					Prefix(content);
					using (Horizontal())
					{
						Space(15f);
						INTERNAL_Quaternion(ref value);
					}
				}
			}
			else
			{
				using (Horizontal())
				{
					Prefix(content);
					INTERNAL_Quaternion(ref value);
				}
			}

			return value;
		}

		private void INTERNAL_Quaternion(ref Quaternion value)
		{
			using (LabelWidth(15f))
			{
				value.x = FloatField("X", value.x);
				Space(2f);
				value.y = FloatField("Y", value.y);
				Space(2f);
				value.z = FloatField("Z", value.z);
				Space(2f);
				value.w = FloatField("W", value.w);
			}
		}
	}
}
