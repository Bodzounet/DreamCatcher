using UnityEngine;
using System.Collections;

public class CharacterInventory : MonoBehaviour {
	public string		item;
	public string		side;
	public Key.KeyType	key;

	MicrophoneInput		microphoneInput;
	double				timer;
	string				blowChar;
	SpriteRenderer		spriteRenderer;
	
	// Use this for initialization
	void Start () {
		microphoneInput = GameObject.Find("MicrophoneInput").GetComponent<MicrophoneInput>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		blowChar = "BlowChar" + side;
		timer = 0;
	}

	public void	SwapItem () {
		if (item == "Flame")
			item = "Water";
		else
			item = "Flame";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("BlowCharLeft") && Input.GetButton ("BlowCharRight") && microphoneInput.loudness > 15 && timer <= 0) {
			spriteRenderer.color = new Color32(0, 255, 0, 255);
			timer = 0.75;
		}
		else if (Input.GetButton(blowChar) && microphoneInput.loudness > 15 && timer <= 0) {
			if (item == "Water")
				spriteRenderer.color = new Color32(0, 0, 255, 255);
			else if (item == "Flame")
				spriteRenderer.color = new Color32(255, 0, 0, 255);
			timer = 0.75;
		}
		if (timer > 0)
			timer -= Time.deltaTime;
	}
}
