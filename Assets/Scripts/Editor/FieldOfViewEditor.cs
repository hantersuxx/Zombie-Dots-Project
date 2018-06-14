using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        var fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.up, 360, fow.ViewRange);

        Vector3 viewAngleA = fow.transform.DirectionFromAngle(-fow.ViewAngle / 2, false);
        Vector3 viewAngleB = fow.transform.DirectionFromAngle(fow.ViewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.ViewRange);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.ViewRange);

        Handles.color = Color.red;
        foreach (var visibleTarget in fow.VisibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }
}
#endif