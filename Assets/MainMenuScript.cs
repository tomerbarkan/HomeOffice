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

	public Button newGameButton;
	public Button creditsButton;
	public Button quitButton;

	// Start is called before the first frame update
	void Start() {
		quitButton.onClick.AddListener(() => {
			shouldQuit = true;
			SceneManager.LoadScene("CreditsScene");
		});

		newGameButton.onClick.AddListener(() => {
			loadingText.SetActive(true);
			buttonsObj.SetActive(false);
			SceneManager.LoadScene("Testing Scene");
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
}
