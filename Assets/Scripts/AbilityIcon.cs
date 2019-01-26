using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    public Button button;
    public Image icon;
    public AudioSource audioSource;

    [SerializeField]
    UnityAction actionToInvoke;

   public void Set(Sprite sprite, UnityAction action)
    {
        this.button.image.sprite = sprite;

		if (sprite == null) {
			icon.gameObject.SetActive(false);
		} else {
			icon.gameObject.SetActive(true);
		}

		button.onClick.RemoveAllListeners();
		if (action != null) {
			//Debug.Log("Setting action");
			button.onClick.AddListener(action);
		}
    }
}
