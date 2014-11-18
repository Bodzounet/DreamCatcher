using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public int id;
    public Transform other;
    public bool isActive;

	// Use this for initialization
	void Start () 
    {
        isActive = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Activate ()
    {
        isActive = true;
    }

    public void Desactivate()
    {
        isActive = false;
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (isActive && c.gameObject.name == "CharacterLeft" && Input.GetAxis("Vertical") > 0.05 && !c.GetComponent<MovementController>().isEnter)
        {
            c.transform.position = other.position;
            c.transform.position += Vector3.down * 0.42f;
            c.GetComponent<MovementController>().isEnter = true;
        }
    }
}
