using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour 
{

    private MovementController player;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("CharacterLeft").GetComponent<MovementController>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player.gameObject)
            player.dead = true;
    }
	
}
