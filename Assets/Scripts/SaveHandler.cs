using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveHandler : MonoBehaviour {

	PlayerHealth playerHealth;
	PlayerMovement playerMovement;
	private InventoryManager inventoryManager;
	string fileName = "Assets/save.json";
	public GameObject cow;

	// Use this for initialization
	void Start () {
		inventoryManager = GetComponent<InventoryManager>();
		playerHealth = GetComponent<PlayerHealth>();
		playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F2)) {
			Save();
		}
		if (Input.GetKeyDown(KeyCode.F3)) {
			Load();
		}
	}

	private void Save() {
		
		GameObject[] cowsObj = GameObject.FindGameObjectsWithTag("cow");
		SaveObject saveObject = new SaveObject {
			playerPosition = transform.position,
			playerRotation = transform.rotation,
			inventory = inventoryManager.inventory,
			health = playerHealth.health,
			hunger = playerHealth.hunger,
			deaths = playerHealth.deaths,
			cowPositions = new Vector3[cowsObj.Length],
			cowRotations = new Quaternion[cowsObj.Length]
		};
		for (int i = 0; i < cowsObj.Length; i++) {
			saveObject.cowPositions[i] = cowsObj[i].transform.position;
			saveObject.cowRotations[i] = cowsObj[i].transform.rotation;
		}

		string saveJson = JsonUtility.ToJson(saveObject, true);
		
		File.WriteAllText("Assets/save.json", saveJson);
	}

	private void Load() {
		string saveInJson;
		saveInJson = File.ReadAllText("Assets/save.json");
		SaveObject save = (SaveObject)JsonUtility.FromJson(saveInJson, typeof(SaveObject));
		
		// Commented the reloading the scene out for later, due to lighting baking not working correctly
		SceneManager.LoadScene("Scenes/TemperatePlanet", LoadSceneMode.Single);

		transform.position = save.playerPosition;
		transform.rotation = save.playerRotation;
		playerHealth.health = save.health;
		playerHealth.hunger = save.hunger;
		inventoryManager.inventory = save.inventory;

		GameObject[] cowsObj = GameObject.FindGameObjectsWithTag("cow");
		for (int i = 0; i < save.cowPositions.Length; i++) {
			Destroy(cowsObj[i]);
			Instantiate(cow, save.cowPositions[i], save.cowRotations[i]);
		}


		print(save.health);
	}

	[Serializable]
	public class SaveObject {
		public Vector3 playerPosition;
		public Quaternion playerRotation;
		public int cowAmount;
		public float health;
		public float hunger;
		public int deaths;
		public InventoryManager.Inventory inventory;
		
		public Vector3[] cowPositions;
		public Quaternion[] cowRotations;
	}
	[Serializable]
	public class Cow {
		public Vector3 cowPosition;
		public Quaternion cowRotation;
	}
}


