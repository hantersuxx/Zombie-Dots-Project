using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static partial class Extensions
{
    public static Vector3 GetClosestPosition(Vector3 position, IEnumerable<Vector3> positions) => positions.OrderBy(p => (p - position).sqrMagnitude).FirstOrDefault();
    //TODO:add get closest ground tile
    public static Vector3 DirectionFromAngle(this Transform transform, float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
