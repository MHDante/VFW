using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public Matrix4x4 Matrix4x4Field(Matrix4x4 value)
		{
			return Matrix4x4Field(string.Empty, value);
		}

		public Matrix4x4 Matrix4x4Field(string label, Matrix4x4 value)
		{
			return Matrix4x4Field(label, value, null);
		}

		public Matrix4x4 Matrix4x4Field(string label, string tooltip, Matrix4x4 value)
		{
			return Matrix4x4Field(label, tooltip, value, null);
		}

		public Matrix4x4 Matrix4x4Field(string label, Matrix4x4 value, Layout option)
		{
			return Matrix4x4Field(label, string.Empty, value, option);
		}

		public Matrix4x4 Matrix4x4Field(string label, string tooltip, Matrix4x4 value, Layout option)
		{
			return Matrix4x4Field(GetContent(label, tooltip), value, option);
		}

		public Matrix4x4 Matrix4x4Field(GUIContent content, Matrix4x4 value, Layout option)
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
						INTERNAL_Matrix4x4(ref value);
					}
				}
			}
			else
			{
				using (Horizontal())
				{
					Prefix(content.text);
					INTERNAL_Matrix4x4(ref value);
				}
			}

			return value;
		}

		private void INTERNAL_Matrix4x4(ref Matrix4x4 value)
		{
			using (LabelWidth(28f))
			{
				using (Vertical())
				{
					// Row 0
					using (Horizontal())
					{
						value.m00 = FloatField("M00", value.m00);
						Space(2f);
						value.m01 = FloatField("M01", value.m01);
						Space(2f);
						value.m02 = FloatField("M02", value.m02);
						Space(2f);
						value.m03 = FloatField("M03", value.m03);
					}

					// Row 1
					using (Horizontal())
					{
						value.m10 = FloatField("M10", value.m10);
						Space(2f);
						value.m11 = FloatField("M11", value.m11);
						Space(2f);
						value.m12 = FloatField("M12", value.m12);
						Space(2f);
						value.m13 = FloatField("M13", value.m13);
					}

					// Row 2
					using (Horizontal())
					{
						value.m20 = FloatField("M20", value.m20);
						Space(2f);
						value.m21 = FloatField("M21", value.m21);
						Space(2f);
						value.m22 = FloatField("M22", value.m22);
						Space(2f);
						value.m23 = FloatField("M23", value.m23);
					}

					// Row 3
					using (Horizontal())
					{
						value.m30 = FloatField("M30", value.m30);
						Space(2f);
						value.m31 = FloatField("M31", value.m31);
						Space(2f);
						value.m32 = FloatField("M32", value.m32);
						Space(2f);
						value.m33 = FloatField("M33", value.m33);
					}
				}
			}
		}
	}
}
