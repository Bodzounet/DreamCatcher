using UnityEngine;
using System.Collections;

public class returnMenu : MonoBehaviour {

    public GameObject go;

    void OnMouseDown()
    {
        go.SetActive(false);
    }
}
