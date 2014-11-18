using UnityEngine;
using System.Collections;

public class PickUpKey : MonoBehaviour {
    public Key.KeyType key;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerStay2D (Collider2D other)
    {
        if (other.gameObject.GetComponent<CharacterInventory>() != null && other.gameObject.GetComponent<CharacterInventory>().key == Key.KeyType.NO_KEY)
        {
            other.gameObject.GetComponent<CharacterInventory>().key = key;
            Destroy(this.gameObject);
        }
    }
}
