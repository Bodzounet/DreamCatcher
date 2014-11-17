﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour {
	public GUISkin						mySkin;
	public float						delayBetweenFocusChanges = .5f;
	
	private JoystickButtonMenu			mainMenu;
	private Rect[]						myRects = new Rect[4];
	private string[]					mainMenuLabels = new string[4];
	private int							currentlyPressedButton = -1;

	CharacterInventory					characterInventoryLeft;
	CharacterInventory					characterInventoryRight;
	Dictionary<string, Texture>			items = new Dictionary<string, Texture>();
	Dictionary<Key.KeyType, Texture>	keys = new Dictionary<Key.KeyType, Texture>();
	
	// Use this for initialization
	void Start () {
		myRects[0] = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 25, 50, 50);
		myRects[1] = new Rect(Screen.width / 2 - 90, Screen.height / 2 - 25, 50, 50);
		myRects[2] = new Rect(Screen.width / 2 + 90, Screen.height / 2 - 25, 50, 50);
		myRects[3] = new Rect(Screen.width / 2 + 150, Screen.height / 2 - 25, 50, 50);
		
		mainMenuLabels[0] = "";
		mainMenuLabels[1] = "";
		mainMenuLabels[2] = "";
		mainMenuLabels[3] = "";

		mainMenu = new JoystickButtonMenu(4, myRects, mainMenuLabels, "Validate", JoystickButtonMenu.JoyAxis.Horizontal);
		mainMenu.enabled = false;

		characterInventoryLeft = GameObject.Find ("CharacterLeft").GetComponent<CharacterInventory> ();
		characterInventoryRight = GameObject.Find ("CharacterRight").GetComponent<CharacterInventory> ();
		items ["Flame"] = Resources.Load<Texture>("Flame");
		items ["Water"] = Resources.Load<Texture>("Water");
		keys [Key.KeyType.NO_KEY] = Resources.Load<Texture>("NoKey");
		keys [Key.KeyType.NORMAL] = Resources.Load<Texture>("NormalKey");
		keys [Key.KeyType.SPECTRAL] = Resources.Load<Texture>("SpectralKey");
	}
	
	void	OnGUI() {
		GUI.skin = mySkin;

		if (mainMenu.enabled) {
			GUI.Box (new Rect (Screen.width / 2 - 170, Screen.height / 2 - 40, 390, 80), "");
			mainMenu.DisplayButtons ();
			GUI.Label (new Rect (Screen.width / 2 - 145, Screen.height / 2 - 20, 40, 40), items[characterInventoryLeft.item]);
			GUI.Label (new Rect (Screen.width / 2 + 155, Screen.height / 2 - 20, 40, 40), items[characterInventoryRight.item]);
			GUI.Label (new Rect (Screen.width / 2 - 80, Screen.height / 2 - 20, 30, 40), keys[characterInventoryLeft.key]);
			GUI.Label (new Rect (Screen.width / 2 + 100, Screen.height / 2 - 20, 30, 40), keys[characterInventoryRight.key]);
		} 
	}

	// Update is called once per frame
	void Update () {
		if(mainMenu.enabled) {
			if(mainMenu.CheckJoystickAxis()){
				Invoke("Delay",delayBetweenFocusChanges);
			}
			currentlyPressedButton = mainMenu.CheckJoystickButton();
			if (currentlyPressedButton == 0) {
				characterInventoryLeft.SwapItem();
				characterInventoryRight.SwapItem();
				return;
			}
			else if (currentlyPressedButton == 1) {
				Key.KeyType		tmp = characterInventoryLeft.key;

				characterInventoryLeft.key = characterInventoryRight.key;
				characterInventoryRight.key = tmp;
				return;
			}
			if (Input.GetButtonDown ("Cancel"))
				mainMenu.enabled = false;
		}
		else if (Input.GetButtonDown("Validate")) {
			mainMenu.enabled = true;
		}
	}

	private void Delay(){
		mainMenu.isCheckingJoy = false;
	}
}
