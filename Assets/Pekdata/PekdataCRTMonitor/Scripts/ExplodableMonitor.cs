using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableMonitor : MonoBehaviour
{
    [SerializeField]
    private GameObject screenExplosionParticleSystem;
    [SerializeField]
    private GameObject screenOff;
    [SerializeField]
    private GameObject screenOn;
    [SerializeField]
    private GameObject shards;
    private bool broken;

    private void Start()
    {
        // Ensure screen starts in an "on" state
        screenOn.SetActive(true);
        screenOff.SetActive(false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && !broken)
        {
            broken = true;
            StartCoroutine(FlickerScreen());
        }
    }

    IEnumerator FlickerScreen()
    {
        int flickerCount = 5;
        float flickerDuration = 0.1f;

        for (int i = 0; i < flickerCount; i++)
        {
            screenOn.SetActive(!screenOn.activeSelf);
            screenOff.SetActive(!screenOff.activeSelf);
            yield return new WaitForSeconds(flickerDuration);
        }

        //// After flickering, handle breaking the screen
        //screenOff.SetActive(false);
        //screenOn.SetActive(false);
        //shards.SetActive(true);
        //Rigidbody[] shardRBs = GetComponentsInChildren<Rigidbody>();
        //screenExplosionParticleSystem.SetActive(true);

        //foreach (Rigidbody shardRB in shardRBs)
        //{
        //    float randomForce = Random.Range(1, 5);
        //    float randomRotationX = Random.Range(-20, 20);
        //    float randomRotationY = Random.Range(-20, 20);
        //    float randomRotationZ = Random.Range(-20, 20);
        //    shardRB.transform.Rotate(randomRotationX, randomRotationY, randomRotationZ);
        //    shardRB.AddRelativeForce(Vector3.forward * randomForce, ForceMode.Impulse);
        }
    }
