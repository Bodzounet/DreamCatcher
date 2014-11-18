using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {

    public Key.KeyType type;
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
        if (c.GetComponent<CharacterInventory>() != null && c.GetComponent<CharacterInventory>().key == type)
        {
            this.transform.parent.collider2D.enabled = false;
            this.collider2D.enabled = false;
        }
    }
}
