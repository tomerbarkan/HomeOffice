using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpawner 
{
	protected Transform[] spawnPositions;
	protected PowerupCollectible[] spawns;
	protected PowerupCollectible[] powerupOptions;

	protected float timePassed = 0;

    public BoostSpawner(Transform spawnsParent, PowerupCollectible[] powerupOptions) {
		this.spawnPositions = new Transform[spawnsParent.childCount];
		this.spawns = new PowerupCollectible[spawnsParent.childCount];
		this.powerupOptions = powerupOptions;

		for (int i = 0; i < spawnsParent.childCount; i++) {
			spawnPositions[i] = spawnsParent.GetChild(i);
		}
    }

	public void Simulate() {
		if (timePassed >= ConfigManager.instance.boostRate) {
			timePassed = 0;
			SpawnBoost();
		}
		timePassed += Time.deltaTime;
	}

	public void SpawnBoost() {
		List<int> freeSpawns = new List<int>();
		for (int i = 0; i < spawnPositions.Length; i++) {
			if (spawns[i] == null || spawns[i].gameObject == null) {
				freeSpawns.Add(i);
			}
		}

		if (freeSpawns.Count == 0) {
			return;
		}

		int spawnPositionIndex = freeSpawns[Random.Range(0, freeSpawns.Count)];
		PowerupCollectible powerup = powerupOptions[Random.Range(0, powerupOptions.Length)];

		PowerupCollectible instance = GameObject.Instantiate(powerup, spawnPositions[spawnPositionIndex], true);
		instance.transform.localPosition = Vector3.zero;
		spawns[spawnPositionIndex] = instance;
	}
}
