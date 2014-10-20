using UnityEngine;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class MinMax2D {

	public float MinX = 0.0f;
	public float MaxX = 0.0f;
	public float MinY = 0.0f;
	public float MaxY = 0.0f;

	public void set(float minX, float maxX, float minY, float maxY) {
		this.MinX = minX;
		this.MaxX = maxX;
		this.MinY = minY;
		this.MaxY = maxY;
	}
}
