using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{

    private GameObject player;
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerStay2D (Collider2D c)
    {
        if (c.gameObject == player)
        {
            player.GetComponent<MovementController>().isOnLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject == player)
        {
            player.GetComponent<MovementController>().isOnLadder = false;
        }
    }
}
