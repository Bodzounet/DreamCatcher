﻿using UnityEngine;
using System.Collections;

public class switchTorchMode : MonoBehaviour
{

    public bool isActive = false;
    public Animator anim;
    public GameObject link;
    public bool invert = false;

    Ladder linkScriptLadder;
    Door linkScriptDoor;
    bool lastState;

	// Use this for initialization
	void Start () 
    {
        anim = this.GetComponent<Animator>();
        linkScriptLadder = link.GetComponent<Ladder>();
        linkScriptDoor = link.GetComponent<Door>();
        lastState = !isActive;
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
	}

    public void Invert()
    {
        isActive = true;
        invert = true;
        anim.SetBool("isActive", true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Fire")
        {
            isActive = true;
        }
        else if (col.tag == "Water")
        {
            isActive = false;
        }
    }
}
