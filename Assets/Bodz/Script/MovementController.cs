﻿
using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{
    private static float MAGIC_CONSTANT = 0.23f;
    private static float MAGIC_CONSTANTX = 0.17f;

	public float changeVelocityX = 0.8f;
	public float maxVelocityX = 1f;
    public float jumpVelocity = 5f;
    public bool isGUIOpen = false;

    [SerializeField]
    private bool isGrounded = false;
    public bool isOnLadder = false;
    public bool dead = false;
    public bool isEnter = false;

	private Vector2 lastVelocity;
    public Vector2 ladderX;

    public Vector3 spawnPos;

	// Use this for initialization
	void Start () 
	{
        spawnPos = rigidbody2D.transform.position;
		lastVelocity = new Vector2(rigidbody2D.velocity.x, -jumpVelocity);
	}
	
	// Update is called once per frame
	void Update () 
	{
        //death
        if (dead)
        {
            transform.position = spawnPos;
            rigidbody2D.velocity = new Vector2(0, -jumpVelocity);
            dead = false;
            return;
        }

		float x = lastVelocity.x;
		float y = lastVelocity.y;

		//change velocity

		if (Input.GetAxis("Horizontal") < 0 && !isGUIOpen &&
                  Physics2D.Linecast((transform.position + Vector3.left * (MAGIC_CONSTANTX + 0.02f)) + Vector3.up * MAGIC_CONSTANT,
            (transform.position + Vector3.left * (MAGIC_CONSTANTX + 0.03f)) + Vector3.down * MAGIC_CONSTANT,
            (1 << 8) + (1 << 9)).transform == null)
		{
			if (x < -maxVelocityX)
				x = -maxVelocityX;
			else
				x -= changeVelocityX;
            isOnLadder = false;
		}
        else if (Input.GetAxis("Horizontal") > 0 && !isGUIOpen &&
                  Physics2D.Linecast((transform.position + Vector3.right * (MAGIC_CONSTANTX + 0.02f)) + Vector3.up * MAGIC_CONSTANT,
            (transform.position + Vector3.right * (MAGIC_CONSTANTX + 0.03f)) + Vector3.down * MAGIC_CONSTANT,
            (1 << 8) + (1 << 9)).transform == null)
		{
			if (x > maxVelocityX)
				x = maxVelocityX;
			else
				x += changeVelocityX;
            isOnLadder = false;
		}
		else
			x = 0;

        //grounded ?
        if (Physics2D.Linecast(transform.position + Vector3.right * (MAGIC_CONSTANTX - 0.02f), transform.position - Vector3.up * (MAGIC_CONSTANT * 1.1f) + Vector3.right * (MAGIC_CONSTANTX - 0.02f), (1 << 8) + (1 << 9)).transform != null ||
            Physics2D.Linecast(transform.position + Vector3.left * (MAGIC_CONSTANTX - 0.02f), transform.position - Vector3.up * (MAGIC_CONSTANT * 1.1f) + Vector3.left * (MAGIC_CONSTANTX - 0.02f), (1 << 8) + (1 << 9)).transform != null)
            isGrounded = true;
        else
            isGrounded = false;

        //jump
        if (Input.GetButtonDown("Jump") && !isGUIOpen && isGrounded && rigidbody2D.velocity.y <= 0)
        {
            isGrounded = false;
            y = jumpVelocity;
        }
        else
            y = rigidbody2D.velocity.y;

        if (Input.GetAxis("Vertical") <= 0)
            isEnter = false;
        //Ladder
        if (Input.GetAxis("Vertical") > 0 && isOnLadder && !isGUIOpen)
        {
            this.rigidbody2D.gravityScale = 0;
            this.transform.position = new Vector3(ladderX.x, transform.position.y);
            y = 1;
        }
        else if (Input.GetAxis("Vertical") < 0 && isOnLadder && !isGUIOpen)
        {
            this.rigidbody2D.gravityScale = 0;
            this.transform.position = new Vector3(ladderX.x, transform.position.y);
            y = -1;
        }
        else if (isOnLadder && this.rigidbody2D.gravityScale == 0)
            y = 0;
        else
            this.rigidbody2D.gravityScale = 1;

		lastVelocity = new Vector2 (x, y);
		rigidbody2D.velocity = lastVelocity;
	}

   /* void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.right * (MAGIC_CONSTANT - 0.02f), transform.position - Vector3.up * (MAGIC_CONSTANT * 1.1f) + Vector3.right * (MAGIC_CONSTANT - 0.02f));
    }*/
}
