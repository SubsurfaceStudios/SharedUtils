using UnityEngine;

namespace SubsurfaceStudios.Attributes.Editor
{
    using UnityEditor;

    [CustomPropertyDrawer(typeof(InspectorReadOnly))]
    public class InspectorReadOnlyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}