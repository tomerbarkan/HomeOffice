using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
	public static bool shouldQuit = false;

	public GameObject loadingText;
	public GameObject buttonsObj;

	public GameObject singleInstructions;
	public GameObject mpInstructions;

	public Button newGameButton;
	public Button mpButton;
	public Button creditsButton;
	public Button quitButton;

	// Start is called before the first frame update
	void Start() {
		if(Input.GetJoystickNames().Length < 2) {
			Destroy(mpButton.gameObject);
		}

		quitButton.onClick.AddListener(() => {
			shouldQuit = true;
			SceneManager.LoadScene("CreditsScene");
		});

		mpButton.onClick.AddListener(() => {
			GameManager.useAi = false;
			StartCoroutine(NewGameCoroutine(mpInstructions));
		});

		newGameButton.onClick.AddListener(() => {
			GameManager.useAi = true;
			StartCoroutine(NewGameCoroutine(singleInstructions));
		});

		creditsButton.onClick.AddListener(() => {
			SceneManager.LoadScene("CreditsScene");
		});
    }

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			shouldQuit = true;
			SceneManager.LoadScene("CreditsScene");
		}
	}

	private IEnumerator NewGameCoroutine(GameObject instructions) {
		buttonsObj.SetActive(false);
		instructions.SetActive(true);
		yield return null;
		while (!Input.anyKeyDown) {
			yield return null;
		}

		instructions.SetActive(false);
		loadingText.SetActive(true);

		SceneManager.LoadScene("Testing Scene");
	}
}
