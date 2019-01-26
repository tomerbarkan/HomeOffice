using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    public AudioSource loopSound;
    public AudioSource OffSound;
    public Collider fanTrigger;
    public Animator animator;

    public float force;

    private void Start()
    {
        animator.SetBool("Active",false);
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Triggered!!!");
       
        other.attachedRigidbody.AddForce(transform.forward * 1,ForceMode.Impulse);
    }

    public void Activate()
    {
        animator.SetBool("Active", true);
        loopSound.Play();
        fanTrigger.enabled = true;
    }

    public void Deactivate()
    {
        animator.SetBool("Active", false);
        loopSound.Stop();
        OffSound.Play();
        fanTrigger.enabled = false;
    }
}
