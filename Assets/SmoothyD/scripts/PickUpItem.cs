using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    public Item.ItemType item;
    public bool isTuto = false;
    public Texture tutoSprite;
    GameObject other;

    bool showTuto;
    float timer;

	// Use this for initialization
	void Start () {
        showTuto = false;
        timer = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (showTuto == true && Input.GetButtonDown("Jump"))
            timer = 0;
        if (showTuto == true && timer > 0)
            timer -= Time.deltaTime;
        else if (showTuto == true)
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
            Debug.Log(tutoSprite.width + " " + tutoSprite.height);
            GUI.Label(new Rect(Screen.width / 2 - tutoSprite.width / 2, Screen.height / 2 - tutoSprite.height / 2, tutoSprite.width, tutoSprite.height), tutoSprite);
        }
    }

    void OnTriggerStay2D (Collider2D c) {
        if (c.gameObject.GetComponent<CharacterInventory>() != null)
        {
            if (isTuto == true)
            {

                other = GameObject.Find("CharacterLeft");
                other.GetComponent<MovementController>().isGUIOpen = true;
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
