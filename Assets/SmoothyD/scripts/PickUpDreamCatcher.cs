using UnityEngine;
using System.Collections;

public class PickUpDreamCatcher : MonoBehaviour {
    public bool isTuto = false;
    public Texture tutoSprite;
    GameObject other;

    bool showTuto;
    float timer;

    // Use this for initialization
    void Start()
    {
        showTuto = false;
        timer = 5;
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
            GUI.Label(new Rect(Screen.width / 2 - tutoSprite.width / 2, Screen.height / 2 - tutoSprite.height / 2, tutoSprite.width, tutoSprite.height), tutoSprite);
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
