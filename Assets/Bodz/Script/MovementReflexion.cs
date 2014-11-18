using UnityEngine;
using System.Collections;

public class MovementReflexion : MonoBehaviour {

    public GameObject RealPlayer;
    private MapController mc;

	// Use this for initialization
	void Start () 
    {
        mc = GameObject.Find("MapManager").GetComponent<MapController>();
        setPos();
	}
	
	// Update is called once per frame
	void Update () 
    {
        setPos();
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
            x = (mc.width * (mc.tileWidth - 1) / 50.0f) - RealPlayer.transform.position.x; // - XLeftBorderLevel, but always 0 so forget
            y = RealPlayer.transform.position.y - (mc.height * (mc.tileHeight - 1) / 50.0f) / 2;
        }


        transform.position = new Vector3(x, y, 0);
        transform.localScale = new Vector3(RealPlayer.transform.localScale.x * -1, RealPlayer.transform.localScale.y, RealPlayer.transform.localScale.z);
    }

}
