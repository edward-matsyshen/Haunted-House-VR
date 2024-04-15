using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightFlickering))]
public class LightFlickerInspector : Editor
{
    SerializedProperty onStart,
    lightSource,

    fadeEffect,
    fadeTime,
    fadeTo,

    randomizeFlickerings,
    minRandomizeTime,
    maxRandomizeTime,

    flickerings,
    loop,

    lightings,

    playAudio,
    buzzAudio,
    randomizeAudioPitch,
    pitchRandomizer,
    
    changeMaterial,
    bulbObject,
    newMaterial;


    void OnEnable()
    {
        onStart = serializedObject.FindProperty("onStart");
        lightSource = serializedObject.FindProperty("lightSource");

        fadeEffect = serializedObject.FindProperty("fadeEffect");
        fadeTime = serializedObject.FindProperty("fadeTime");
        fadeTo = serializedObject.FindProperty("fadeTo");

        randomizeFlickerings = serializedObject.FindProperty("randomizeFlickerings");
        minRandomizeTime = serializedObject.FindProperty("minRandomizeTime");
        maxRandomizeTime = serializedObject.FindProperty("maxRandomizeTime");
        
        flickerings = serializedObject.FindProperty("flickerings");
        loop = serializedObject.FindProperty("loop");
        
        lightings = serializedObject.FindProperty("lightings");
        
        playAudio = serializedObject.FindProperty("playAudio");
        buzzAudio = serializedObject.FindProperty("buzzAudio");
        randomizeAudioPitch = serializedObject.FindProperty("randomizeAudioPitch");
        pitchRandomizer = serializedObject.FindProperty("pitchRandomizer");
        
        changeMaterial = serializedObject.FindProperty("changeMaterial");
        bulbObject = serializedObject.FindProperty("bulbObject");
        newMaterial = serializedObject.FindProperty("newMaterial");
    }

    public override void OnInspectorGUI()
    {
        var button = GUILayout.Button("Click for more tools");
        if (button) Application.OpenURL("https://assetstore.unity.com/publishers/39163");
        EditorGUILayout.Space(5);

        LightFlickering script = (LightFlickering) target;

        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(onStart);
        EditorGUILayout.PropertyField(lightSource);
        EditorGUILayout.Space(10);


        EditorGUILayout.LabelField("Fade Effect", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(fadeEffect);
        if (script.fadeEffect) {
            EditorGUILayout.PropertyField(fadeTime);
            EditorGUILayout.PropertyField(fadeTo);
        }
        EditorGUILayout.Space(10);


        EditorGUILayout.LabelField("Randomize", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(randomizeFlickerings);
        if (script.randomizeFlickerings) {
            EditorGUILayout.PropertyField(minRandomizeTime);
            EditorGUILayout.PropertyField(maxRandomizeTime);
        }
        EditorGUILayout.Space(10);

        
        if (!script.randomizeFlickerings) {
            EditorGUILayout.LabelField("Manual Flickering", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(flickerings);
            EditorGUILayout.PropertyField(loop);
            EditorGUILayout.Space(10);
        }


        EditorGUILayout.LabelField("Lighting", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(lightings);
        EditorGUILayout.Space(10);


        EditorGUILayout.LabelField("Audio", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(playAudio);
        if (script.playAudio) {
            EditorGUILayout.PropertyField(buzzAudio);
            EditorGUILayout.PropertyField(randomizeAudioPitch);
            if (script.randomizeAudioPitch) {
                EditorGUILayout.PropertyField(pitchRandomizer);
            }
        }
        EditorGUILayout.Space(10);


        EditorGUILayout.LabelField("Change Material", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(changeMaterial);
        if (script.changeMaterial) {
            EditorGUILayout.PropertyField(bulbObject);
            EditorGUILayout.PropertyField(newMaterial);
        }

        
        serializedObject.ApplyModifiedProperties();
    }
}