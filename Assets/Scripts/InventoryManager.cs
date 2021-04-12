using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
	
	PlayerHealth playerHealth;
	public Inventory inventory;
	public GameObject inventoryPrefab;
	private bool open = false;
	GameObject inventoryInstance;

	// Use this for initialization
	void Start () {
		playerHealth = GetComponent<PlayerHealth>();

		inventory = new Inventory {
			id = new int[32],
			name = new string[32],
			imagePath = new string[32],
			amount = new int[32]
		};
		Item item = getById(0, 5);
		inventory.id[0] = item.id;
		inventory.name[0] = item.name;
		inventory.imagePath[0] = item.imagePath;
		inventory.amount[0] = playerHealth.meatInt;		
	}
	
	// Update is called once per frame
	void Update () {

		// I don't like this line and I would like to get rid of it
		inventory.amount[0] = playerHealth.meatInt;

		if (Input.GetKeyDown(KeyCode.E)) {
			if (!open) {
				open = true;
				inventoryInstance = Instantiate(inventoryPrefab);
				DisplayInventory();
				Cursor.lockState = CursorLockMode.None;
				GetComponent<PlayerMovement>().enabled = false;
				
			} 
			
			else if ((inventoryInstance != null) && open) {
				open = false;
				Destroy(inventoryInstance);
				Cursor.lockState = CursorLockMode.Locked;
				GetComponent<PlayerMovement>().enabled = true;
			}
			
		}
	}

	[Serializable]
	public class Inventory {
		public int[] id;
		public string[] name;
		public Image[] image;
		public string[] imagePath;
		public int[] amount;
	}
	public class Item {
		public int id;
		public string name;
		public Image image;
		public string imagePath;
		public string modelPath;
		public int amount;
	}
	void OnTriggerEnter (Collider col) {
		GameObject collided = col.gameObject;
		if (col.gameObject.tag == "meat") {
			playerHealth.meatInt += 1;
			Destroy(col.gameObject);	
			inventory.amount[0]++;		
		}
	}
	
	void DisplayInventory() {
		Image currentImage;
		Text amountText;
		print(inventory.name.Length);
		for (int i = 0; i < inventory.name.Length; i++) {
			if (inventory.name[i] != null) {
				currentImage = Instantiate(Resources.Load<Image>(inventory.imagePath[i]));
				
				currentImage.transform.parent = inventoryInstance.transform;
				currentImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-195 + i * 55, 85);
				amountText = currentImage.GetComponentInChildren<Text>();
				amountText.text = inventory.amount[i].ToString();
			}
			
		}
	}
	public Item getById(int id, int amount) {
		string itemsAsJson = File.ReadAllText("Assets/items.json");
		Inventory itemList = (Inventory)JsonUtility.FromJson(itemsAsJson, typeof(Inventory));
		Item item = new Item{
			id = itemList.id[id],
			name = itemList.name[id],
			imagePath = itemList.imagePath[id],
			amount = amount
		};
		return item;
	}
}
