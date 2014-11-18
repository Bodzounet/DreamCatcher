using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Key : MonoBehaviour {
	public string				side;
	public enum KeyType {
		NO_KEY,
		NORMAL,
		SPECTRAL
	};

	CharacterInventory			characterInventory;
	SpriteRenderer				spriteRenderer;
	Dictionary<KeyType, Sprite>	items = new Dictionary<KeyType, Sprite>();

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		characterInventory = GameObject.Find ("Character" + side).GetComponent<CharacterInventory>();
		items [KeyType.NO_KEY] = Resources.Load<Sprite>("NoKey");
		items [KeyType.NORMAL] = Resources.Load<Sprite>("NormalKeySprite");
		items [KeyType.SPECTRAL] = Resources.Load<Sprite>("SpectralKeySprite");
	}
	
	// Update is called once per frame
	void Update () {
		spriteRenderer.sprite = items[characterInventory.key];
	}
}
