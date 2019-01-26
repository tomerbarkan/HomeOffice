using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; set; }

    public AudioSource jointMusicSource;
    public AudioSource mainAudioSource;
    public AudioSource bossEnteranceAudioSource;
    public AudioSource[] phases;
    
    

    int currentPhase = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateNextPhase(int newPhase)
    {
        currentPhase = newPhase;
		if (newPhase > 0) {
			phases[newPhase - 1].DOFade(0, 1);
		}
        phases[newPhase].DOFade(1, 1).ChangeStartValue(0);
        phases[newPhase].Play();
    }


    public void PlayAudioOneShot(AudioClip clipToPlay)
    {
        mainAudioSource.PlayOneShot(clipToPlay);
    }
    

    public void PlayJointMusic()
    {
        jointMusicSource.Play();
        phases[currentPhase].Pause();
        StartCoroutine(ResumeMusic(3.75f));
    }

    public void PlayBossEnterance()
    {
        bossEnteranceAudioSource.Play();
    }

    IEnumerator ResumeMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        jointMusicSource.Stop();
        phases[currentPhase].Play();
    }

    
}
