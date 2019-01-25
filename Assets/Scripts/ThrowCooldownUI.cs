using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowCooldownUI : MonoBehaviour
{

    public Image fillBar;

    public void Set(float percent)
    {
        this.fillBar.fillAmount = percent;
    }


    [ContextMenu("Try To Set")]
    void SetTry()
    {
        Set(0.5f);
    }
}
