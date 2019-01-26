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

	protected RectTransform dragIndicator;

	float widthMultiplier;
	int playerNum;

	public PlayerInputHandler(Character player, Vector3 playerPosition, Camera camera, RectTransform dragIndicator, string[] controllerInputs, int playerNum) {
		this.player = player;
		this.playerPosition = playerPosition;
		this.camera = camera;
		this.dragIndicator = dragIndicator;
		this.controllerX = controllerInputs[0];
		this.controllerY = controllerInputs[1];
		this.controllerFire = controllerInputs[2];
		this.playerNum = playerNum;

		widthMultiplier = 1920f / Screen.width;
	}

	public void HandleInput() {
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		if (Input.GetKeyDown("joystick " + playerNum + " button 3")) {
			player.ActivatePowerup(0);
		}

		if (Input.GetKeyDown("joystick " + playerNum + " button 4")) {
			player.ActivatePowerup(1);
		}

		if (Input.GetKeyDown("joystick " + playerNum + " button 1")) {
			player.ActivatePowerup(2);
		}


		if (player.cooldownRemaining >= 0) {
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
		Vector2 mouseEnd;
		Vector2 toMouse = Vector2.zero;
		float mag = 0;

        if (player.cooldownRemaining <= 0 && player.canShoot)
            player.animator.SetTrigger("Throw");
        while (!Input.GetMouseButtonUp(0)) {
			yield return null;

			mouseEnd = Input.mousePosition;
			toMouse = mouseEnd - mouseStart;
			mag = Mathf.Clamp(toMouse.magnitude, 0, ConfigManager.instance.maxSwipeRange);

			if (mag >= ConfigManager.instance.minSwipeRange) {
				dragIndicator.gameObject.SetActive(true);
				dragIndicator.right = toMouse;
				dragIndicator.sizeDelta = new Vector2(mag * widthMultiplier, dragIndicator.sizeDelta.y);
				dragIndicator.position = mouseStart;
			} else {
				dragIndicator.gameObject.SetActive(false);
			}
		}

		if (mag < ConfigManager.instance.minSwipeRange) {
			yield break;
		}

		dragIndicator.gameObject.SetActive(false);
		float force = Mathf.InverseLerp(0, ConfigManager.instance.maxSwipeRange, mag);
		player.Throw(toMouse.normalized * (ConfigManager.instance.minThrowSpeed + force * (ConfigManager.instance.maxThrowSpeed - ConfigManager.instance.minThrowSpeed)));
	}

	public IEnumerator HandleFire() {
		float mag = 0;
		float magPerSec = ConfigManager.instance.maxSwipeRange / ConfigManager.instance.buttonMaxForceTime;
		Vector2 toMouse = Vector2.zero;

        if (player.cooldownRemaining <= 0 && player.canShoot)
            player.animator.SetTrigger("Throw");

        while (!Input.GetButtonUp(controllerFire)) {
			yield return null;

			float x = Input.GetAxisRaw(controllerX);
			float y = Input.GetAxisRaw(controllerY);

			mag += Time.deltaTime * magPerSec;
			toMouse = new Vector2(x, y).normalized;

			if (mag >= ConfigManager.instance.minSwipeRange) {
				dragIndicator.gameObject.SetActive(true);
				dragIndicator.right = toMouse;
				dragIndicator.sizeDelta = new Vector2(mag * widthMultiplier, dragIndicator.sizeDelta.y);
				dragIndicator.position = Camera.main.WorldToScreenPoint(player.throwableSpawnPoint.position);
			} else {
				dragIndicator.gameObject.SetActive(false);
			}
		}


		dragIndicator.gameObject.SetActive(false);
		float force = Mathf.InverseLerp(0, ConfigManager.instance.maxSwipeRange, mag);
		player.Throw(toMouse * (ConfigManager.instance.minThrowSpeed + force * (ConfigManager.instance.maxThrowSpeed - ConfigManager.instance.minThrowSpeed)));
	}

}
