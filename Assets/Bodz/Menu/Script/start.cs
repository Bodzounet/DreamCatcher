using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

    public GameObject Eye;
    public AudioClip darkness;
    void OnMouseDown()
    {
        Eye.SetActive(true);
        if (darkness != null)
        {
            Camera.main.audio.clip = darkness;
            Camera.main.audio.Play();
        }

        Eye.GetComponent<Animator>().SetBool("dead", true);
        //Application.loadedLevel();
    }

    void Update()
    {
        if (Eye.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            Application.LoadLevel(1);
        }
    }
}
