using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour 
{

    private MovementController player;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("P1").GetComponent<MovementController>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player.gameObject)
            player.dead = true;
    }
	
}
