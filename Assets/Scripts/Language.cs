using UnityEngine;
using System.Collections;

public class Language : MonoBehaviour {

    public bool fr = true;

    public TextMesh t1;
    public TextMesh t2;

    void OnMouseDown()
    {
        if (fr)
        {
            fr = false;
            gameObject.GetComponent<TextMesh>().text = "Language  : English";
            t1.text = "Play";
            t2.text = "Quit";
            PlayerPrefs.SetInt("fr", 0);
        }
        else
        {
            fr = true;
            gameObject.GetComponent<TextMesh>().text = "Langue     : Français";
            t1.text = "Jouer";
            t2.text = "Quitter";
            PlayerPrefs.SetInt("fr", 1);
        }
    }
}
