using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerupCollectible : MonoBehaviour, IHitable
{

    public AudioSource[] pickupSources;

	[SerializeField] public Powerup powerup;

    public void Hit(Throwable throwable) {
		throwable.thrower.AddPowerup(powerup);
        AudioManager.Instance.PlayAudioOneShot(pickupSources[Random.Range(0, pickupSources.Length)].clip);
      //  pickupSources[Random.Range(0, pickupSources.Length)].Play();
		Destroy(gameObject);
    }
}
