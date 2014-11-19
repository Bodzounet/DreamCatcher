using UnityEngine;
using System.Collections;

public class clickOptions : MonoBehaviour {

    public GameObject go;
    public GameObject otherGo;

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
