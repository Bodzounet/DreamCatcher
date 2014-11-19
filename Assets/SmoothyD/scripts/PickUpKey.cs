﻿using UnityEngine;
using System.Collections;

public class PickUpKey : MonoBehaviour {
    public Key.KeyType key;
    public bool isTuto = false;
    public Texture tutoSprite_fr;
    public Texture tutoSprite_en;
    GameObject other;
    public AudioClip pickS;

    bool showTuto;
    int fr;

	// Use this for initialization
	void Start () {
        showTuto = false;
        fr = PlayerPrefs.GetInt("fr", 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (showTuto == true && Input.GetButtonDown("Validate"))
        {
            other.GetComponent<MovementController>().isGUIOpen = false;
            Destroy(this.gameObject);
        }
	}

    void OnGUI()
    {
        if (showTuto == true)
        {
            if (fr == 1)
                GUI.Label(new Rect(Screen.width / 2 - tutoSprite_fr.width / 2, Screen.height / 2 - tutoSprite_fr.height / 2, tutoSprite_fr.width, tutoSprite_fr.height), tutoSprite_fr);
            else
                GUI.Label(new Rect(Screen.width / 2 - tutoSprite_en.width / 2, Screen.height / 2 - tutoSprite_en.height / 2, tutoSprite_en.width, tutoSprite_en.height), tutoSprite_en);
        }
    }

    void OnTriggerStay2D (Collider2D c)
    {
        if (c.gameObject.GetComponent<CharacterInventory>() != null && c.gameObject.GetComponent<CharacterInventory>().key == Key.KeyType.NO_KEY)
        {
            if (pickS != null)
            {
                GameObject.Find("DreamEye").audio.clip = pickS;
                GameObject.Find("DreamEye").audio.Play();
            }
            if (isTuto == true)
            {
                other = GameObject.Find("CharacterLeft");
                c.gameObject.GetComponent<CharacterInventory>().key = key;
                other.GetComponent<MovementController>().isGUIOpen = true;
                showTuto = true;
            }
            else
            {
                c.gameObject.GetComponent<CharacterInventory>().key = key;
                Destroy(this.gameObject);
            }
        }
    }
}
