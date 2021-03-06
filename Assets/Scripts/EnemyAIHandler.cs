﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHandler 
{
    protected Character enemyCharacter;
    protected Character playerCharacter;
    protected Vector3 position;
    protected PowerupCollectible[] powerupOptions;

    float timeUntilNextBoost;

    bool shootingThisFrame = false;

    public EnemyAIHandler(Character enemy, Character player, Vector3 position, PowerupCollectible[] powerupOptions)
    {
        this.enemyCharacter = enemy;
        this.playerCharacter = player;
        this.position = position;
        this.powerupOptions = powerupOptions;
        timeUntilNextBoost = GetTimeUntilNextBoost();
		enemy.powerupsArea.SetActive(false);
    }

    
    public void Think()
    {
        if (enemyCharacter.cooldownRemaining <= 0.2f)
        {
            if (enemyCharacter.canShoot && !shootingThisFrame)
            {
                enemyCharacter.animator.SetTrigger("Throw");
                shootingThisFrame = true;
            }
            if (enemyCharacter.cooldownRemaining <= 0)
            {
                bool willHit = (Random.value <= ConfigManager.instance.enemyHitChance);
                Vector3 positionModifier = Vector3.zero;
                float timeModifier = 0;
                if (!willHit)
                {
                    //Debug.Log("Will not hit!");
                    positionModifier = Random.value <= 0.5f ? Vector3.right * Random.Range(5f, 10f) : Vector3.right * Random.Range(-5f, -10f);
                    timeModifier = Random.Range(-0.5f, 1f);
                }
                enemyCharacter.Throw(calculateBestThrowSpeed(enemyCharacter.throwableSpawnPoint.position,
                    playerCharacter.aimToPoint.position + positionModifier, 2 + timeModifier));
                shootingThisFrame = false;
            }
        }

        timeUntilNextBoost -= Time.deltaTime;
        if(timeUntilNextBoost <= 0)
        {
            PowerupCollectible powerup = powerupOptions[Random.Range(0, powerupOptions.Length)];
            powerup.powerup.ActivatePowerup(enemyCharacter);
            timeUntilNextBoost = GetTimeUntilNextBoost();
        }
    }

    float GetTimeUntilNextBoost()
    {
        return Random.Range(ConfigManager.instance.enemyBoostMin, ConfigManager.instance.enemyBoostMax);
    }

     Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
    {

        Vector3 toTarget = target - origin;
        Vector3 toTargetXZ = toTarget;
        toTargetXZ.y = 0;

        // calculate xz and y
        float y = toTarget.y;
        float xz = toTargetXZ.magnitude;

        // calculate starting speeds for xz and y
        float t = timeToTarget;
        float v0y = y / t + 0.5f * Physics.gravity.magnitude * t; // based on x = v0 * t + 1/2 * a * t * t, where a is negative Physics.gravity.magnitude
        float v0xz = xz / t;                                      // based on the above with a = 0

        // create result vector for calculated starting speeds
        Vector3 result = toTargetXZ.normalized;
        result *= v0xz;
        result.y = v0y;

        return result;
    }
}
