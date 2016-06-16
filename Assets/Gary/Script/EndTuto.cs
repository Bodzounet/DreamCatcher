using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndTuto : MonoBehaviour {

    public GameObject eye;
    private bool ending = false;
    public int nextScene;
    public string anim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (ending == true && eye.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            SceneManager.LoadScene(nextScene);
        }
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<MovementController>() != null)
        {
            c.GetComponent<MovementController>().stop = true;
            eye.GetComponent<Animator>().SetBool(anim, true);
            ending = true;
        }
    }
}
