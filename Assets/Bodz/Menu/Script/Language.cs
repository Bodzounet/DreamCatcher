<<<<<<< HEAD
﻿using UnityEngine;
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
=======
﻿using UnityEngine;
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
>>>>>>> 092d30ba5fbd2f8772572886ce3665b36eef9556
