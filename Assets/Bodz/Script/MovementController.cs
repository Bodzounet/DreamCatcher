using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{
    public struct AnimState
    {
        public bool isMoving;
        public bool fire;
        public bool wind;
        public bool isJumping;
        public bool jumpOver;
        public bool isFalling;
    }

    public enum e_dir
    {
        LEFT,
        RIGHT,
        NONE
    };

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

    public e_dir currentDir = e_dir.RIGHT;

    private Animator anim;
    public AnimState animState;

    private CharacterInventory inventory;

	// Use this for initialization
	void Start () 
	{
        spawnPos = rigidbody2D.transform.position;
		lastVelocity = new Vector2(rigidbody2D.velocity.x, -jumpVelocity);
        anim = GameObject.Find("CharacterLeft").GetComponent<Animator>();
        inventory = GameObject.Find("CharacterLeft").GetComponent<CharacterInventory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        //death
        if (dead)
        {
            onDeath();
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
            //setting good direction
            if (currentDir != e_dir.LEFT)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            currentDir = e_dir.LEFT;

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
            //setting good direction
            if (currentDir != e_dir.RIGHT)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            currentDir = e_dir.RIGHT;


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
        else
            isEnter = true;
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

        animCharacter();
        setAnim();
	}

    //some private fcts to make the update() readable...
    private void onDeath()
    {
        transform.position = spawnPos;
        rigidbody2D.velocity = new Vector2(0, -jumpVelocity);
        currentDir = e_dir.LEFT;
        dead = false;
    }

    private void animCharacter()
    {
        Debug.Log(rigidbody2D.velocity);

        animState.jumpOver = false;
        if (Mathf.Abs(rigidbody2D.velocity.x) > maxVelocityX / 10)
            animState.isMoving = true;
        else
            animState.isMoving = false;

        if (rigidbody2D.velocity.y > 1f)
            animState.isJumping = true;

        if (rigidbody2D.velocity.y < -1f)
        {
            animState.isJumping = false;
            animState.isFalling = true;
        }

        if (isGrounded)
        {
            animState.isFalling = false;
            animState.jumpOver = true;
        }

        if (inventory.item == Item.ItemType.FLAME)
        {
            animState.fire = true;
            animState.wind = false;
        }
        else if (inventory.item == Item.ItemType.WATER)
        {
            animState.fire = false;
            animState.wind = true;
        }
        else
        {
            animState.fire = false;
            animState.wind = false;
        }
    }

    private void setAnim()
    {
        anim.SetBool("isMoving", animState.isMoving);
        anim.SetBool("isJumping", animState.isJumping);
        anim.SetBool("isFalling", animState.isFalling);
        anim.SetBool("jumpOver", animState.jumpOver);
        anim.SetBool("holdMatch", animState.fire);
        anim.SetBool("holdWindMill", animState.wind);
    }

   /* void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.right * (MAGIC_CONSTANT - 0.02f), transform.position - Vector3.up * (MAGIC_CONSTANT * 1.1f) + Vector3.right * (MAGIC_CONSTANT - 0.02f));
    }*/
}
