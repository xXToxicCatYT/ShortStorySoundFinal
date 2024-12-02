using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    // vars
    public Animator anim;

    public Dummy dummy;

    public float counter = 1f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        counter += Time.deltaTime;

        if (counter > 0.7)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.swordSwing, this.transform.position);
                dummy.Enable();
                anim.SetBool("Attacking", true);
                gameObject.GetComponent<MeshCollider>().enabled = true;
                counter = 0f;
            }
            else
            {
                anim.SetBool("Attacking", false);
                gameObject.GetComponent<MeshCollider>().enabled = false;
            }
        }
    }
}