using UnityEngine;
using System.Collections;

public class Language : MonoBehaviour {

    public TextMesh txt1;
    public TextMesh txt2;
    public bool fr = true;

    void OnMouseDown()
    {
        if (fr)
        {
            fr = false;
            txt1.text = "Language";
            txt2.text = "English";
            PlayerPrefs.SetInt("fr", 0);
        }
        else
        {
            fr = true;
            txt1.text = "Langue";
            txt2.text = "Francais";
            PlayerPrefs.SetInt("fr", 1);
        }
    }
}
