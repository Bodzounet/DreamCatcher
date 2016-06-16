using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    public Item.ItemType item;
    public bool isTuto = false;
    public Texture tutoSprite1_fr;
    public Texture tutoSprite2_fr;
    public Texture tutoSprite1_en;
    public Texture tutoSprite2_en;
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
            other.GetComponent<CharacterInventory>().item = item;
            other.GetComponent<MovementController>().isGUIOpen = false;
            Destroy(this.gameObject);
        }
	}

    void OnGUI()
    {
        if (showTuto == true)
        {
            if (fr == 1)
            {
                GUI.Label(new Rect(Screen.width / 4 - tutoSprite1_fr.width / 2, Screen.height / 2 - tutoSprite1_fr.height / 2, tutoSprite1_fr.width, tutoSprite1_fr.height), tutoSprite1_fr);
                GUI.Label(new Rect(Screen.width / 4 * 3 - tutoSprite2_fr.width / 2, Screen.height / 2 - tutoSprite2_fr.height / 2, tutoSprite2_fr.width, tutoSprite2_fr.height), tutoSprite2_fr);
            }
            else
            {
                GUI.Label(new Rect(Screen.width / 4 - tutoSprite1_en.width / 2, Screen.height / 2 - tutoSprite1_en.height / 2, tutoSprite1_en.width, tutoSprite1_en.height), tutoSprite1_en);
                GUI.Label(new Rect(Screen.width / 4 * 3 - tutoSprite2_en.width / 2, Screen.height / 2 - tutoSprite2_en.height / 2, tutoSprite2_en.width, tutoSprite2_en.height), tutoSprite2_en);
            }
        }
    }

    void OnTriggerStay2D (Collider2D c) {
        if (c.gameObject.GetComponent<CharacterInventory>() != null)
        {

            if (isTuto == true)
            {

                other = GameObject.Find("CharacterLeft");
                other.GetComponent<MovementController>().isGUIOpen = true;
                if (pickS != null && !showTuto)
                {
                    GameObject.Find("DreamEye").GetComponent<AudioSource>().clip = pickS;
                    GameObject.Find("DreamEye").GetComponent<AudioSource>().Play();
                }
                showTuto = true;
            }
            else
            {
                c.gameObject.GetComponent<CharacterInventory>().item = item;
                Destroy(this.gameObject);
            }
        }
    }
}
