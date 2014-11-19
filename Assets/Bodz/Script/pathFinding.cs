using UnityEngine;
using System.Collections;

public class pathFinding : MonoBehaviour {
    
    public float speed = 0.02f;
    private Transform trans;

    private float lastX;
    private MovementController.e_dir lastDir = MovementController.e_dir.RIGHT;
    private float scaleX;

    private Animator anim;
    public AudioClip[] sounds;
    
    // Use this for initialization
    void Start () 
    {
        trans = GameObject.Find("CharacterLeft").GetComponent<Transform>();
        lastX = transform.position.x;
        scaleX = transform.localScale.x;

        anim = this.GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update () 
    {
        MovementController.e_dir dir = MovementController.e_dir.NONE;

        transform.position = Vector2.MoveTowards(this.transform.position, trans.position, speed * Time.deltaTime);
        if (trans.position.x < transform.position.x)
            dir = MovementController.e_dir.LEFT;
        else if (trans.position.x > transform.position.x)
            dir = MovementController.e_dir.RIGHT;
        lastX = transform.position.x;

        if (dir == MovementController.e_dir.LEFT)
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);

        if (Vector2.Distance(trans.position, transform.position) > 2f)
        {
            anim.SetBool("mustAttack", false);
            if (sounds.Length > 0 && sounds[0] != null && !this.audio.isPlaying)
            {
                this.audio.clip = sounds[0];
                this.audio.Play();
                this.transform.GetChild(0).audio.Stop();
            }
        }
        else
        {
            anim.SetBool("mustAttack", true);
            if (sounds.Length > 1 && sounds[1] != null && !this.transform.GetChild(0).audio.isPlaying )
            {
                this.transform.GetChild(0).audio.clip = sounds[1];
                this.transform.GetChild(0).audio.Play();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Target")
        {
            col.GetComponent<MovementController>().dead = true;
        }
    }
}