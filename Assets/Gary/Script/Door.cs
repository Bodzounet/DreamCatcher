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
    }

    public void Desactivate()
    {
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
        if (isActive && c.gameObject.name == "CharacterLeft" && Input.GetAxis("Vertical") > 0.05 && !c.GetComponent<MovementController>().isEnter)
        {
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
