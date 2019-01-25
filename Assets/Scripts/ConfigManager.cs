using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
	public float maxThrowSpeed = 10f;
	public float boostRate = 4f;
	public float enemyHitChance = 0.6f;
	public float enemyBoostMin = 5f;
	public float enemyBoostMax = 14f;

	public static ConfigManager instance;

    void Awake() {
		instance = this;
    }
}
