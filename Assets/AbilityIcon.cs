using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    public Button button;
    public Image icon;

    [SerializeField]
    UnityAction actionToInvoke;

   public void Set(Sprite sprite, UnityAction action)
    {
        this.button.image.sprite = sprite;

        if (sprite == null)
            icon.enabled = false;
        else
            icon.enabled = true;

        if (action == null)
            button.interactable = false;
        else
            button.interactable = true;
    }

    public void InvokeAction()
    {
        actionToInvoke.Invoke();
    }
}
