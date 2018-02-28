using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static partial class Extensions
    {
        public static Vector3 GetClosestPosition(Vector3 position, IEnumerable<Vector3> positions) => positions.OrderBy(p => (p - position).sqrMagnitude).FirstOrDefault();
        //TODO:add get closest ground tile
    }
}
