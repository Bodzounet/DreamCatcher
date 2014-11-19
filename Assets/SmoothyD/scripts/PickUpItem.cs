using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    public Item.ItemType item;
    public bool isTuto = false;
    public Texture tutoSprite1;
    public Texture tutoSprite2;
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
            GUI.Label(new Rect(Screen.width / 4 - tutoSprite1.width / 2, Screen.height / 2 - tutoSprite1.height / 2, tutoSprite1.width, tutoSprite1.height), tutoSprite1);
            GUI.Label(new Rect(Screen.width / 4*3 - tutoSprite2.width / 2, Screen.height / 2 - tutoSprite2.height / 2, tutoSprite2.width, tutoSprite2.height), tutoSprite2);
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
