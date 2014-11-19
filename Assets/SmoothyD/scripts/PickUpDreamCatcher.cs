using UnityEngine;
using System.Collections;

public class PickUpDreamCatcher : MonoBehaviour {
    public bool isTuto = false;
    public Texture tutoSprite_fr;
    public Texture tutoSprite_en;
    GameObject other;

    bool showTuto;
    float timer;
    int fr;

    // Use this for initialization
    void Start()
    {
        showTuto = false;
        timer = 5;
        fr = PlayerPrefs.GetInt("fr", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (showTuto == true && Input.GetButtonDown("Jump"))
            timer = 0;
        if (showTuto == true && timer > 0)
            timer -= Time.deltaTime;
        else if (showTuto == true)
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

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.GetComponent<CharacterInventory>() != null)
        {
            c.gameObject.GetComponent<CharacterInventory>().dreamCatcher = true;
            if (isTuto == true)
            {

                other = GameObject.Find("CharacterLeft");
                other.GetComponent<MovementController>().isGUIOpen = true;
                showTuto = true;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
