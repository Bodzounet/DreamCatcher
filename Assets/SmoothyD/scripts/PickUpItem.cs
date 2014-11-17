using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    public Item.ItemType item;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D (Collider2D other) {
        other.gameObject.GetComponent<CharacterInventory>().item = item;
        Destroy(this.gameObject);
    }
}
