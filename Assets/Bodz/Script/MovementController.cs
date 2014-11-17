using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{
    private static float MAGIC_CONSTANT = 0.32f;


	public float changeVelocityX = 0.8f;
	public float maxVelocityX = 1f;
    public float jumpVelocity = 5f;
    private bool isGrounded = false;
    public bool isOnLadder = false;
	private Vector2 lastVelocity;

	// Use this for initialization
	void Start () 
	{
		lastVelocity = new Vector2(rigidbody2D.velocity.x, -jumpVelocity);
	}
	
	// Update is called once per frame
	void Update () 
	{
		float x = lastVelocity.x;
		float y = lastVelocity.y;

		//change velocity
		if (Input.GetAxis("Horizontal") < 0 &&
                  Physics2D.Linecast((transform.position + Vector3.left * (MAGIC_CONSTANT + 0.01f)) + Vector3.up * MAGIC_CONSTANT,
            (transform.position + Vector3.left * (MAGIC_CONSTANT + 0.01f)) + Vector3.down * MAGIC_CONSTANT,
            (1 << 8) + (1 << 9)).transform == null)
		{
			if (x < -maxVelocityX)
				x = -maxVelocityX;
			else
				x -= changeVelocityX;
		}
		else if (Input.GetAxis("Horizontal") > 0 &&
                  Physics2D.Linecast((transform.position + Vector3.right * (MAGIC_CONSTANT + 0.01f)) + Vector3.up * MAGIC_CONSTANT,
            (transform.position + Vector3.right * (MAGIC_CONSTANT + 0.01f)) + Vector3.down * MAGIC_CONSTANT,
            (1 << 8) + (1 << 9)).transform == null)
		{
			if (x > maxVelocityX)
				x = maxVelocityX;
			else
				x += changeVelocityX;
		}
		else
			x = 0;

        //grounded ?
        if (Physics2D.Linecast(transform.position, transform.position - Vector3.up * (MAGIC_CONSTANT * 1.1f), (1 << 8) + (1 << 9)).transform != null)
            isGrounded = true;

        //jump
        if (Input.GetAxis("Vertical") > 0 && isGrounded)
        {
            isGrounded = false;
            y = jumpVelocity;
        }
        else
            y = rigidbody2D.velocity.y;

		lastVelocity = new Vector2 (x, y);
		rigidbody2D.velocity = lastVelocity;
	}   

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine((transform.position + Vector3.right * (MAGIC_CONSTANT + 0.001f)) + Vector3.up * MAGIC_CONSTANT,
            (transform.position + Vector3.right * (MAGIC_CONSTANT + 0.001f)) + Vector3.down * MAGIC_CONSTANT);
    }*/
}
