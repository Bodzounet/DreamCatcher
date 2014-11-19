using UnityEngine;
using System.Collections;

public class micro : MonoBehaviour {

    public TextMesh txt;
    public bool onOff = true;

    public GameObject panel;
    public Sprite[] spirtes;

    void OnMouseDown()
    {
        if (onOff)
        {
            panel.GetComponent<SpriteRenderer>().sprite = spirtes[0];
            onOff = false;
            txt.text = "Off";
            PlayerPrefs.SetInt("micro", 0);
        }
        else
        {
            panel.GetComponent<SpriteRenderer>().sprite = spirtes[1];
            onOff = true;
            txt.text = "On";
            PlayerPrefs.SetInt("micro", 1);
        }
    }
}
