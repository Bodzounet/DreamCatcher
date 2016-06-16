﻿using UnityEngine;
using System.Collections;

public class clickOptions : MonoBehaviour {

    public GameObject go;
    public GameObject otherGo;

    void Start ()
    {
        PlayerPrefs.SetInt("fr", 1);
        PlayerPrefs.SetInt("micro", 0);
    }

    void OnMouseDown()
    {
        if (go.activeSelf == false)
        {
            otherGo.SetActive(false);
            go.SetActive(true);
        }
        else
            go.SetActive(false);
    }
}
