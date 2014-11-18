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

    void OnTriggerStay2D (Collider2D other) {
        Debug.Log(other.name);
        if (other.gameObject.GetComponent<CharacterInventory>() != null)
        {
            other.gameObject.GetComponent<CharacterInventory>().item = item;
            Destroy(this.gameObject);
        }
    }
}
