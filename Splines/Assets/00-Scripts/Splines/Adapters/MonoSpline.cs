using UnityEngine;

public class MonoSpline : MonoBehaviour {
    [SerializeField] private Spline spline;
    
#if UNITY_EDITOR
    [SerializeField] private float _gizmoRadius = 0.05f;
    [SerializeField] private Color _color = Color.hotPink;
    [SerializeField] private Color _bezierColor = Color.softGreen;
    
    private bool _splineOutdated = true;
    private uint _splineLenghtCache = 0;
#endif
    
#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (spline == null)
            return;
        
        for (int i = 0; i < spline.Count; i++) {
            SplineGizmos.DrawVertexGizmo(spline, i, _color, _bezierColor);
            if (i > 0) {
                SplineGizmos.DrawConnectionGizmo(spline, i - 1, i, _color);
            }
        }
    }

    private void OnValidate() {
        if (spline == null)
            return;
        
        // invalidate spline whenever its points change.
        _splineOutdated = _splineLenghtCache != spline.Count;
        if (_splineOutdated) {
            spline.Invalidate();
        }
    }
#endif
}
