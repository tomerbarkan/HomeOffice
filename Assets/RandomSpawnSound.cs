using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnSound : MonoBehaviour
{

    public AudioSource[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        sounds[Random.Range(0, sounds.Length)].Play();
    }

    
}
