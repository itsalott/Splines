using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spline {

    public Vertex this[int index] {
        get { return _points[index]; }
    }

    public int Count {
        get { return _points.Count; }
    }

    public float Length {
        get { return _length?? CalculateLenght(); }
    }

    [SerializeField] private List<Vertex> _points;
    private float? _length;

    public Spline(int count = 0) {
        if (count > 0)
            _points = new List<Vertex>(count);
        else
            _points = new List<Vertex>();

        CalculateLenght();
    }
    
    /// <summary>
    /// Add new point to the end of the spline.
    /// </summary>
    public void Add(Vector3 point) {
        _points.Add(new Vertex(point, 0));

        _length = null;
    }
    
    /// <summary>
    /// Remove point at index.
    /// </summary>
    public void Remove(int index) {
        _points.RemoveAt(index);

        _length = null;
    }
    
    /// <summary>
    /// Retrieve position on spline, at t
    /// </summary>
    /// <param name="t">Value that is used to interpolate the spline.</param>
    public Vector3 GetPosition(float t) {
        // add spline point class to speed this process up?
        float goalLenght = Length * Mathf.Clamp01(t);
        int i = 1;
        for (; i < _points.Count; i++) {
            goalLenght -= (_points[i].position - _points[i - 1].position).magnitude;
            if (goalLenght <= 0) {
                // position is between point i-1 and i
                break;
            }
        }

        goalLenght = Mathf.Abs(goalLenght);
        
        // Prevent division by 0 errors
        if (goalLenght <= 1e-5)
            return _points[i - 1].position;
        
        // Technically Vector3.Lerp, but because we need the magnitude of FromTo,
        // but manual calculation is faster (no need to calculate FromTo twice).
        Vector3 fromTo = _points[i].position - _points[i].position;
        float t2 = goalLenght / fromTo.magnitude;
        
        return _points[i - 1].position + fromTo * t2;
    }

#region EDITOR_FUNCTIONS

    /// <summary>
    /// Notifies spline of changes, and forces recalculation of values like Length.
    /// </summary>
    public void Invalidate() {
        _length = null;
    }

#endregion
    
    private float CalculateLenght() {
        _length = 0f;
        for (int i = 1; i < _points.Count; i++) {
            _length += (_points[i].position - _points[i - 1].position).magnitude;
        }

        return _length.Value;
    }
}
