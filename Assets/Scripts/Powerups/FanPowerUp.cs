using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPowerUp : Powerup
{
    public float activeTime;
    public GameObject fanPrefab;

    public override void ActivatePowerup(Character activator)
    {
        activator.StartCoroutine(DelayCoroutine(activator, activeTime));
    }

    private IEnumerator DelayCoroutine(Character activator, float delay)
    {
        GameObject spawnedFan = GameObject.Instantiate(fanPrefab, activator.fanSpawnPoint.position,activator.fanSpawnPoint.rotation);
        

        while (delay > 0)
        {
          

            delay -= Time.deltaTime;
            yield return null;
        }

        Destroy(spawnedFan);
    }
}
