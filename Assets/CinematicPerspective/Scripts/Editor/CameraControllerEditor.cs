using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CinematicPerspective;

namespace CinematicPerspectiveEditor
{
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerEditor : Editor
    {
        CameraController script
        {
            get
            {
                return (CameraController)target;
            }
        }

        


        public override void OnInspectorGUI()
        {
            GUILayout.Label(AssetPreview.GetAssetPreview(CinematicControllerEditor.logo));
            EditorGUILayout.HelpBox("Use the root to edit options of this Script", MessageType.Warning);            
        }

        
    }
}
