using UnityEngine;
using System.Collections;


public class Ladder : MonoBehaviour 
{
    public bool isActive;
    private MovementController player;
    SpriteRenderer spriteRenderer;
    float timer;

	// Use this for initialization
	void Start () 
    {
        isActive = true;
        player = GameObject.Find("CharacterLeft").GetComponent<MovementController>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.GetComponent<BoxCollider2D>().enabled = false;
        if (this.transform.childCount > 0)
        {
            spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
            Destroy(this.transform.GetChild(0).GetComponent<Ladder>());
        }
        timer = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isActive)
        {
            spriteRenderer.color = new Color(1, 1, 1, timer);
            timer += Time.deltaTime;
        }
	}

    public void Activate ()
    {
        isActive = true;
        timer = 0;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Desactivate()
    {
        isActive = false;
        if (spriteRenderer != null)
        spriteRenderer.color = new Color(1, 1, 1, 0);
        timer = 0;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnTriggerStay2D (Collider2D c)
    {
        if (c.gameObject == player.gameObject)
        {
            player.ladderX = this.transform.position;
            player.isOnLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject == player.gameObject)
        {
            player.isOnLadder = false;
        }
    }
}
