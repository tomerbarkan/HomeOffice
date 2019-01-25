using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
	public float maxThrowSpeed = 10f;
	public float boostRateMin = 5f;
	public float boostRateMax = 14f;
	public float enemyHitChance = 0.6f;
	public float enemyBoostMin = 5f;
	public float enemyBoostMax = 14f;
	public float cooldown = 1f;
	public float stageTime = 60f;
	public float maxSwipeRange = 300f;
	public float minSwipeRange = 15f;
	public float throwableLiveTime = 5f;

	public static ConfigManager instance;

    void Awake() {
		instance = this;
    }
}
