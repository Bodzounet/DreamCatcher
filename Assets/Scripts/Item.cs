using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
	public string					side;
	public enum ItemType {
		NO_ITEM,
		WATER,
		FLAME
	};
	
	CharacterInventory				characterInventory;
	SpriteRenderer					spriteRenderer;
	Dictionary<ItemType, Sprite>	items = new Dictionary<ItemType, Sprite>();

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		characterInventory = GameObject.Find ("Character" + side).GetComponent<CharacterInventory>();
		items [ItemType.NO_ITEM] = Resources.Load<Sprite>("NoItem");
		items [ItemType.FLAME] = Resources.Load<Sprite>("FlameSprite");
		items [ItemType.WATER] = Resources.Load<Sprite>("WaterSprite");
	}
	
	// Update is called once per frame
	void Update () {
		spriteRenderer.sprite = items[characterInventory.item];
	}
}
