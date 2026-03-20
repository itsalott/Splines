using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public static class SplineExtentions
{
	public static Vector3 GetBezierDirection(this Spline spline, int i) {
		// start and end position
		if (i == 0)
			return spline[1] - spline[0];
		if (i == spline.Count - 1)
			return spline[i] - spline[i - 1];

		Vector3 incoming = spline[i] - spline[i - 1];
		Vector3 outgoing = spline[i + 1] - spline[i];

		return (incoming + outgoing) * 0.5f;
	}

	public static (Vector3 inP, Vector3 outP) GetBezierPoints(this Spline s, int i) {
		Vector3 direction = s.GetBezierDirection(i);
		
		return (s[i] - direction * s[i].bezier,
		        s[i] + direction * s[i].bezier);
	}
	
	public static Vector3 GetBezierPos(this Spline s, int startI, int stopI, float t) {
		Vector3 start = s[startI].position;
		Vector3 startB = s.GetBezierPoints(startI).outP;
		
		Vector3 stop = s[stopI].position;
		Vector3 stopB = s.GetBezierPoints(stopI).inP;

		return GetCubicBezierPos(start, startB, stopB, stop, t);
	}
	
	private static Vector3 GetQuadraticBezierPoint(Vector3 b0, Vector3 b1, Vector3 b2, float t) {
		t = Mathf.Clamp01(t);
		float nT = 1f - t;
		
		return nT * (nT * b0 + t * b1) + t * (nT * b1 + t * b2);
	}
	
	private static Vector3 GetCubicBezierPos(Vector3 b0, Vector3 b1, Vector3 b2, Vector3 b3, float t) {
		t = Mathf.Clamp01(t);
		float nT = 1f - t;

		return nT * GetQuadraticBezierPoint(b0, b1, b2, t) + t * GetQuadraticBezierPoint(b1, b2, b3, t);
	}
}
