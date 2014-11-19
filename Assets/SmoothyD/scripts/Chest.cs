using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour {
	public GUISkin						mySkin;
	public float						delayBetweenFocusChanges = .5f;
    public bool                         isOpen;
	
	private JoystickButtonMenu			mainMenu;
	private Rect[]						myRects = new Rect[4];
	private string[]					mainMenuLabels = new string[4];
	private int							currentlyPressedButton = -1;
    private bool                        canBeOpenend = false;
    private GameObject                  player;
	CharacterInventory				    characterInventoryLeft;
	CharacterInventory				    characterInventoryRight;
	Dictionary<Item.ItemType, Texture>	items = new Dictionary<Item.ItemType, Texture>();
	Dictionary<Key.KeyType, Texture>	keys = new Dictionary<Key.KeyType, Texture>();
    MovementController                  movementController;
    public GameObject                     realChest;
    public GameObject centre;
    public bool centerH;
    public AudioClip[] sounds;
    GameObject che;
	void Start () {
		myRects[0] = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 25, 50, 50);
		myRects[1] = new Rect(Screen.width / 2 - 90, Screen.height / 2 - 25, 50, 50);
		myRects[2] = new Rect(Screen.width / 2 + 90, Screen.height / 2 - 25, 50, 50);
		myRects[3] = new Rect(Screen.width / 2 + 150, Screen.height / 2 - 25, 50, 50);
		
		mainMenuLabels[0] = "";
		mainMenuLabels[1] = "";
		mainMenuLabels[2] = "";
		mainMenuLabels[3] = "";

		mainMenu = new JoystickButtonMenu(4, myRects, mainMenuLabels, "Jump", JoystickButtonMenu.JoyAxis.Horizontal);
		mainMenu.enabled = false;

        isOpen = false;

		characterInventoryLeft = GameObject.Find ("CharacterLeft").GetComponent<CharacterInventory> ();
		characterInventoryRight = GameObject.Find ("CharacterRight").GetComponent<CharacterInventory> ();
		items [Item.ItemType.NO_ITEM] = Resources.Load<Texture>("NoItem");
		items [Item.ItemType.FLAME] = Resources.Load<Texture>("FlameSprite");
		items [Item.ItemType.WATER] = Resources.Load<Texture>("WaterSprite");
		keys [Key.KeyType.NO_KEY] = Resources.Load<Texture>("NoKeySprite");
		keys [Key.KeyType.NORMAL] = Resources.Load<Texture>("NormalKeySprite");
		keys [Key.KeyType.SPECTRAL] = Resources.Load<Texture>("SpectralKeySprite");

        player = GameObject.Find("CharacterLeft");
        movementController = player.GetComponent<MovementController>();
        che = Instantiate(realChest) as GameObject;
          float center = 0;
        if (centre != null)
            center = centre.transform.position.x;
        else
         center = GameObject.Find("MapManager").GetComponent<MapController>().center * 0.32f;
        if (GameObject.Find("MapManager"))
            centerH = GameObject.Find("MapManager").GetComponent<MapController>().centerH;
        if (!centerH)
            che.transform.position = new Vector3(-(this.transform.position.x - center) + center, transform.position.y);
        else
            che.transform.position = new Vector3(18.60f - this.transform.position.x, transform.position.y - (17.0f * 32.0f / 100.0f));
        che.name = "DarkChest";
	}
	
	void	OnGUI() {
		GUI.skin = mySkin;

		if (mainMenu.enabled) {
			GUI.Box (new Rect (Screen.width / 2 - 170, Screen.height / 2 - 40, 390, 80), "");
			mainMenu.DisplayButtons ();
			GUI.Label (new Rect (Screen.width / 2 - 145, Screen.height / 2 - 20, 40, 40), items[characterInventoryLeft.item]);
			GUI.Label (new Rect (Screen.width / 2 + 155, Screen.height / 2 - 20, 40, 40), items[characterInventoryRight.item]);
			GUI.Label (new Rect (Screen.width / 2 - 85, Screen.height / 2 - 20, 40, 40), keys[characterInventoryLeft.key]);
			GUI.Label (new Rect (Screen.width / 2 + 95, Screen.height / 2 - 20, 40, 40), keys[characterInventoryRight.key]);
		} 
	}

	void Update () {
		if(mainMenu.enabled) {
			if(mainMenu.CheckJoystickAxis()){
				Invoke("Delay",delayBetweenFocusChanges);
			}
            int newButton = mainMenu.CheckJoystickButton();
            if (sounds.Length > 2  && newButton != currentlyPressedButton)
            {
                this.audio.clip = sounds[2];
                this.audio.Play();
            }
			currentlyPressedButton = newButton;
			if (currentlyPressedButton == 0) {
				Item.ItemType	tmp = characterInventoryLeft.item;
				
				characterInventoryLeft.item = characterInventoryRight.item;
				characterInventoryRight.item = tmp;
				return;
			}
			else if (currentlyPressedButton == 1) {
				Key.KeyType		tmp = characterInventoryLeft.key;

				characterInventoryLeft.key = characterInventoryRight.key;
				characterInventoryRight.key = tmp;
				return;
			}
			if (Input.GetButtonDown ("Validate") || !canBeOpenend) {
                if (sounds.Length > 1)
                {
                    this.audio.clip = sounds[1];
                    this.audio.Play();
                }
				mainMenu.currentFocus = 0;
				mainMenu.SetFocus(0);
				mainMenu.enabled = false;
                movementController.isGUIOpen = false;
                this.GetComponent<Animator>().SetBool("open", false);
                che.GetComponentInChildren<Animator>().SetBool("open", false);
			}
		}
		else if (Input.GetButtonDown("Validate") && canBeOpenend) {
            if (sounds.Length > 0 )
            {
                this.audio.clip = sounds[0];
                this.audio.Play();
            }
			mainMenu.enabled = true;
            movementController.isGUIOpen = true;
            this.GetComponent<Animator>().SetBool("open", true);
            che.GetComponentInChildren<Animator>().SetBool("open", true);
		}
	}

	private void Delay(){
		mainMenu.isCheckingJoy = false;
	}

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject == player)
        {
            canBeOpenend = true;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject == player)
        {
            canBeOpenend = false;
        }
    }
}
