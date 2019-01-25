using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (Input.GetMouseButtonDown(0)) {
			Vector2 playerScreenPoint = camera.WorldToScreenPoint(playerPosition);
			Vector2 toMouse = (Vector2)Input.mousePosition - playerScreenPoint);
			float angle = Vector2.Angle(Vector2.right, toMouse);
			float force = Mathf.Clamp(toMouse.magnitude, 0, 1);
			player.Throw(angle, force);
		}
	}
}
