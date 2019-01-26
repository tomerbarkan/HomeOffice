using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler {
	protected Character player;
	protected Vector3 playerPosition;
	protected Camera camera;

	protected string controllerX;
	protected string controllerY;
	protected string controllerFire;

	public PlayerInputHandler(Character player, Vector3 playerPosition, Camera camera, string[] controllerInputs) {
		this.player = player;
		this.playerPosition = playerPosition;
		this.camera = camera;
		this.controllerX = controllerInputs[0];
		this.controllerY = controllerInputs[1];
		this.controllerFire = controllerInputs[2];
	}

	public void HandleInput() {
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		if (Input.GetMouseButtonDown(0)) {
			GameManager.instance.StartCoroutine(HandleSwipe());
		}

		if (Input.GetButtonDown(controllerFire)) {
			GameManager.instance.StartCoroutine(HandleFire());
		}
	}

	public IEnumerator HandleSwipe() {
		Vector2 mouseStart = Input.mousePosition;

        if(player.cooldownRemaining <= 0 && player.canShoot)
          player.animator.SetTrigger("Throw");
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

	public IEnumerator HandleFire() {
		float force = 0;
		float forcePerSec = 1f;

        if(player.cooldownRemaining <= 0 && player.canShoot)
            player.animator.SetTrigger("Throw");
        while (!Input.GetButtonUp(controllerFire)) {
			force += Time.deltaTime * forcePerSec;
			yield return null;
		}

		float x = Input.GetAxisRaw(controllerX);
		float y = Input.GetAxisRaw(controllerY);
		Vector2 direction = new Vector2(x, y).normalized;

		player.Throw(direction * (force * ConfigManager.instance.maxThrowSpeed));
	}

}
