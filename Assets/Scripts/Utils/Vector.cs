using EpPathFinding.cs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static partial class Extensions
{
    public static Vector3 GetClosestPosition(Vector3 position, IEnumerable<Vector3> positions) => positions
        .OrderBy(p => (p - position).sqrMagnitude)
        .FirstOrDefault();

    public static KeyValuePair<Vector3, string> GetClosestTile(Vector3 position, Dictionary<Vector3, string> grid) => grid
        .OrderBy(t => (t.Key - position).sqrMagnitude)
        .FirstOrDefault(t => t.Value != SortingLayers.Obstruction);

    public static KeyValuePair<Vector3, EpPathFinding.cs.Node> GetClosestPosition(this Dictionary<Vector3, EpPathFinding.cs.Node> grid, Vector3 position) => grid
    .OrderBy(n => (n.Key - position).sqrMagnitude)
    .FirstOrDefault(n => n.Value.walkable);

    public static Vector3 DirectionFromAngle(this Transform transform, float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public static IEnumerator LerpLocalScale(this Transform transform, Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * 2f;
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
