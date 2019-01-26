using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character : MonoBehaviour, IHitable {

    public Transform throwableSpawnPoint;
    public Transform fanSpawnPoint;
    public Transform bossSpawnPoint;
    public Transform aimToPoint;

    public float cooldownRemaining;

    public bool canHeal = true;
    [SerializeField] protected ParticleSystem fireParticles;
   
    [SerializeField] protected Throwable defaultthrowable;
	[SerializeField] protected AbilityIcon[] powerupButtons;
	[SerializeField] protected ThrowCooldownUI cooldownMeter;
	[SerializeField] protected AngerMeterUI angerMeter;

    [SerializeField] protected float angeyDecayPerSecond;
	[SerializeField] protected int maxAnger;

    [SerializeField] protected AudioSource fireScreamAudioSource;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float additionalCooldown;


	public Throwable ActiveThrowable { get { return currentThrowable; } }


	protected Throwable currentThrowable;
	protected float anger;
	protected float startCooldown;

	protected List<Powerup> powerUps;

    bool onFire = false;
    float timeOnFireRemaining = 0;
    

	public void Awake() {
		powerUps = new List<Powerup>();
		currentThrowable = defaultthrowable;
	}

	public void Start() {
		anger = 0;
		cooldownRemaining = 0;
		startCooldown = ConfigManager.instance.cooldown + additionalCooldown;
		SetAnger(0);		
	}

	public void Update() {
		cooldownRemaining -= Time.deltaTime;
		if (cooldownMeter != null) {
			cooldownMeter.Set(1f - Mathf.Clamp(cooldownRemaining, 0, startCooldown) / startCooldown);
		}

		Heal(angeyDecayPerSecond * Time.deltaTime);
       
        if(onFire)
        {
            timeOnFireRemaining -= Time.deltaTime;
            if (timeOnFireRemaining <= 0)
            {
                onFire = false;
                fireParticles.Stop();
                canHeal = true;
                fireScreamAudioSource.Stop();
            }
        }
	}


	public void Hit(Throwable throwable) {
		SetAnger(anger + throwable.damage);
        throwable.OnHitCharacter(this);
		Destroy(throwable.gameObject);
    }

	public void Heal(float amount) {
		if (canHeal) {
			SetAnger(anger - amount);
		}
	}

    public void Throw(Vector2 force) {
		if (cooldownRemaining > 0) {
			return;
		}

        Throwable spawnedThrowable = Throwable.Instantiate<Throwable>(currentThrowable);
        spawnedThrowable.transform.position = throwableSpawnPoint.position;
		spawnedThrowable.transform.forward = force;
        spawnedThrowable.Throw(force, this);
		startCooldown = ConfigManager.instance.cooldown + additionalCooldown;
		cooldownRemaining = startCooldown;

        animator.SetTrigger("Throw");
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
		if (cooldownRemaining > cooldown) {
			return;
		}

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

	protected void SetAnger(float anger) {
		this.anger = Mathf.Clamp(anger, 0, maxAnger);
		if (angerMeter != null) {
			angerMeter.Set((float)anger / maxAnger);
		}

		if (anger >= maxAnger) {
			Debug.Log("Game over. Loser: " + name);
		}
	}

    public void SetOnFire(float time)
    {
        fireParticles.Play();
        timeOnFireRemaining = time;
        canHeal = false;
        onFire = true;
        fireScreamAudioSource.Play();
    }

   
}
