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
    public AudioSource youWinAudioSource;
    public AudioSource youLoseAudioSource;
    public AudioSource[] phases;

	public JointPowerup jointPowerup;
	public BossPowerUp bossPowerjup;

    int currentPhase = -1;
	private float phaseVol = 1;

	private int bossPlaying = 0;
	private int jointPlaying = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateNextPhase(int newPhase) {
		if (currentPhase >= 0) {
			phases[currentPhase].DOFade(0, 1);
		}
	    currentPhase = newPhase;

	    if (jointPlaying == 0 && bossPlaying == 0) {
		    phases[newPhase].Play();
		    phases[newPhase].DOFade(1, 1);
	    } 
    }

    public void PlayAudioOneShot(AudioClip clipToPlay)
    {
        mainAudioSource.PlayOneShot(clipToPlay);
    }    

    public void PlayJointMusic()
    {
        jointMusicSource.Play();
	    jointMusicSource.volume = 1;
	    jointPlaying++;
        phases[currentPhase].Pause();
        StartCoroutine(ResumeMusicJoint(jointPowerup.activeTime));
    }

    public void PlayBossEnterance()
    {
        bossEnteranceAudioSource.Play();
	    bossPlaying++;
        phases[currentPhase].DOFade(0.25f, 0.4f);
	    StartCoroutine(ResumeMusicBoss(bossPowerjup.activeTime));
    }

    IEnumerator ResumeMusicJoint(float delay)
    {
        yield return new WaitForSeconds(delay);

	    jointPlaying--;

	    if (jointPlaying == 0) {
		    jointMusicSource.DOFade(0, 0.4f);
	    }

	    if (jointPlaying == 0 && bossPlaying == 0) {
		    phases[currentPhase].Play();
		    phases[currentPhase].DOFade(1, 0.4f);
	    }
    }

	IEnumerator ResumeMusicBoss(float delay) {
		yield return new WaitForSeconds(delay);

		bossPlaying--;

		if (jointPlaying == 0 && bossPlaying == 0) {
			phases[currentPhase].Play();
			phases[currentPhase].DOFade(1, 0.4f);
		}
	}


    public void PlayYouWin() {
	    bossEnteranceAudioSource.Stop();
	    jointMusicSource.Stop();
	    phases[currentPhase].Stop();
        youWinAudioSource.Play();
    }

    public void PlayYouLose()
    {
	    bossEnteranceAudioSource.Stop();
	    jointMusicSource.Stop();
	    phases[currentPhase].Stop();
        youLoseAudioSource.Play();
    }
}
