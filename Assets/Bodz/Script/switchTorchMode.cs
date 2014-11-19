using UnityEngine;
using System.Collections;

public class switchTorchMode : MonoBehaviour
{

    public bool isActive = false;
    public Animator anim;
    public GameObject link;
    public bool invert = false;
    public AudioClip[] sounds;
    public bool isTuto = false;
    public Texture tutoSprite;

    Ladder linkScriptLadder;
    Door linkScriptDoor;

    bool lastState;
    GameObject other;
    bool showTuto;
    float timer;

	// Use this for initialization
	void Start () 
    {
        anim = this.GetComponent<Animator>();
        linkScriptLadder = link.GetComponent<Ladder>();
        linkScriptDoor = link.GetComponent<Door>();
        lastState = !isActive;
        showTuto = false;
        timer = 5;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (link != null && lastState != isActive)
        {
            if ((invert && isActive) || (!invert && !isActive))
            {
                if (linkScriptDoor != null)
                    linkScriptDoor.Desactivate();
                else
                    linkScriptLadder.Desactivate();
            }
            else
            {
                if (linkScriptDoor != null)
                    linkScriptDoor.Activate();
                else
                    linkScriptLadder.Activate();
            }
        }
        anim.SetBool("isActive", isActive);
        lastState = isActive;
        if (showTuto == true && Input.GetButtonDown("Jump"))
            timer = 0;
        if (showTuto == true && timer > 0)
            timer -= Time.deltaTime;
        else if (showTuto == true)
        {
            other.GetComponent<MovementController>().isGUIOpen = false;
            isTuto = false;
            showTuto = false;
        }
	}

    public void Invert()
    {
        isActive = true;
        invert = true;
        anim.SetBool("isActive", true);
    }

    void OnGUI()
    {
        if (showTuto == true)
            GUI.Label(new Rect(Screen.width / 2 - tutoSprite.width / 2, Screen.height / 2 - tutoSprite.height / 2, tutoSprite.width, tutoSprite.height), tutoSprite);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Fire" && !isActive)
        {
            isActive = true;
            if (sounds.Length > 0)
            {
                this.audio.clip = sounds[0];
                this.audio.Play();
            }
        }
        else if (col.tag == "Water" && isActive)
        {
            isActive = false;
            if (sounds.Length > 1)
            {
                this.audio.clip = sounds[1];
                this.audio.Play();
            }
        }
        else if (isTuto == true && col.gameObject.GetComponent<CharacterInventory>() != null && col.gameObject.GetComponent<CharacterInventory>().key == Key.KeyType.NO_KEY)
        {
            other = GameObject.Find("CharacterLeft");
            other.GetComponent<MovementController>().isGUIOpen = true;
            showTuto = true;
        }

    }
}
