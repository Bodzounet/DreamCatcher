using UnityEngine;
using System.Collections;


public class Translation : MonoBehaviour
{
    
    public enum e_language
    {
        French,
        English
    };

    private TextMesh txt;

    public string txt_Fr;
    public string txt_En;

    void Start()
    {
        txt_Fr = txt_Fr.Replace('\\', '\n');
        txt_En = txt_En.Replace('\\', '\n');
    }

    public void UpdateTxt(e_language language)
    {
        if (txt == null)
            txt = this.GetComponent<TextMesh>();
        txt.text = language == e_language.English ? txt_En : txt_Fr;
    }
}
