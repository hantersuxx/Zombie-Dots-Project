
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(InterfaceHelper))]
public class InterfaceHelperDrawer : PropertyDrawer
{

    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        EditorGUI.BeginProperty(pos, label, prop);
        pos = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), label);
        EditorGUI.PropertyField(pos, prop.FindPropertyRelative("target"), GUIContent.none);
        EditorGUI.EndProperty();
    }
}