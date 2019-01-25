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
			Vector2 playerScreenPoint = camera.WorldToScreenPoint(playerPosition);
			Vector2 toMouse = (Vector2)Input.mousePosition - playerScreenPoint;
			float force = Mathf.InverseLerp(0, 100, toMouse.magnitude);
			player.Throw(toMouse.normalized * (force * ConfigManager.instance.maxThrowSpeed));
		}
	}
}
