<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;

public class play : MonoBehaviour 
{

    public Font niceFont;
    public Font horrorFont;
    public TextMesh txt;

    private float t = 0.1f;
    private bool lerpDark = false;
    private bool lerpLight = false;

    public Color c1;
    public Color c2;

    public GameObject Eye;
    public AudioClip darkness;

    void Update()
    {
        if (lerpDark)
        {
            txt.color = Color.Lerp(c1, c2, t);
            t += Time.deltaTime;
            if (t > 1)
            {
                t = 0;
                lerpDark = false;
            }
        }

        if (lerpLight)
        {
            txt.color = Color.Lerp(c2, c1, t);
            t += Time.deltaTime;
            if (t > 1)
            {
                t = 0;
                lerpLight = false;
            }
        }
    }

    void OnMouseEnter()
    {
        lerpDark = true;
        lerpLight = false;
        t = 0;
    }

    void OnMouseExit()
    {
        lerpLight = true;
        lerpDark = false;
        t = 0;
    }
}
=======
﻿using UnityEngine;
using System.Collections;

public class play : MonoBehaviour 
{

    public Font niceFont;
    public Font horrorFont;
    public TextMesh txt;

    private float t = 0.1f;
    private bool lerpDark = false;
    private bool lerpLight = false;

    public Color c1;
    public Color c2;

    public GameObject Eye;
    public AudioClip darkness;

    void Update()
    {
        if (lerpDark)
        {
            txt.color = Color.Lerp(c1, c2, t);
            t += Time.deltaTime;
            if (t > 1)
            {
                t = 0;
                lerpDark = false;
            }
        }

        if (lerpLight)
        {
            txt.color = Color.Lerp(c2, c1, t);
            t += Time.deltaTime;
            if (t > 1)
            {
                t = 0;
                lerpLight = false;
            }
        }
    }

    void OnMouseEnter()
    {
        lerpDark = true;
        lerpLight = false;
        t = 0;
    }

    void OnMouseExit()
    {
        lerpLight = true;
        lerpDark = false;
        t = 0;
    }
}
>>>>>>> 4c7c6adade5e85372af206f0fd9cd710083d96eb
