using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Throwable : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody throwableRigidbody;

    public void Throw(Vector2 force) {
		throwableRigidbody.velocity = force;
    }
    
}
