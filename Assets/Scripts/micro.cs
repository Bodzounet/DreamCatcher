using UnityEngine;
using System.Collections;

public class micro : MonoBehaviour {

    public bool onOff = false;

    public GameObject panel;
    public Sprite[] sprites;

    void OnMouseDown()
    {
        if (onOff)
        {
            panel.GetComponent<SpriteRenderer>().sprite = sprites[1];
            transform.gameObject.GetComponent<TextMesh>().text = "Micro       : On";
            onOff = false;
            PlayerPrefs.SetInt("micro", 1);
        }
        else
        {
            panel.GetComponent<SpriteRenderer>().sprite = sprites[0];
            onOff = true;
            transform.gameObject.GetComponent<TextMesh>().text = "Micro       : Off";
            PlayerPrefs.SetInt("micro", 0);
        }
    }
}

