using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

   
	public Character player;
    public Character enemy;
	public new Camera camera;
	public Transform boostSpawns;
	public PowerupCollectible[] powerupOptions;

	public PlayerInputHandler playerInput;
    public EnemyAIHandler enemyAI;
	public BoostSpawner boostSpawner;

    public AudioManager audioManager;

	public ConfigManager[] nextConfigs;

    
	protected int nextConfig = 0;


	private void Awake() {
		instance = this;
		GameObject.Instantiate(nextConfigs[nextConfig++]);
	}

	// Start is called before the first frame update
	private void Start() {
		playerInput = new PlayerInputHandler(player, player.transform.position, camera);
        enemyAI = new EnemyAIHandler(enemy,player,enemy.transform.position, powerupOptions);
		boostSpawner = new BoostSpawner(boostSpawns, powerupOptions);
    }

    // Update is called once per frame
    private void Update()
    {
		playerInput.HandleInput();
        enemyAI.Think();
		boostSpawner.Simulate();
		AdvanceStages();
    }

    [ContextMenu("Activate Next Stage")]
	protected void AdvanceStages() {
		if (nextConfig >= nextConfigs.Length) {
			return;
		}

		ConfigManager.instance.stageTime -= Time.deltaTime;
		if (ConfigManager.instance.stageTime <= 0) {
			Destroy(ConfigManager.instance.gameObject);

            audioManager.ActivateNextPhase(nextConfig);
            GameObject.Instantiate(nextConfigs[nextConfig++]);
           
        }
	}

    
}
