using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Character player;
    public Character enemy;
	public new Camera camera;
	public Transform boostSpawns;
	public PowerupCollectible[] powerupOptions;

	public PlayerInputHandler playerInput;
    public EnemyAIHandler enemyAI;
	public BoostSpawner boostSpawner;

    // Start is called before the first frame update
    void Start() {
		playerInput = new PlayerInputHandler(player, player.transform.position, camera);
        enemyAI = new EnemyAIHandler(enemy,player,enemy.transform.position);
		boostSpawner = new BoostSpawner(boostSpawns, powerupOptions);
    }

    // Update is called once per frame
    void Update()
    {
		playerInput.HandleInput();
        enemyAI.Think();
		boostSpawner.Simulate();
    }
}
