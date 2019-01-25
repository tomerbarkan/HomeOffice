using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, IHitable
{
    public UnityEvent onHit { get; protected set; }

   

    [SerializeField]
    protected Throwable currentThrowable;
    [SerializeField]
    protected Transform throwableSpawnPoint;

    public void Hit()
    {
        onHit.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// Update is called once per frame
    //void Update()
    //{
    
    //}

    //bool CheckInputFire()
    //{
    //    return true;
    //}

    public void Throw(float angle, float force)
    {
        Throwable spawnedThrowable = Throwable.Instantiate<Throwable>(currentThrowable);
        spawnedThrowable.transform.position = throwableSpawnPoint.position;
        spawnedThrowable.Throw(angle, force);
    }
}
