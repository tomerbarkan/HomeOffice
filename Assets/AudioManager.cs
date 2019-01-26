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

		// play new phase
	    phases[newPhase].Play();
	    if (jointPlaying == 0) { 
		    if (bossPlaying == 0) {
				// If no joint and boss playing, fade to normal volume
			    phases[newPhase].DOFade(1, 1);
		    } else {
				// If boss playing, fade to boss lower volume
			    phases[newPhase].DOFade(0.25f, 1f);
		    }
		} else {
			// if joint playing, volume should stay 0
		    phases[newPhase].volume = 0;
	    }
    }

    public void PlayAudioOneShot(AudioClip clipToPlay)
    {
        mainAudioSource.PlayOneShot(clipToPlay);
    }    

    public void PlayJointMusic()
    {
        jointMusicSource.Play();
	    jointMusicSource.DOFade(1, 0.4f);
	    phases[currentPhase].DOFade(0, 0.4f);
	    jointPlaying++;

        StartCoroutine(ResumeMusic(jointPowerup.activeTime, () => jointPlaying--));
    }

    public void PlayBossEnterance()
    {
        bossEnteranceAudioSource.Play();
	    bossPlaying++;
        phases[currentPhase].DOFade(0.25f, 0.4f);

	    StartCoroutine(ResumeMusic(bossPowerjup.activeTime, () => bossPlaying--));
    }

	IEnumerator ResumeMusic(float delay, System.Action onDone) {
		yield return new WaitForSeconds(delay);

		// decrease boss or joint playing as necessary
		onDone();

		if (jointPlaying == 0) {
			// no more joint, so fade it out
			jointMusicSource.DOFade(0, 0.4f);

			// if boss still playing, fade music in only to 0.25
			if (bossPlaying != 0) {
				phases[currentPhase].DOFade(0.25f, 0.4f);
			}
		}

		if (bossPlaying == 0) {
			// if none playing, fade music back to 1
			// if joint is playing, then music should remain 0, and when joint is done it will take care of it
			if (jointPlaying == 0) {
				phases[currentPhase].DOFade(1, 0.4f);			
			}
		}
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
