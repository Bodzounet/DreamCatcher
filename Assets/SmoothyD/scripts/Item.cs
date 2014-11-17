using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
	public string				side;

	CharacterInventory			characterInventory;
	SpriteRenderer				spriteRenderer;
	Dictionary<string, Sprite>	items = new Dictionary<string, Sprite>();

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		characterInventory = GameObject.Find ("Character" + side).GetComponent<CharacterInventory>();
		items ["Flame"] = Resources.Load<Sprite>("Flame");
		items ["Water"] = Resources.Load<Sprite>("Water");
	}
	
	// Update is called once per frame
	void Update () {
		spriteRenderer.sprite = items[characterInventory.item];
	}
}
