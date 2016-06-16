using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class controlXBox : MonoBehaviour {

    private GameObject current;
    private GameObject next;
    private int childId = 0;

    public Color c1;
    public Color c2;

    private bool checkAgain = true;

    public GameObject opt;
    public GameObject credits;

    public GameObject Eye;
    public AudioClip darkness;

    public GameObject[] subMenu;
    bool subMenuActive = false;

    private bool onOff = true;
    public Sprite[] spirtes;


    private bool fr = true;

	// Use this for initialization
	void Start () 
    {
        current = transform.GetChild(childId).gameObject;
        current.GetComponent<TextMesh>().color = c2;

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!checkAgain)
        {
            if (Input.GetAxis("Vertical") == 0)
                checkAgain = true;
            return;
        }

	    if (Input.GetAxis("Vertical") < -0.1f)
        {
            checkAgain = false;

            next = getNextChild();

            StopAllCoroutines();
            StartCoroutine("lostFocus");
            StartCoroutine("gainFocus");

            current = next;
        }
        else if (Input.GetAxis("Vertical") > 0.1f)
        {
            checkAgain = false;
            next = getPreviousChild();

            StopAllCoroutines();
            StartCoroutine("lostFocus");
            StartCoroutine("gainFocus");

            current = next;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            if (current.name == "Play")
                doPlayButton();
            else if (current.name == "Options")
                doOptionButton();
            else if (current.name == "Credits")
                doCredit();
            else if (current.name == "Quit")
                Application.Quit();
            else if (current.name == "micro")
                doMicro();
            else if (current.name == "language")
                doLanguage();
            else if (current.name == "exit")
                doExit();
        }
	}

    private GameObject getNextChild()
    {
        if (subMenuActive)
        {
            if (++childId >= subMenu.Length)
                childId = 0;
            return subMenu[childId];
        }
        else
        {
            if (++childId >= transform.childCount)
                childId = 0;
            return transform.GetChild(childId).gameObject;
        }
    }

    private GameObject getPreviousChild()
    {
        if (subMenuActive)
        {
            if (--childId < 0)
                childId = subMenu.Length - 1;
            return subMenu[childId];
        }
        else
        {
            if (--childId < 0)
                childId = transform.childCount - 1;
            return transform.GetChild(childId).gameObject;
        }
    }

    private IEnumerator lostFocus()
    {
        current.GetComponent<TextMesh>().color = Color.Lerp(c1, c2, Time.deltaTime);
        yield return null;
    }

    private IEnumerator gainFocus()
    {
        next.GetComponent<TextMesh>().color = Color.Lerp(c2, c1, Time.deltaTime);
        yield return null;
    }

    private IEnumerable pseudoUpdate()
    {
        if (Eye.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            SceneManager.LoadScene(1);
        }
        yield return null;
    }

    private void doPlayButton()
    {
        Eye.SetActive(true);
        if (darkness != null)
        {
            Camera.main.GetComponent<AudioSource>().clip = darkness;
            Camera.main.GetComponent<AudioSource>().Play();
        }

        Eye.GetComponent<Animator>().SetBool("dead", true);
        StartCoroutine("pseudoUpdate");
    }

    private void doOptionButton()
    {
        if (opt.activeSelf == false)
        {
            subMenuActive = true;
            current = subMenu[0];
            current.GetComponent<TextMesh>().color = c2;
            childId = 0;
            credits.SetActive(false);
            opt.SetActive(true);
        }
        else
        {
            opt.SetActive(false);
            subMenuActive = false;
            current = transform.GetChild(1).gameObject;
            childId = 1;
        }
    }

    private void doCredit()
    {
        if (credits.activeSelf == false)
        {
            opt.SetActive(false);
            subMenuActive = false;
            credits.SetActive(true);
        }
        else
            credits.SetActive(false);
    }

    private void doMicro()
    {
        if (onOff)
        {
            opt.GetComponent<SpriteRenderer>().sprite = spirtes[0];
            onOff = false;
            current.gameObject.GetComponent<TextMesh>().text = "Micro       : Non fonctionnel";
            PlayerPrefs.SetInt("micro", 1);
        }
        else
        {
            opt.GetComponent<SpriteRenderer>().sprite = spirtes[1];
            onOff = true;
            current.gameObject.GetComponent<TextMesh>().text = "Micro       : Off";
            PlayerPrefs.SetInt("micro", 0);
        }
    }

    private void doLanguage()
    {
        if (fr)
        {
            fr = false;
            current.GetComponent<TextMesh>().text = "Language  : English";
            PlayerPrefs.SetInt("fr", 0);

            transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "Play";
            transform.GetChild(3).gameObject.GetComponent<TextMesh>().text = "Quit";
        }
        else
        {
            fr = true;
            current.GetComponent<TextMesh>().text = "Langue     : Français";
            PlayerPrefs.SetInt("fr", 1);

            transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "Jouer";
            transform.GetChild(3).gameObject.GetComponent<TextMesh>().text = "Quitter";
        }
    }

    private void doExit()
    {
        current.GetComponent<TextMesh>().color = c1;
        subMenuActive = false;
        current = transform.GetChild(1).gameObject;
        childId = 1;
        opt.SetActive(false);
    }
}
