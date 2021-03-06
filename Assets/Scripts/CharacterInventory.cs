﻿using UnityEngine;
using System.Collections;

public class CharacterInventory : MonoBehaviour {
	public string			side;
	public Item.ItemType	item;
	public Key.KeyType		key;
    public bool             dreamCatcher = false;

    float                   visibleTime;
	double					timer;
    public double           hiddenEntTimer;

    private BoxCollider2D   childrenBox;
    private float           animTime = 1f;

    GameObject              hiddenEntities;
    GameObject              shownEntities;
    SpriteRenderer          dreamEye;

    public bool             attack = false;
    public bool             dream = false;
    public AudioClip        yogaFlame;
    public AudioClip        yogaSouffle;
    public AudioClip        dreamCatch;
    public Animator fus;

    private MicrophoneInput micro;
	
	// Use this for initialization
	void Start () {
		timer = 0;
        hiddenEntTimer = 0;
        childrenBox = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        childrenBox.enabled = false;
        if (side == "Left")
        {
            hiddenEntities = GameObject.Find("HiddenEntities");
            shownEntities = GameObject.Find("ShownEntities");
            hiddenEntities.SetActive(false);
        }
        visibleTime = 3;
        dreamEye = GameObject.Find("DreamEye").GetComponent<SpriteRenderer>();
        dreamEye.color = new Color(1, 1, 1, 0);

        micro = GameObject.FindObjectOfType<MicrophoneInput>();
	}

    void HideEntities() {
        hiddenEntities.SetActive(false);
        shownEntities.SetActive(true);
    }

	// Update is called once per frame
	void Update () {

		if (dreamCatcher == true && ((Input.GetButton ("BlowCharLeft") && Input.GetButton ("BlowCharRight")) || Input.GetKeyDown(KeyCode.LeftControl)) && hiddenEntTimer <= 0) {
            if (side == "Left")
            {
                if (dreamCatch != null)
                {
                    dreamEye.gameObject.GetComponent<AudioSource>().clip = dreamCatch;
                    dreamEye.GetComponent<AudioSource>().Play();
                }
                dream = true;
                Invoke("resetDream", 1);
                dreamEye.color = new Color(1, 1, 1, 0.75f);
                hiddenEntities.SetActive(true);
                shownEntities.SetActive(false);
                Invoke("HideEntities", visibleTime);
                visibleTime++;
                hiddenEntTimer = visibleTime;
            }
			timer = 0.75;
		}
        else if ((Input.GetButtonDown("Blow") || (PlayerPrefs.GetInt("micro") == 1 && micro.MicLoudness > 0.3f)) && timer <= 0) 
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
            foreach (SpriteRenderer hiddenEntity in hiddenEntities.GetComponentsInChildren<SpriteRenderer> ()) {
                hiddenEntity.color = new Color(1, 1, 1, (float)(hiddenEntTimer / visibleTime) * 0.8f);
            }
        }
	}

    private void throwWater()
    {
        if (yogaSouffle != null)
        {
            this.transform.GetChild(0).GetComponent<AudioSource>().clip = yogaSouffle;
            this.transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
        attack = true;
        fus.SetTrigger("air");
        childrenBox.gameObject.tag = "Water";
        childrenBox.enabled = true;
        Invoke("stopComp", animTime);
    }

    private void throwFire()
    {
        if (yogaFlame != null)
        {
            this.transform.GetChild(0).GetComponent<AudioSource>().clip = yogaFlame;
            this.transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
        attack = true;
        fus.SetTrigger("fire");
        childrenBox.gameObject.tag = "Fire";
        childrenBox.enabled = true;
        Invoke("stopComp", animTime);
    }

    private void stopComp()
    {
        attack = false;
        childrenBox.gameObject.tag = "Untagged";
        childrenBox.enabled = false;
    }

    private void resetDream()
    {
        dream = false;
    }
}
