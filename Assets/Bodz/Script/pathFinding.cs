using UnityEngine;
using System.Collections;

public class pathFinding : MonoBehaviour {

    public GameObject player;
    public float speed = 1f;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
	}
}
