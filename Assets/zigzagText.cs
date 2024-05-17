using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class zigzagText : MonoBehaviour
{
    TMP_Text textMesh;

    List<int> wordIndexes;
    List<int> wordLengths;

    public Gradient rainbow;
    public float fadeDuration = 5f; // Total duration for the fade out in seconds

    private float[] fadeTimers; // Timers for each word's fade

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();

        wordIndexes = new List<int> { 0 };
        wordLengths = new List<int>();

        string s = textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);

        // Initialize fade timers
        fadeTimers = new float[wordIndexes.Count];
        for (int i = 0; i < fadeTimers.Length; i++)
        {
            fadeTimers[i] = fadeDuration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();

        TMP_TextInfo textInfo = textMesh.textInfo;
        TMP_MeshInfo[] meshInfos = textInfo.CopyMeshInfoVertexData();

        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            if (fadeTimers[w] > 0)
            {
                fadeTimers[w] -= Time.deltaTime;
            }

            float alpha = Mathf.Clamp01(fadeTimers[w] / fadeDuration);

            for (int i = 0; i < wordLengths[w]; i++)
            {
                if (wordIndex + i < textInfo.characterCount)
                {
                    TMP_CharacterInfo c = textInfo.characterInfo[wordIndex + i];
                    if (c.isVisible)
                    {
                        int materialIndex = c.materialReferenceIndex;
                        int vertexIndex = c.vertexIndex;

                        Vector3[] vertices = meshInfos[materialIndex].vertices;
                        Color32[] colors = meshInfos[materialIndex].colors32;

                        // Apply the fading alpha to the colors of each vertex
                        Color32 fadeColor = new Color32(
                            (byte)(rainbow.Evaluate((float)w / wordIndexes.Count).r * 255),
                            (byte)(rainbow.Evaluate((float)w / wordIndexes.Count).g * 255),
                            (byte)(rainbow.Evaluate((float)w / wordIndexes.Count).b * 255),
                            (byte)(alpha * 255));

                        colors[vertexIndex] = fadeColor;
                        colors[vertexIndex + 1] = fadeColor;
                        colors[vertexIndex + 2] = fadeColor;
                        colors[vertexIndex + 3] = fadeColor;
                    }
                }
            }
        }

        // Update the mesh with the modified vertices and colors
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = meshInfos[i].vertices;
            textInfo.meshInfo[i].mesh.colors32 = meshInfos[i].colors32;
            textMesh.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}