using UnityEngine;
using System.Collections;

public class HiddenEntity : MonoBehaviour {
    double timer;
    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;

	// Use this for initialization
	void Start () {
        collider = this.GetComponent<BoxCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        collider.isTrigger = true;
        spriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
            timer -= Time.deltaTime;
        else {
            collider.isTrigger = true;
            spriteRenderer.enabled = false;
        }
	}

    public void Reveal()
    {
        Debug.Log("test");
        collider.isTrigger = false;
        spriteRenderer.enabled = true;
        timer = 5;
    }
}
