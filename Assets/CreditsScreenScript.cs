using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator Start()
    {
		yield return new WaitForSeconds(1);

		while (true) {
			if (Input.anyKeyDown) {
				if (MainMenuScript.shouldQuit) {
					Application.Quit();
				}

				SceneManager.LoadScene("NewGameScene");
			}
			yield return null;
		}        
    }

}
