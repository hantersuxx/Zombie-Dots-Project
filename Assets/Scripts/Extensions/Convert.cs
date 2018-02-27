using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static partial class Extensions
    {
        public static decimal ToDecimal(float number, int precision)
        {
            return Math.Round((decimal)number, precision);
        }
    }
}
