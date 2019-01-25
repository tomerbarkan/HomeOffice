﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPowerUp : Powerup
{
    public float activeTime;
    public GameObject bossPrefab;

    public override void ActivatePowerup(Character activator)
    {
        activator.StartCoroutine(DelayCoroutine(activator, activeTime));
    }

    private IEnumerator DelayCoroutine(Character activator, float delay)
    {
        GameObject spawnedBoss = GameObject.Instantiate(bossPrefab, activator.bossSpawnPoint.position, activator.bossSpawnPoint.rotation);


        while (delay > 0)
        {


            delay -= Time.deltaTime;
            yield return null;
        }

        Destroy(spawnedBoss);
    }
}
