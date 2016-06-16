using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour {

    public Sprite end, fin;
    public GameObject Eye;
	// Use this for initialization
	void Start () {
	    if (PlayerPrefs.GetInt("fr") == 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = end;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = fin;
        }
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, Time.deltaTime / 2);
        if (this.GetComponent<SpriteRenderer>().color.a > 1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            if (Input.GetButton("Jump"))
            {
                Eye.GetComponent<Animator>().SetBool("dead", true);
            }
            if (Eye.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("close"))
            {
                SceneManager.LoadScene(0);
            }
        }
	}
}
