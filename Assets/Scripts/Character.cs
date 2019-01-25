using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character : MonoBehaviour, IHitable
{
	public class PowerupButton : MonoBehaviour {
		public void Set(Sprite sprite, UnityAction action) {

		}
	}

    [SerializeField] protected Throwable defaultthrowable;
    [SerializeField] protected Transform throwableSpawnPoint;
	[SerializeField] protected PowerupButton[] powerupButtons;
	[SerializeField] protected int maxAnger;

	protected Throwable currentThrowable;
	protected int anger;

	protected List<Powerup> powerUps;

	public void Awake() {
		powerUps = new List<Powerup>();
		currentThrowable = defaultthrowable;
		anger = 0;
	}

	public void Hit(Throwable throwable) {
		anger += throwable.damage;
		Debug.Log("Anger: " + anger);

		if (anger >= maxAnger) {
			Debug.Log("Game over. Loser: " + name);
		}

		Destroy(throwable.gameObject);
    }

    public void Throw(Vector2 force) {
        Throwable spawnedThrowable = Throwable.Instantiate<Throwable>(currentThrowable);
        spawnedThrowable.transform.position = throwableSpawnPoint.position;
		spawnedThrowable.transform.forward = force;
        spawnedThrowable.Throw(force, this);
    }

	public void AddPowerup(Powerup powerup) {
		powerUps.Add(powerup);
		if (powerUps.Count > ConfigManager.instance.maxPowerups) {
			powerUps.RemoveAt(0);
		}

		// TODO: update abilities UI
	}

	public void SetThrowable(Throwable throwable) {
		this.currentThrowable = throwable ?? defaultthrowable;
	}
}
