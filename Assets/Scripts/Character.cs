using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character : MonoBehaviour, IHitable {

    public Transform throwableSpawnPoint;
    public float cooldownRemaining;

    [SerializeField] protected Throwable defaultthrowable;
	[SerializeField] protected AbilityIcon[] powerupButtons;
	[SerializeField] protected ThrowCooldownUI cooldownMeter;
	[SerializeField] protected AngerMeterUI angerMeter;

	[SerializeField] protected int maxAnger;
	[SerializeField] protected float cooldown;


	public Throwable ActiveThrowable { get { return currentThrowable; } }


	protected Throwable currentThrowable;
	protected int anger;
	protected float startCooldown;

	protected List<Powerup> powerUps;

	public void Awake() {
		powerUps = new List<Powerup>();
		currentThrowable = defaultthrowable;
		anger = 0;
		cooldownRemaining = 0;
		startCooldown = cooldown;
		SetAnger(0);
	}

	public void Update() {
		cooldownRemaining -= Time.deltaTime;
		if (cooldownMeter != null) {
			cooldownMeter.Set(1f - Mathf.Clamp(cooldownRemaining, 0, startCooldown) / startCooldown);
		}
	}


	public void Hit(Throwable throwable) {
		SetAnger(anger + throwable.damage);

		Destroy(throwable.gameObject);
    }

	public void Heal(int amount) {
		SetAnger(anger - amount);
	}

    public void Throw(Vector2 force) {
		if (cooldownRemaining > 0) {
			return;
		}

        Throwable spawnedThrowable = Throwable.Instantiate<Throwable>(currentThrowable);
        spawnedThrowable.transform.position = throwableSpawnPoint.position;
		spawnedThrowable.transform.forward = force;
        spawnedThrowable.Throw(force, this);
		cooldownRemaining = cooldown;
		startCooldown = cooldown;
    }

	public void AddPowerup(Powerup powerup) {
		powerUps.Add(powerup);
		if (powerUps.Count > powerupButtons.Length) {
			powerUps.RemoveAt(0);
		}

		UpdatePowerupIcons();
		// TODO: update abilities UI
	}

	public void SetThrowable(Throwable throwable) {
		this.currentThrowable = throwable ?? defaultthrowable;
	}

	public void SetSingleCooldown(float cooldown) {
		cooldownRemaining = cooldown;
		startCooldown = cooldown;
	}




	protected void UpdatePowerupIcons() {
		for (int i = 0; i < powerupButtons.Length; i++) {
			int index = i;
			powerupButtons[i].Set(i < powerUps.Count ? powerUps[i]?.Sprite : null, () => ActivatePowerup(index));
		}
	}

	protected void ActivatePowerup(int index) {
		if (index >= powerUps.Count) {
			return;
		}

		powerUps[index].ActivatePowerup(this);
		powerUps.RemoveAt(index);
		UpdatePowerupIcons();
	}

	protected void SetAnger(int anger) {
		this.anger = Mathf.Clamp(anger, 0, maxAnger);
		if (angerMeter != null) {
			angerMeter.Set((float)anger / maxAnger);
		}

		if (anger >= maxAnger) {
			Debug.Log("Game over. Loser: " + name);
		}
	}
}
