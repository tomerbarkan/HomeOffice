using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPowerUp : Powerup
{
    public float activeTime;
   // public GameObject fanPrefab;

    public override void ActivatePowerup(Character activator)
    {
        activator.StartCoroutine(DelayCoroutine(activator, activeTime));
    }

    private IEnumerator DelayCoroutine(Character activator, float delay)
    {
        // GameObject spawnedFan = GameObject.Instantiate(fanPrefab, activator.fanSpawnPoint.position,activator.fanSpawnPoint.rotation);
        activator.fan.Activate();


        while (delay > 0)
        {
          

            delay -= Time.deltaTime;
            yield return null;
        }

        activator.fan.Deactivate();
        // Destroy(spawnedFan);
    }
}
