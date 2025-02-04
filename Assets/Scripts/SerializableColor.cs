using System;
using UnityEngine;

[Serializable]
public class SerializableColor
{
	public static SerializableColor white;

	public byte R { get; set; }

	public byte G { get; set; }

	public byte B { get; set; }

	static SerializableColor()
	{
		SerializableColor.white = new SerializableColor(1f, 1f, 1f);
	}

	public SerializableColor(Color color)
	{
		this.R = Convert.ToByte(color.r * 255f);
		this.G = Convert.ToByte(color.g * 255f);
		this.B = Convert.ToByte(color.b * 255f);
	}

	public SerializableColor(float r, float g, float b)
	{
		this.R = Convert.ToByte(r * 255f);
		this.G = Convert.ToByte(g * 255f);
		this.B = Convert.ToByte(b * 255f);
	}

	public Color ToUnityEngineColor()
	{
		return new Color((float)(int)this.R / 255f, (float)(int)this.G / 255f, (float)(int)this.B / 255f, 1f);
	}

	public override bool Equals(object obj)
	{
		SerializableColor serializableColor = obj as SerializableColor;
		if (serializableColor == (SerializableColor)null)
		{
			return false;
		}
		return this == serializableColor;
	}

	public static bool operator ==(SerializableColor a, SerializableColor b)
	{
		return a.R == b.R && (a.G == b.G & a.B == b.B);
	}

	public static bool operator !=(SerializableColor a, SerializableColor b)
	{
		return !(a == b);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}


