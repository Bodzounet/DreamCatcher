using UnityEngine;
using System.Collections;

public class KeyBoard_Controller_Switcher : MonoBehaviour
{
    public GameObject KeyBoard;
    public GameObject Controller;

    void OnMouseDown()
    {
        KeyBoard.SetActive(!KeyBoard.activeSelf);
        Controller.SetActive(!Controller.activeSelf);
    }
}
