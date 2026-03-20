using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class SplineGizmos {
	private const float RADIUS = 0.05f;
	private const float BEZIER_SCALAR = 0.5f;
	
	public static void DrawVertexGizmo(Spline s, int i, Color vertexCol, Color bezierColor) {
		Gizmos.color = vertexCol;
		Gizmos.DrawSphere(s[i].position, RADIUS);

		if (s[i].bezier > 1e-5) {
			Gizmos.color = bezierColor;
			Vector3 bezierDir = s.GetBezierDirection(i);
			
			Vector3 start = (i == 0)
				? s[i].position 
				: s[i] - bezierDir * s[i].bezier * BEZIER_SCALAR;
			Vector3 end = (i == s.Count - 1)
				? s[i].position
				: s[i] + bezierDir * s[i].bezier * BEZIER_SCALAR;
			
			if (i != 0)
				Gizmos.DrawSphere(start, RADIUS * BEZIER_SCALAR);
			if (i != s.Count - 1)
				Gizmos.DrawSphere(end, RADIUS * BEZIER_SCALAR);
			
			Gizmos.DrawLine(start, end);
		}
	}

	public static void DrawConnectionGizmo(Spline s, int start, int stop, Color color, int segmentation = 6) {
		Gizmos.color = color;

		float tStep = 1f / segmentation;
		Vector3 prev, cur;
		prev = s[start].position;
		
		for (int i = 1; i < segmentation; i++) {
			cur = s.GetBezierPos(start, stop, tStep * i);
			
			Gizmos.DrawLine(prev, cur);
			prev = cur;
		}
	}
}
