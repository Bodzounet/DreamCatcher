using UnityEngine;
using System.Collections;

public class micro : MonoBehaviour {

    public TextMesh txt;
    public bool onOff = true;

    void OnMouseDown()
    {
        if (onOff)
        {
            onOff = false;
            txt.text = "Off";
            PlayerPrefs.SetInt("micro", 0);
        }
        else
        {
            onOff = true;
            txt.text = "On";
            PlayerPrefs.SetInt("micro", 1);
        }
    }
}
