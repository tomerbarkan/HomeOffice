using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
	public float maxThrowSpeed = 10f;

	public static ConfigManager instance;

    void Awake() {
		instance = this;
    }
}
