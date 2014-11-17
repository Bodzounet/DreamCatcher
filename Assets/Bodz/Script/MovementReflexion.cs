using UnityEngine;
using System.Collections;

public class MovementReflexion : MonoBehaviour {

    public GameObject RealPlayer;
    public GameObject Border;

	// Use this for initialization
	void Start () 
    {
        transform.position = new Vector3(Border.transform.position.x - (RealPlayer.transform.position.x - Border.transform.position.x),
            RealPlayer.transform.position.y,
            RealPlayer.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () 
    {
        rigidbody2D.velocity = new Vector2(-RealPlayer.rigidbody2D.velocity.x, RealPlayer.rigidbody2D.velocity.y);
	}
}
