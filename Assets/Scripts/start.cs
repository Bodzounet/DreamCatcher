using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour {

    public GameObject Eye;
    public AudioClip darkness;
    void OnMouseDown()
    {
        Eye.SetActive(true);
        if (darkness != null)
        {
            Camera.main.GetComponent<AudioSource>().clip = darkness;
            Camera.main.GetComponent<AudioSource>().Play();
        }

        Eye.GetComponent<Animator>().SetBool("dead", true);
        //Application.loadedLevel();
    }

    void Update()
    {
        if (Eye.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
