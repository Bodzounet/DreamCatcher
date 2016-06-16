using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public int id;
    public Transform other;
    public bool isActive;
    public AudioClip sound;
    Animator animator;
    Collider2D _collider;

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
        _collider.transform.position = other.position;

        //collider.transform.position += Vector3.down * 0.42f;
        _collider.GetComponent<MovementController>().isEnter = true;
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (isActive && c.gameObject.name == "CharacterLeft" && Input.GetAxis("Vertical") > 0.05f && !c.GetComponent<MovementController>().isEnter)
        {
            if (this.transform.childCount > 1)
                this.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("OpenDoor");
            animator.Play("OpenDoor");
            _collider = c;
            if (sound != null)
            {
                this.GetComponent<AudioSource>().clip = sound;
                this.GetComponent<AudioSource>().Play();
            }
            Invoke("delayedTP", 0.35f);
        }
    }
}
