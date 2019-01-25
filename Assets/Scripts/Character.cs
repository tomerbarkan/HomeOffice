using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character : MonoBehaviour, IHitable
{
    [SerializeField] protected Throwable currentThrowable;
    [SerializeField] protected Transform throwableSpawnPoint;

    public void Hit(Character thrower) {
    }

    public void Throw(Vector2 force) {
        Throwable spawnedThrowable = Throwable.Instantiate<Throwable>(currentThrowable);
        spawnedThrowable.transform.position = throwableSpawnPoint.position;
		spawnedThrowable.transform.forward = force;
        spawnedThrowable.Throw(force);
    }
}
