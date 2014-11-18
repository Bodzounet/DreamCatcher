using UnityEngine;
using System.Collections;

public class switchTorchMode : MonoBehaviour
{

    public bool isActive = false;
    public Animator anim;
    public GameObject link;
    public bool invert = false;
	// Use this for initialization
	void Start () 
    {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (link != null)
        {
                if (invert)
                    link.SetActive(!isActive);
                else
                    link.SetActive(isActive);
        }
        anim.SetBool("isActive", isActive);
	}

    public void Invert()
    {
        isActive = true;
        invert = true;
        anim.SetBool("isActive", true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Fire")
        {
            isActive = true;
        }
        else if (col.tag == "Water")
        {
            isActive = false;
        }
    }
}
