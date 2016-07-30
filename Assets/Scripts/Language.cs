using UnityEngine;
using System.Collections;

public class Language : MonoBehaviour {

    public Translation.e_language language;

    public Translation[] txts;

    void OnMouseDown()
    {
        if (language == Translation.e_language.English)
        {
            language = Translation.e_language.French;
            foreach (Translation t in txts)
            {
                t.UpdateTxt(Translation.e_language.French);
            }
            PlayerPrefs.SetInt("fr", 1);
        }
        else
        {
            language = Translation.e_language.English;
            foreach (Translation t in txts)
            {
                t.UpdateTxt(Translation.e_language.English);
            }
            PlayerPrefs.SetInt("fr", 0);

        }
    }
}
