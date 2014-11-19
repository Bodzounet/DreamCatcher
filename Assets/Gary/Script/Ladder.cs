using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ladder : MonoBehaviour 
{
    public bool isActive;
    private MovementController player;
    public List<SpriteRenderer> spriteRenderer = new List<SpriteRenderer>();
    public Sprite[] chip;
    public bool isStatic = false;
    public int height;
    float timer;

	// Use this for initialization
	void Start () 
    {
        //isActive = true;
        player = GameObject.Find("CharacterLeft").GetComponent<MovementController>();
        if (!isActive)
            this.GetComponent<BoxCollider2D>().enabled = false;

        if (this.transform.childCount > 0 && this.transform.GetChild(0).GetComponent<Ladder>() != null)
        {
            spriteRenderer = this.transform.GetChild(0).GetComponent<Ladder>().spriteRenderer;
            this.GetComponent<BoxCollider2D>().size = new Vector2(0.32f, this.transform.GetChild(0).GetComponent<BoxCollider2D>().size.y);
            Destroy(this.transform.GetChild(0).GetComponent<Ladder>());
        }
        if (isStatic)
        {
            setHeight(height);
        }
        timer = 1;
	}
	
    public void setHeight(int height)
    {
        for (int i =  -height / 2 - 1; i < height / 2; i++)
        {
            GameObject child = new GameObject();
            child.transform.parent = this.transform;
            if (i ==  -height / 2 - 1)
                child.AddComponent<SpriteRenderer>().sprite = chip[0];
            else if (i == height / 2 - 1)
                child.AddComponent<SpriteRenderer>().sprite = chip[2];
            else
                child.AddComponent<SpriteRenderer>().sprite = chip[1];
            child.transform.localPosition = new Vector3(0, -i * 0.32f, 0);
            child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            spriteRenderer.Add(child.GetComponent<SpriteRenderer>());
        }
        if (this.transform.parent == null || this.transform.parent.name != "hiddenEntities")
            this.GetComponent<BoxCollider2D>().size = new Vector2(0.32f, 0.32f * height);
        else
            this.transform.parent.GetComponent<BoxCollider2D>().size = new Vector2(0.32f, 0.32f * height);
    }
	// Update is called once per frame
	void Update () 
    {
        if (isActive)
        {
            foreach (SpriteRenderer s in spriteRenderer)
                s.color = new Color(1, 1, 1, timer);
            timer += Time.deltaTime;
        }
        else
        {
            foreach (SpriteRenderer s in spriteRenderer)
                s.color = new Color(1, 1, 1, 0);
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
        {
            foreach (SpriteRenderer s in spriteRenderer)
            s.color = new Color(1, 1, 1, 0);
        }
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
