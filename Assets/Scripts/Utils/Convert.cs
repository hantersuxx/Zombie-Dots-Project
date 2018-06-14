using System;

public static partial class Extensions
{
    public static decimal ToDecimal(float number, int precision)
    {
        return Math.Round((decimal)number, precision);
    }
}
