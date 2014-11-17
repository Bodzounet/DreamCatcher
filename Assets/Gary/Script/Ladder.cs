using UnityEngine;
using System.Collections;


public class Ladder : MonoBehaviour 
{


    private MovementController player;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("CharacterLeft").GetComponent<MovementController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerStay2D (Collider2D c)
    {
        if (c.gameObject == player.gameObject)
        {
            player.ladderX = this.transform.position;
            player.isOnLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject == player.gameObject)
        {
            player.isOnLadder = false;
        }
    }
}
