using UnityEngine;
using System.Collections;

public class switchTorchMode : MonoBehaviour
{

    private bool isActive = false;
    public Animator anim;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Fire")
        {
            isActive = true;
            anim.SetBool("Active", true);
        }
        else if (col.tag == "Water")
        {
            isActive = false;
            anim.SetBool("Active", false);
        }
    }
}
