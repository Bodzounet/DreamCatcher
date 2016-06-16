using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {
	public GameObject	micVolume;

	SpriteRenderer		spriteRenderer;
	MicrophoneInput		microphoneInput;
	Color32[]			colors = new Color32[3];
	double				timer;

	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		microphoneInput = micVolume.GetComponent<MicrophoneInput>();
		colors[0] = new Color32(255, 0, 0, 255);
		colors[1] = new Color32(0, 255, 0, 255);
		colors[2] = new Color32(0, 0, 255, 255);
		timer = 0;
	}
	
	void Update () {
		if (Input.GetButton ("BlowCharLeft") && Input.GetButton ("BlowCharRight") && microphoneInput.loudness > 15 && timer <= 0) {
			spriteRenderer.color = colors[2];
		}
		else if (Input.GetButton("BlowCharRight") && microphoneInput.loudness > 15 && timer <= 0) {
			spriteRenderer.color = colors[1];
			timer = 0.75;
		}
		else if (Input.GetButton("BlowCharLeft") && microphoneInput.loudness > 15 && timer <= 0) {
			spriteRenderer.color = colors[0];
			timer = 0.75;
		}
		if (timer > 0)
			timer -= Time.deltaTime;
	}
}
