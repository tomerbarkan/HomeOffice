using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Character : MonoBehaviour, IHitable {

    public Transform throwableSpawnPoint;
    public Transform fanSpawnPoint;
    public Transform bossSpawnPoint;
    public Transform aimToPoint;
	public Transform firedText;
    public GameObject joint;

    public Animator animator;
    public Animator chairAnimator;

    public Fan fan;

    public float cooldownRemaining;

    public bool canHeal = true;
    public bool canGetHit = true;
    public bool canShoot = true;

	public Image weaponIcon;


    [SerializeField] protected ParticleSystem fireParticles;
   
    [SerializeField] protected Throwable defaultthrowable;
	[SerializeField] protected AbilityIcon[] powerupButtons;
	[SerializeField] protected ThrowCooldownUI cooldownMeter;
	[SerializeField] protected AngerMeterUI angerMeter;
	public GameObject powerupsArea;

    [SerializeField] protected float angeyDecayPerSecond;
	[SerializeField] protected int maxAnger;

    public AudioSource[] hitsSources;
    [SerializeField] protected AudioSource fireScreamAudioSource;
  
    [SerializeField] protected float additionalCooldown;


	public Throwable ActiveThrowable { get { return currentThrowable; } }


	protected Throwable currentThrowable;
	protected float anger;
	protected float startCooldown;
	protected float overrideCooldown = -1;

	protected List<Powerup> powerUps;

    bool onFire = false;
    float timeOnFireRemaining = 0;
    

	public void Awake() {
		powerUps = new List<Powerup>();
		SetThrowable(defaultthrowable);
	}

	public void Start() {
		anger = 0;
		cooldownRemaining = 0;
		if (ConfigManager.instance != null) {
			startCooldown = ConfigManager.instance.cooldown + additionalCooldown;
		}
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
        if (!canGetHit)
            return;

        SetAnger(anger + throwable.damage);
        throwable.OnHitCharacter(this);
        animator.SetTrigger("Hit");

        hitsSources[Random.Range(0, hitsSources.Length)].Play();
		Destroy(throwable.gameObject);
    }

	public void Heal(float amount) {
		if (canHeal) {
			SetAnger(anger - amount);
		}
	}

    public void Throw(Vector2 force) {
		if (cooldownRemaining > 0 || !canShoot) {
			return;
		}

        Throwable spawnedThrowable = Throwable.Instantiate<Throwable>(currentThrowable);
        spawnedThrowable.transform.position = throwableSpawnPoint.position;
		spawnedThrowable.transform.forward = force;
        spawnedThrowable.Throw(force, this);
		startCooldown = (overrideCooldown < 0 ? ConfigManager.instance.cooldown : overrideCooldown) + additionalCooldown;
		cooldownRemaining = startCooldown;

      
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
		if (weaponIcon != null) {
			weaponIcon.sprite = throwable.sprite;
		}
	}

	public void SetSingleCooldown(float cooldown) {
		if (cooldownRemaining > cooldown) {
			return;
		}

		cooldownRemaining = cooldown;
		startCooldown = cooldown;
	}

    [ContextMenu("Try smoke weed animation")]
    public void StartSmokeWeedAnimation()
    {
        animator.SetTrigger("Smoke Weed");
        chairAnimator.SetTrigger("Smoke Weed");
        joint.SetActive(true);
    }

	public void SetOverrideCooldown(float cooldown) {
		this.overrideCooldown = cooldown;
	}

	public void CancelOverrideCooldown() {
		overrideCooldown = -1;
	}


	protected void UpdatePowerupIcons() {
		for (int i = 0; i < powerupButtons.Length; i++) {
			int index = i;
			powerupButtons[i].Set(i < powerUps.Count ? powerUps[i]?.Sprite : null, i < powerUps.Count ? powerUps[i]?.audioClip : null,() => ActivatePowerup(index));
		}
	}

	public void ActivatePowerup(int index) {
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
			GameManager.instance.GameOver(this);
		}
	}

    public void SetOnFire(float time)
    {
        if (!canGetHit)
            return;

        fireParticles.Play();
        timeOnFireRemaining = time;
        canHeal = false;
        onFire = true;
        fireScreamAudioSource.Play();

        animator.SetTrigger("Get Burn");
    }

   
    public void DeactivateJoint()
    {
        joint.SetActive(false);
    }
}
