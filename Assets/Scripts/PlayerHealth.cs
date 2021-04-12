using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public Text healthText;
	public Text hungerText;
	public Text meatAmount;
	public int meatInt = 5;
	public Image healthBar;
	public Image hungerBar;
	public float health;
	public float hunger;
	private bool dead = false;

	public int deaths;
	public GameObject gameOverPrefab;
	private GameObject gameOverInstance;
	// Use this for initialization
	void Start () {
		health = 100;
		hunger = 100;
		InvokeRepeating("DecrementHunger", 1f, 1f);
		InvokeRepeating("DecrementHealthAway", 0.1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		healthText.text = health.ToString();
		hungerText.text = hunger.ToString();
		meatAmount.text = meatInt.ToString();
		if (health <= 0 && !dead) {
			dead = true;
			HandleDeath();
		}
		if (Input.GetKeyDown(KeyCode.F) && meatInt > 0) {
			hunger += 10f;
			meatInt--;
		}
		if (hunger >= 100) {
			hunger = 100f;
		}
		if (hunger <= 0) {
			hunger = 0;
		}

		hungerBar.fillAmount = hunger / 100;
		healthBar.fillAmount = health / 100;
	}

	void DecrementHealthAway() {
		if (hunger <= 0) {
			DecrementHealth(2);
		}
	}
	void DecrementHealth (int amount) {
		health -= amount;
	}
	void DecrementHunger () {
		hunger--;
	}

	void Respawn() {
		transform.position = new Vector3(355,52,144);
		health = 100;
		hunger = 100;
		dead = false;
		Cursor.lockState = CursorLockMode.Locked;
		EnableDisableScripts(true);
		if (gameOverInstance != null) {
			Destroy(gameOverInstance);
		}		
	}
	void HandleDeath() {
		gameOverInstance = Instantiate(gameOverPrefab);
		Cursor.lockState = CursorLockMode.None;
		EnableDisableScripts(false);
		Button button = gameOverInstance.GetComponentInChildren(typeof(Button)) as Button;
		button.onClick.AddListener(Respawn);
		deaths++;
	}
	public void EnableDisableScripts(bool enabled) {
		GetComponent<PlayerMovement>().enabled = enabled;
		GetComponent<PlayerHealth>().enabled = enabled;
		//cameraBehaviour = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<CameraBehvaiour>.enabled = false;
	}
}
