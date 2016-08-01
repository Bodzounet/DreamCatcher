using UnityEngine;
using System.Collections;

public class micro : MonoBehaviour {

    public bool onOff = true;

    public GameObject panel;
    public GameObject Cross;

    void OnMouseDown()
    {
        if (onOff)
        {
            Cross.SetActive(false);
            transform.gameObject.GetComponent<TextMesh>().text = "Micro            : On";
            onOff = false;
            PlayerPrefs.SetInt("micro", 1);
        }
        else
        {
            Cross.SetActive(true);
            onOff = true;
            transform.gameObject.GetComponent<TextMesh>().text = "Micro            : Off";
            PlayerPrefs.SetInt("micro", 0);
        }
    }
}

