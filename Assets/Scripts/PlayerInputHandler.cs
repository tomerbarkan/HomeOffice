using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler {
	protected Character player;
	protected Vector3 playerPosition;
	protected Camera camera;

	public PlayerInputHandler(Character player, Vector3 playerPosition, Camera camera) {
		this.player = player;
		this.playerPosition = playerPosition;
		this.camera = camera;
	}

	public void HandleInput() {
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		if (Input.GetMouseButtonDown(0)) {
			GameManager.instance.StartCoroutine(HandleSwipe());
		}
	}

	public IEnumerator HandleSwipe() {
		Vector2 mouseStart = Input.mousePosition;

		while (!Input.GetMouseButtonUp(0)) {
			yield return null;
		}

		Vector2 mouseEnd = Input.mousePosition;
		Vector2 toMouse = mouseEnd - mouseStart;
		float mag = toMouse.magnitude;
		if (mag < ConfigManager.instance.minSwipeRange) {
			yield break;
		}

		float force = Mathf.InverseLerp(0, ConfigManager.instance.maxSwipeRange, toMouse.magnitude);
		player.Throw(toMouse.normalized * (force * ConfigManager.instance.maxThrowSpeed));
	}
}
