using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Throwable : MonoBehaviour
{
    [SerializeField] protected Rigidbody throwableRigidbody;
	public int damage;

	public Character thrower;

    public virtual void Throw(Vector2 force, Character thrower) {
		throwableRigidbody.velocity = force;
		this.thrower = thrower;
    }

	public void OnTriggerEnter(Collider other) {
		IHitable hittable = other.GetComponent<IHitable>();
		if (hittable != null && (object)hittable != thrower) {
			hittable.Hit(this);
		}
	}
}
