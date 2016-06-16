using UnityEngine;
using System.Collections;

public class HiddenEntity : MonoBehaviour {
    double timer;
    SpriteRenderer spriteRenderer;
    BoxCollider2D _collider;

	// Use this for initialization
	void Start () {
        _collider = this.GetComponent<BoxCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        _collider.isTrigger = true;
        spriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
            timer -= Time.deltaTime;
        else {
            _collider.isTrigger = true;
            spriteRenderer.enabled = false;
        }
	}

    public void Reveal()
    {
        Debug.Log("test");
        _collider.isTrigger = false;
        spriteRenderer.enabled = true;
        timer = 5;
    }
}
