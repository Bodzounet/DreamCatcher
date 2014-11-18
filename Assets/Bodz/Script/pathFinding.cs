using UnityEngine;
using System.Collections;

public class pathFinding : MonoBehaviour {
    
    public float speed = 1f;
    private Transform trans;

    private float lastX;
    private MovementController.e_dir lastDir = MovementController.e_dir.RIGHT;
    private float scaleX;
    
    // Use this for initialization
    void Start () 
    {
        trans = GameObject.Find("CharacterRight").GetComponent<Transform>();
        lastX = transform.position.x;
        scaleX = transform.localScale.x;
    }
	
    // Update is called once per frame
    void Update () 
    {
        MovementController.e_dir dir = MovementController.e_dir.NONE;

        transform.position = Vector2.MoveTowards(this.transform.position, trans.position, speed * Time.deltaTime);
        if (trans.position.x < transform.position.x)
            dir = MovementController.e_dir.LEFT;
        else if (trans.position.x > transform.position.x)
            dir = MovementController.e_dir.RIGHT;
        lastX = transform.position.x;

        if (dir == MovementController.e_dir.LEFT)
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
}