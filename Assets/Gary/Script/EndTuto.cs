﻿using UnityEngine;
using System.Collections;

public class EndTuto : MonoBehaviour {

    public GameObject eye;
    private bool ending = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (ending == true && eye.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            Application.LoadLevel(2);
        }
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<MovementController>() != null)
        {
            c.GetComponent<MovementController>().stop = true;
            eye.GetComponent<Animator>().SetBool("tuto", true);
            ending = true;
        }
    }
}
