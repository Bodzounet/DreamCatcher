using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public int id;
    public Transform other;
    public bool isActive;
    public AudioClip sound;
    Animator animator;
    Collider2D collider;

	// Use this for initialization
	void Start () 
    {
        isActive = true;
        animator = this.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void Activate ()
    {
        isActive = true;
        //this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void Desactivate()
    {
        //this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
        isActive = false;
    }
    
    void delayedTP()
    {
        collider.transform.position = other.position;

        //collider.transform.position += Vector3.down * 0.42f;
        collider.GetComponent<MovementController>().isEnter = true;
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (isActive && c.gameObject.name == "CharacterLeft" && Input.GetAxis("Vertical") > 0.05f && !c.GetComponent<MovementController>().isEnter)
        {
            if (this.transform.childCount > 1)
                this.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("OpenDoor");
            animator.Play("OpenDoor");
            collider = c;
            if (sound != null)
            {
                this.audio.clip = sound;
                this.audio.Play();
            }
            Invoke("delayedTP", 0.35f);
        }
    }
}
