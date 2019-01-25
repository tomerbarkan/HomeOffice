using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{


    public AudioSource[] phases;

    
    public void ActivateNextPhase(int newPhase)
    {
        phases[newPhase - 1].DOFade(0, 1);
        phases[newPhase].DOFade(1, 1).ChangeStartValue(0);
        phases[newPhase].Play();
    }
    
    
}
