using UnityEngine;
using System.Collections;

public class MonsterFactory : MonoBehaviour {

    public GameObject model;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        GameObject monster;

        if (Input.GetKeyDown(KeyCode.Return))
            monster = Instantiate(model, transform.position, model.transform.rotation) as GameObject;
	}
}
