using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public int id;
    public Transform other;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.name == "CharacterLeft" && Input.GetAxis("Vertical") > 0.05 && !c.GetComponent<MovementController>().isEnter)
        {
            c.transform.position = other.position;
            c.transform.position += Vector3.down * 0.42f;
            c.GetComponent<MovementController>().isEnter = true;
        }
    }
}
