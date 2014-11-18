using UnityEngine;
using System.Collections;

public class CharacterInventory : MonoBehaviour {
	public string			side;
	public Item.ItemType	item;
	public Key.KeyType		key;

    float                   visibleTime;
	MicrophoneInput			microphoneInput;
	double					timer;
	string					blowChar;
	SpriteRenderer			spriteRenderer;

    private BoxCollider2D   childrenBox;
    private float           animTime = 2f;

    GameObject              hiddenEntities;
	
	// Use this for initialization
	void Start () {
		microphoneInput = GameObject.Find("MicrophoneInput").GetComponent<MicrophoneInput>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		blowChar = "BlowChar" + side;
		timer = 0;

        childrenBox = GetComponentInChildren<BoxCollider2D>();
        childrenBox.enabled = false;
        if (side == "Left")
        {
            hiddenEntities = GameObject.Find("HiddenEntities");
            hiddenEntities.SetActive(false);
        }
        visibleTime = 3;
	}

    void HideEntities() {
        hiddenEntities.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("BlowCharLeft") && Input.GetButton ("BlowCharRight") && microphoneInput.loudness > 15 && timer <= 0) {
            if (side == "Left")
            {
                hiddenEntities.SetActive(true);
                Invoke("HideEntities", visibleTime);
                visibleTime++;
            }
			timer = 0.75;
		}
		else if (Input.GetButton(blowChar) && microphoneInput.loudness > 15 && timer <= 0) 
        {
            if (item == Item.ItemType.WATER)
            {
                throwWater();
                spriteRenderer.color = new Color32(0, 0, 255, 255);
            }
            else if (item == Item.ItemType.FLAME)
            {
                throwFire();
                spriteRenderer.color = new Color32(255, 0, 0, 255);
            }
			timer = 0.75;
		}
        if (timer > 0)
            timer -= Time.deltaTime;
	}

    private void throwWater()
    {
        //todo : jouer l'anim de l'eau
        childrenBox.gameObject.tag = "Water";
        childrenBox.enabled = true;
        Invoke("stopComp", animTime);
    }

    private void throwFire()
    {
        //todo : jouer l'anim du feu
        childrenBox.gameObject.tag = "Fire";
        childrenBox.enabled = true;
        Invoke("stopComp", animTime);
    }

    private void stopComp()
    {
        childrenBox.gameObject.tag = "Untagged";
        childrenBox.enabled = false;
    }
}
