using UnityEngine;
using System.Collections;

public class MonsterFactory : MonoBehaviour {

    public GameObject model;

    private Animator anim;
    private CharacterInventory ci;

    private bool wait = false;
    private float timeBeforeDeath = 3f;
	// Use this for initialization
	void Start () 
    {
        anim = this.gameObject.GetComponent<Animator>();
        ci = GameObject.Find("CharacterLeft").GetComponent<CharacterInventory>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (ci.dream && !wait)
        {
            anim.SetBool("spawn", true);
            Invoke("popMonster", 1.5f);
            wait = true;
        }
        else if (ci.hiddenEntTimer < 0)
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
            wait = false;
        }
	}

    private void popMonster()
    {
        GameObject monster;

        anim.SetBool("spawn", false);
        this.gameObject.GetComponent<Renderer>().enabled = false;
        monster = Instantiate(model, transform.position, model.transform.rotation) as GameObject;
        Destroy(monster, timeBeforeDeath);
        timeBeforeDeath += 1f;
    }
}
