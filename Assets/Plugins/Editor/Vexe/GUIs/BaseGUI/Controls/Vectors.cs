using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public Vector4 Vector4Field(Vector4 value)
		{
			return Vector4Field(string.Empty, value);
		}

		public Vector4 Vector4Field(string label, Vector4 value)
		{
			return Vector4Field(label, value, null);
		}

		public Vector4 Vector4Field(string label, string tooltip, Vector4 value)
		{
			return Vector4Field(label, tooltip, value, null);
		}

		public Vector4 Vector4Field(string label, Vector4 value, Layout option)
		{
			return Vector4Field(label, string.Empty, value, option);
		}

		public Vector4 Vector4Field(string label, string tooltip, Vector4 value, Layout option)
		{
			return Vector4Field(GetContent(label, tooltip), value, option);
		}

		public Vector4 Vector4Field(GUIContent content, Vector4 value, Layout option)
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
						INTERNAL_Vector4(ref value);
					}
				}
			}
			else
			{
				using (Horizontal())
				{
					Prefix(content);
					INTERNAL_Vector4(ref value);
				}
			}

			return value;
		}

		private void INTERNAL_Vector4(ref Vector4 value)
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

		public Vector3 Vector3Field(Vector3 value)
		{
			return Vector3Field(string.Empty, value);
		}

		public Vector3 Vector3Field(string label, Vector3 value)
		{
			return Vector3Field(label, value, null);
		}

		public Vector3 Vector3Field(string label, string tooltip, Vector3 value)
		{
			return Vector3Field(label, tooltip, value, null);
		}

		public Vector3 Vector3Field(string label, Vector3 value, Layout option)
		{
			return Vector3Field(label, string.Empty, value, option);
		}

		public Vector3 Vector3Field(string label, string tooltip, Vector3 value, Layout option)
		{
			return Vector3Field(GetContent(label, tooltip), value, option);
		}

		public Vector3 Vector3Field(GUIContent content, Vector3 value, Layout option)
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
						INTERNAL_Vector3(ref value);
					}
				}
			}
			else
			{
				using (Horizontal())
				{
					Prefix(content);
					INTERNAL_Vector3(ref value);
				}
			}

			return value;
		}

		private void INTERNAL_Vector3(ref Vector3 value)
		{
			using (LabelWidth(15f))
			{
				value.x = FloatField("X", value.x);
				Space(2f);
				value.y = FloatField("Y", value.y);
				Space(2f);
				value.z = FloatField("Z", value.z);
			}
		}

		public Vector2 Vector2Field(Vector2 value)
		{
			return Vector2Field(string.Empty, value);
		}

		public Vector2 Vector2Field(string label, Vector2 value)
		{
			return Vector2Field(label, value, null);
		}

		public Vector2 Vector2Field(string label, string tooltip, Vector2 value)
		{
			return Vector2Field(label, tooltip, value, null);
		}

		public Vector2 Vector2Field(string label, Vector2 value, Layout option)
		{
			return Vector2Field(label, string.Empty, value, option);
		}

		public Vector2 Vector2Field(string label, string tooltip, Vector2 value, Layout option)
		{
			return Vector2Field(GetContent(label, tooltip), value, option);
		}

		public Vector2 Vector2Field(GUIContent content, Vector2 value, Layout option)
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
						INTERNAL_Vector2(ref value);
					}
				}
			}
			else
			{
				using (Horizontal())
				{
					Prefix(content);
					INTERNAL_Vector2(ref value);
				}
			}

			return value;
		}

		private void INTERNAL_Vector2(ref Vector2 value)
		{
			using (LabelWidth(15f))
			{
				value.x = FloatField("X", value.x);
				Space(2f);
				value.y = FloatField("Y", value.y);
			}
		}
	}
}
