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
    public Texture tutoSprite_fr;
    public Texture tutoSprite_en;

    Ladder linkScriptLadder;
    Door linkScriptDoor;

    bool lastState;
    GameObject other;
    bool showTuto;
    int fr;

	// Use this for initialization
	void Start () 
    {
        anim = this.GetComponent<Animator>();
        linkScriptLadder = link.GetComponent<Ladder>();
        linkScriptDoor = link.GetComponent<Door>();
        lastState = !isActive;
        showTuto = false;
        fr = PlayerPrefs.GetInt("fr", 0);
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
        if (showTuto == true && Input.GetButtonDown("Validate"))
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
        {
            if (fr == 1)
                GUI.Label(new Rect(Screen.width / 2 - tutoSprite_fr.width / 2, Screen.height / 2 - tutoSprite_fr.height / 2, tutoSprite_fr.width, tutoSprite_fr.height), tutoSprite_fr);
            else
                GUI.Label(new Rect(Screen.width / 2 - tutoSprite_en.width / 2, Screen.height / 2 - tutoSprite_en.height / 2, tutoSprite_en.width, tutoSprite_en.height), tutoSprite_en);
        }
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
