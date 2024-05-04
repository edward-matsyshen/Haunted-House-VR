using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextColorChanger : MonoBehaviour
{
    TMP_Text textMesh;

    List<int> wordIndexes;
    List<int> wordLengths;

    public Gradient rainbow;

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
            Vector3 offset = Wobble(Time.time + w);

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

                        colors[vertexIndex] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[vertexIndex].x * 0.001f, 1f));
                        colors[vertexIndex + 1] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[vertexIndex + 1].x * 0.001f, 1f));
                        colors[vertexIndex + 2] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[vertexIndex + 2].x * 0.001f, 1f));
                        colors[vertexIndex + 3] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[vertexIndex + 3].x * 0.001f, 1f));

                        vertices[vertexIndex] += offset;
                        vertices[vertexIndex + 1] += offset;
                        vertices[vertexIndex + 2] += offset;
                        vertices[vertexIndex + 3] += offset;
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

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 2.5f));
    }
}