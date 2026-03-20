using System;
using UnityEngine;

[Serializable]
public struct Vertex {
	public Vector3 position;
	public float bezier;

	public Vertex(Vector3 position, float bezier) {
		this.position = position;
		this.bezier = bezier;
	}

#region OPERATORS

	public static Vector3 operator +(Vertex lhs, Vertex rhs) {
		return lhs.position + rhs.position;
	}
	
	public static Vector3 operator +(Vector3 lhs, Vertex rhs) {
		return lhs + rhs.position;
	}
	
	public static Vector3 operator +(Vertex lhs, Vector3 rhs) {
		return lhs.position + rhs;
	}
	
	public static Vector3 operator -(Vertex lhs, Vertex rhs) {
		return lhs.position - rhs.position;
	}
	
	public static Vector3 operator -(Vertex lhs, Vector3 rhs) {
		return lhs.position - rhs;
	}
	
	public static Vector3 operator -(Vector3 lhs, Vertex rhs) {
		return lhs - rhs.position;
	}

#endregion
}
