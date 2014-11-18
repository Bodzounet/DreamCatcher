using UnityEngine;
using System.Collections;

public class CharacterInventory : MonoBehaviour {
	public string			side;
	public Item.ItemType	item;
	public Key.KeyType		key;

    float                   visibleTime;
	MicrophoneInput			microphoneInput;
	double					timer;
    double                  hiddenEntTimer;
	string					blowChar;
	SpriteRenderer			spriteRenderer;
    GameObject              hiddenEntities;
    SpriteRenderer          dreamEye;
	
	// Use this for initialization
	void Start () {
		microphoneInput = GameObject.Find("MicrophoneInput").GetComponent<MicrophoneInput>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		blowChar = "BlowChar" + side;
		timer = 0;
        hiddenEntTimer = 0;
        if (side == "Left")
        {
            hiddenEntities = GameObject.Find("HiddenEntities");
            hiddenEntities.SetActive(false);
        }
        visibleTime = 3;
        dreamEye = GameObject.Find("DreamEye").GetComponent<SpriteRenderer>();
        dreamEye.color = new Color(1, 1, 1, 0);
	}

    void HideEntities() {
        hiddenEntities.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("BlowCharLeft") && Input.GetButton ("BlowCharRight") && microphoneInput.loudness > 15 && hiddenEntTimer <= 0) {
            if (side == "Left")
            {
                dreamEye.color = new Color(1, 1, 1, 0.75f);
                hiddenEntities.SetActive(true);
                Invoke("HideEntities", visibleTime);
                visibleTime++;
                hiddenEntTimer = visibleTime;
            }
			timer = 0.75;
		}
		else if (Input.GetButton(blowChar) && microphoneInput.loudness > 15 && timer <= 0) {
			if (item == Item.ItemType.WATER)
				spriteRenderer.color = new Color32(0, 0, 255, 255);
			else if (item == Item.ItemType.FLAME)
				spriteRenderer.color = new Color32(255, 0, 0, 255);
			timer = 0.75;
		}
        if (timer > 0)
            timer -= Time.deltaTime;
        if (hiddenEntTimer > 0) {
            hiddenEntTimer -= Time.deltaTime;
            dreamEye.color = new Color(1, 1, 1, (float)(hiddenEntTimer / visibleTime) * 0.8f);
        }
	}
}
