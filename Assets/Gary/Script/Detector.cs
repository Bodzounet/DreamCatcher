using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {

    public Key.KeyType type;
    private GameObject j2;
	// Use this for initialization
	void Start () 
    {
        j2 = GameObject.Find("CharacterRight");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.GetComponent<CharacterInventory>() != null && (c.GetComponent<CharacterInventory>().key == type))
        {
            this.transform.parent.collider2D.enabled = false;
            this.GetComponentInParent<SpriteRenderer>().enabled = false;
            this.collider2D.enabled = false;
            c.GetComponent<CharacterInventory>().key = Key.KeyType.NO_KEY;
        }
    }
}
