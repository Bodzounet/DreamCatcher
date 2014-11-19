using UnityEngine;
using System.Collections;

public class clickOptions : MonoBehaviour {

    public GameObject go;
    public GameObject otherGo;

    void Start ()
    {
        PlayerPrefs.SetInt("fr", 1);
        PlayerPrefs.SetInt("micro", 1);
    }

    void OnMouseDown()
    {
        if (go.active == false)
        {
            otherGo.SetActive(false);
            go.SetActive(true);
        }
        else
            go.SetActive(false);
    }
}
