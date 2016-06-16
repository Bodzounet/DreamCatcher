using UnityEngine;
using System.Collections;

public class burrowManager : MonoBehaviour 
{

    private Transform heroTransform;
    private GameObject choosenOne = null;

	// Use this for initialization
	void Start () 
    {
        heroTransform = GameObject.Find("CharacterLeft").GetComponent<Transform>();

        float max = 0f;
        float tmp;

        foreach (Transform tr in this.gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (tr.gameObject.tag != "terrier")
                continue;

            muteItem(tr.gameObject);
            tmp = Mathf.Abs(Vector2.Distance(heroTransform.position, tr.position));
            if (choosenOne == null)
            {
                choosenOne = tr.gameObject;
                max = tmp;
            }
            else
            {
                if (tmp < max)
                {
                    max = tmp;
                    choosenOne = tr.gameObject;
                }
            }
        }
        unmuteItem(choosenOne);
	}
	
	// Update is called once per frame
	void Update () 
    {
        float max = 0f;
        float tmp;

        GameObject go = null;

        foreach (Transform tr in this.gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (tr.gameObject.tag != "terrier")
                continue;

            tmp = Mathf.Abs(Vector2.Distance(heroTransform.position, tr.position));
            if (go == null)
            {
                go = tr.gameObject;
                max = tmp;
            }
            else
            {
                if (tmp < max)
                {
                    max = tmp;
                    go = tr.gameObject;
                }
            }
        }

        if (go != choosenOne)
        {
            muteItem(choosenOne);
            unmuteItem(go);
            choosenOne = go;
        }
	}

    private void muteItem(GameObject go)
    {
        go.GetComponent<MonsterFactory>().enabled = false;
        go.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void unmuteItem(GameObject go)
    {
        go.GetComponent<MonsterFactory>().enabled = true;
        go.GetComponent<SpriteRenderer>().enabled = true;
    }
}
