using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSkeletonController : MonoBehaviour
{
    Animator animator;
    SkinnedMeshRenderer skinnedMesh;
    void Start()
    {
        animator = GetComponent<Animator>();

        // Initially deactivate after 60 seconds
        Invoke("DeactivateGameObject", 1f);

        // Reactivate after 180 seconds from start (120 seconds after deactivation)
        Invoke("ReactivateGameObject", 40f);

        // Destroy 120 seconds after reactivation (300 seconds from start)
        Invoke("DestroyGameObject", 120f);
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.L))
        //    animator.SetTrigger("lying");
        //else if (Input.GetKeyDown(KeyCode.Space))
        //    animator.SetTrigger("jump");
        //else if (Input.GetKeyDown(KeyCode.K))
        //    animator.SetTrigger("knockdown");
        //else if (Input.GetKeyDown(KeyCode.Mouse0))
        //    animator.SetTrigger("punch_L");
        //else if (Input.GetKeyDown(KeyCode.Mouse1))
        //    animator.SetTrigger("punch_R");

        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        //if (Input.GetKey(KeyCode.LeftShift))
        //    animator.SetBool("running", true);
        //else
        //    animator.SetBool("running", false);
        //if (Input.GetKey(KeyCode.LeftControl))
        //    animator.SetBool("sidefix", true);
        //else
        //    animator.SetBool("sidefix", false);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void ReactivateGameObject()
    {
        gameObject.SetActive(true);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

