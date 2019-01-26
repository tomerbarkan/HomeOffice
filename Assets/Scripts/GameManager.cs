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
	public bool useAi = true;

	public PlayerInputHandler playerInput;
	public PlayerInputHandler playerInput2;
    public EnemyAIHandler enemyAI;
	public BoostSpawner boostSpawner;

    public AudioManager audioManager;

	public ConfigManager[] nextConfigs;

    
	protected int nextConfig = 0;


	private void Awake() {
		instance = this;
		SetConfig(nextConfig++);
	}

	// Start is called before the first frame update
	private void Start() {
		playerInput = new PlayerInputHandler(player, player.transform.position, camera, new string[] { "Horizontal1", "Vertical1", "Fire1"});
		if (useAi) {
			enemyAI = new EnemyAIHandler(enemy, player, enemy.transform.position, powerupOptions);
		} else {
			playerInput2 = new PlayerInputHandler(enemy, enemy.transform.position, camera, new string[] { "Horizontal2", "Vertical2", "Fire2"});
		}
		boostSpawner = new BoostSpawner(boostSpawns, powerupOptions);
    }

    // Update is called once per frame
    private void Update()
    {
		playerInput.HandleInput();

		if (useAi) {
			enemyAI.Think();
		} else {
			playerInput2.HandleInput();
		}
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
			SetConfig(nextConfig++);
        }
	}

	protected void SetConfig(int index) {
		if (ConfigManager.instance != null && ConfigManager.instance.gameObject != null) {
			Destroy(ConfigManager.instance.gameObject);
		}

        audioManager.ActivateNextPhase(index);
        ConfigManager.instance = GameObject.Instantiate(nextConfigs[index]);
	}

    
}
