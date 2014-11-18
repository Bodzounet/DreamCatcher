using UnityEngine;
using System.Collections;

public class MovementReflexion : MonoBehaviour {

    public GameObject RealPlayer;
    private MapController mc;
    private MovementController mvtc;

    private Animator anim;

	// Use this for initialization
	void Start () 
    {
        anim = GameObject.Find("CharacterRight").GetComponent<Animator>();
        mc = GameObject.Find("MapManager").GetComponent<MapController>();
        mvtc = GameObject.Find("CharacterLeft").GetComponent<MovementController>();
        setPos();
	}
	
	// Update is called once per frame
	void Update () 
    {
        setPos();
        setAnim();
	}

    private void setPos()
    {
        float x;
        float y;

        //case vertical border 
        RaycastHit2D tmp = Physics2D.Raycast(RealPlayer.transform.position, Vector3.right, Mathf.Infinity, 1 << 9);
        if (tmp.transform != null)
        {
            x = RealPlayer.transform.position.x + 2 * Mathf.Abs(tmp.transform.position.x - RealPlayer.transform.position.x);
            y = RealPlayer.transform.position.y;
        }
        //case horizontal Border
        else
        {
            x = 19.20f - RealPlayer.transform.position.x; // - XLeftBorderLevel, but always 0 so forget
            y = RealPlayer.transform.position.y - 9.0f;
        }


        transform.position = new Vector3(x, y, 0);
        transform.localScale = new Vector3(RealPlayer.transform.localScale.x * -1, RealPlayer.transform.localScale.y, RealPlayer.transform.localScale.z);
    }

    private void setAnim()
    {
        anim.SetBool("isMoving", mvtc.animState.isMoving);
        anim.SetBool("isJumping", mvtc.animState.isJumping);
        anim.SetBool("isFalling", mvtc.animState.isFalling);
        anim.SetBool("jumpOver", mvtc.animState.jumpOver);
        anim.SetBool("holdMatch", mvtc.animState.wind);
        anim.SetBool("holdWindMill", mvtc.animState.fire);
    }
}
