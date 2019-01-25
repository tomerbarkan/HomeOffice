using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    public float force;
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Triggered!!!");
        other.attachedRigidbody.AddForce(transform.forward * 1,ForceMode.Impulse);
    }
}
