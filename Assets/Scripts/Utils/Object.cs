﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static partial class Extensions
{
    public static void SetValue<TValue>(this object @object, string propertyName, TValue value)
    {
        var property = @object.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property?.CanWrite == true)
            property.SetValue(@object, value, null);
    }

    public static void SetValue<TObject>(this TObject @object, Action<TObject> assignment)
    {
        assignment(@object);
    }

    public static bool IsNull<T>(this T value)
    {
        return value == null || value.Equals(default(T));
    }
}