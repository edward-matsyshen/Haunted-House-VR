using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


[RequireComponent(typeof(VideoPlayer))]
public class AutoResizeVideo : MonoBehaviour
{
    public GameObject targetGameObject; // The target GameObject whose dimensions to match

    void Start()
    {
        var videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.prepareCompleted += PrepareCompleted;
        videoPlayer.Prepare();
    }

    void PrepareCompleted(VideoPlayer vp)
    {
        if (targetGameObject != null)
        {
            MeshRenderer meshRenderer = targetGameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                Bounds bounds = meshRenderer.bounds;
                float width = bounds.size.x;
                float height = bounds.size.y;

                transform.localScale = new Vector3(width, height, 1); // Adjust z as necessary
            }
        }
    }
}