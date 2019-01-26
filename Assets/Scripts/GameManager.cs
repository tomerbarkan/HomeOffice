using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	public Transform clockHandle;

    public AudioManager audioManager;

	public ConfigManager[] nextConfigs;

	public RectTransform dragIndicator;
	public RectTransform dragIndicator2;

	public RectTransform winScreen;
	public Button restartButton;


	protected int nextConfig = 0;
	protected float totalTime;


	private void Awake() {
		instance = this;
		SetConfig(nextConfig++);
		totalTime = CalculateRemainingTime();
	}

	// Start is called before the first frame update
	private void Start() {
		playerInput = new PlayerInputHandler(player, player.transform.position, camera, dragIndicator, new string[] { "Horizontal1", "Vertical1", "Fire1" }, 1);
		if (useAi) {
			enemyAI = new EnemyAIHandler(enemy, player, enemy.transform.position, powerupOptions);
		} else {
			playerInput2 = new PlayerInputHandler(enemy, enemy.transform.position, camera, dragIndicator2, new string[] { "Horizontal2", "Vertical2", "Fire2" }, 2);
		}
		boostSpawner = new BoostSpawner(boostSpawns, powerupOptions);

		restartButton.onClick.AddListener(() => {
			Time.timeScale = 1;
			SceneManager.LoadScene("NewGameScene");
		});
    }

    // Update is called once per frame
    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("NewGameScene");
		}

		playerInput.HandleInput();

		if (useAi) {
			enemyAI.Think();
		} else {
			playerInput2.HandleInput();
		}
		boostSpawner.Simulate();
		AdvanceStages();

		float handlePercent = CalculateRemainingTime() / totalTime;
		clockHandle.eulerAngles = new Vector3(clockHandle.eulerAngles.x, clockHandle.eulerAngles.y, handlePercent * 360);
    }

    [ContextMenu("Activate Next Stage")]
	protected void AdvanceStages() {
		ConfigManager.instance.stageTime -= Time.deltaTime;

		if (nextConfig >= nextConfigs.Length) {
			return;
		}

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

    protected float CalculateRemainingTime() {
		float time = 0;
		time += ConfigManager.instance.stageTime;
		for (int i = nextConfig; i < nextConfigs.Length; i++) {
			time += nextConfigs[i].stageTime;
		}

		return Mathf.Max(0, time);
	}

	public void GameOver(Character loser) {
		winScreen.gameObject.SetActive(true);
		loser.firedText.gameObject.SetActive(true);
		Time.timeScale = 0;
	}
}
