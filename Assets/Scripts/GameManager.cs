using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Character player;
	public new Camera camera;

	public PlayerInputHandler playerInput;

    // Start is called before the first frame update
    void Start() {
		playerInput = new PlayerInputHandler(player, player.transform.position, camera);
    }

    // Update is called once per frame
    void Update()
    {
		playerInput.HandleInput();
    }
}
