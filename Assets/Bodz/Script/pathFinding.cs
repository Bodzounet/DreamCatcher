using UnityEngine;
using System.Collections;

public class pathFinding : MonoBehaviour {
    
    public float speed = 1f;
    private Transform trans;
    
    // Use this for initialization
    void Start () 
    {
        trans = GameObject.Find("CharacterRight").GetComponent<Transform>();
    }
	
    // Update is called once per frame
    void Update () 
    {
        //Debug.Log("moving from : x : " + transform.position.x + " // y : " + transform.position.y);
        //Debug.Log("moving to : x : " + player.transform.position.x + " // y : " + player.transform.position.y);

        Debug.Log(trans.position);

        transform.position = Vector2.MoveTowards(this.transform.position, trans.position, speed * Time.deltaTime);
    }
}