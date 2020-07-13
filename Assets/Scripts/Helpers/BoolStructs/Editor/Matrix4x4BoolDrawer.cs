using UnityEditor;
using UnityEngine;

namespace Helpers.BoolStructs.Editor
{
    [CustomPropertyDrawer(typeof(Matrix4x4Bool))]
    public class Matrix4x4BoolDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 150;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var prop = property.FindPropertyRelative($"m{i}{j}");
                    if(GUI.Button(new Rect(25 + position.x + i * 25, 25 + position.y + j * 25, 25, 25), prop.boolValue ? "X" : " "))
                    {
                        prop.boolValue = !prop.boolValue;
                    }

                }
            }

            EditorGUI.EndProperty();
        }
    }
}
