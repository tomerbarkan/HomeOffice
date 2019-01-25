using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Throwable : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody throwableRigidbody;
    // Start is called before the first frame update
    protected  virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void Throw(float angle, float force)
    {

    }

    
}
