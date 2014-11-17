using UnityEngine;
using System.Collections;

public class MovementReflexion : MonoBehaviour {

    public GameObject RealPlayer;

	// Use this for initialization
	void Start () 
    {
        setPos();
	}
	
	// Update is called once per frame
	void Update () 
    {
        setPos();
	}

    private void setPos()
    {
        float x = RealPlayer.transform.position.x;
        float y = RealPlayer.transform.position.y;

        //calc x pos of the mirrored player
        RaycastHit2D tmp = Physics2D.Raycast(RealPlayer.transform.position, Vector3.right, Mathf.Infinity, 1 << 9);
        if (tmp.transform != null)
            x = RealPlayer.transform.position.x + 2 * Mathf.Abs(tmp.transform.position.x - x);

        //calc y pos of the mirrored player
        tmp = Physics2D.Raycast(RealPlayer.transform.position, Vector3.down, Mathf.Infinity, 1 << 9);
        if (tmp.transform != null)
            y = RealPlayer.transform.position.y - 2 * Mathf.Abs(tmp.transform.position.y - y);

        transform.position = new Vector3(x, y, RealPlayer.transform.position.z);
    }

}
