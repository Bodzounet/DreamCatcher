using UnityEngine;
using System.Collections;

public class micro : MonoBehaviour {

    public bool onOff = true;

    public GameObject panel;
    public Sprite[] spirtes;

    void OnMouseDown()
    {
        if (onOff)
        {
            panel.GetComponent<SpriteRenderer>().sprite = spirtes[1];
            transform.gameObject.GetComponent<TextMesh>().text = "Micro       : On";
            onOff = false;
            PlayerPrefs.SetInt("micro", 0);
        }
        else
        {
            panel.GetComponent<SpriteRenderer>().sprite = spirtes[0];
            onOff = true;
            transform.gameObject.GetComponent<TextMesh>().text = "Micro       : Off";
            PlayerPrefs.SetInt("micro", 1);
        }
    }
}
