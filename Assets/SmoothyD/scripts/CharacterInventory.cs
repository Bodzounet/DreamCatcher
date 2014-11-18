﻿using UnityEngine;
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

    private BoxCollider2D   childrenBox;
    private float           animTime = 2f;

    GameObject              hiddenEntities;
    SpriteRenderer          dreamEye;
	
	// Use this for initialization
	void Start () {
		microphoneInput = GameObject.Find("MicrophoneInput").GetComponent<MicrophoneInput>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		blowChar = "BlowChar" + side;
		timer = 0;
        hiddenEntTimer = 0;
        childrenBox = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        childrenBox.enabled = false;
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
		if (Input.GetButton ("BlowCharLeft") && Input.GetButton ("BlowCharRight") && hiddenEntTimer <= 0) {
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
		else if (microphoneInput.loudness > 15 && timer <= 0) 
        {
            if (item == Item.ItemType.WATER)
            {
                throwWater();
            }
            else if (item == Item.ItemType.FLAME)
            {
                throwFire();
            }
			timer = 0.75;
		}
        if (timer > 0)
            timer -= Time.deltaTime;
        if (hiddenEntTimer > 0) {
            hiddenEntTimer -= Time.deltaTime;
            dreamEye.color = new Color(1, 1, 1, (float)(hiddenEntTimer / visibleTime) * 0.8f);
        }
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
