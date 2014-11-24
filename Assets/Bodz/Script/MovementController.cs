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
        public bool attack;
        public bool dream;
        public bool isClimbing;
    }

    public enum e_dir
    {
        LEFT,
        RIGHT,
        NONE
    };

    private static float MAGIC_CONSTANT = 0.40f;
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
    bool deathFall;
    double maximumFallVelocity;

    private Animator anim;
    public AnimState animState;
    public GameObject eye;
    private CharacterInventory inventory;

    public AudioClip[] JumpSounds;
    public AudioClip walk;
    public AudioClip death;

    public bool stop = true;

    public bool deathAnim = true;

     IEnumerator lolilol()
    {
        yield return new WaitForSeconds(2.4f);
        stop = false;
    }

	// Use this for initialization
	void Start () 
	{
        anim = GameObject.Find("CharacterLeft").GetComponent<Animator>();
        if (Application.loadedLevel == 1)
        {

            anim.Play("awake");
            StartCoroutine(lolilol());
        }
        else
            stop = false;

        spawnPos = rigidbody2D.transform.position;
		lastVelocity = new Vector2(rigidbody2D.velocity.x, -jumpVelocity);

        inventory = GameObject.Find("CharacterLeft").GetComponent<CharacterInventory>();
        deathFall = false;
        maximumFallVelocity = 5;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (stop)
            return;

        //death
        if (dead)
        {
            onDeath();
            return;
        }

		float x = lastVelocity.x;
		float y = lastVelocity.y;
        if (Mathf.Abs(y) > maximumFallVelocity)
            deathFall = true;

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

            if (!isGrounded && x < -maxVelocityX / 2.5f)
                x = -maxVelocityX / 2.5f;
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


            if (!isGrounded && x > maxVelocityX / 2.5f)
                x = maxVelocityX / 2.5f;
			if (x > maxVelocityX)
				x = maxVelocityX;
			else
				x += changeVelocityX;
            //isOnLadder = false;
		}
		else
			x = 0;

        //grounded ?
        if (Physics2D.Linecast(transform.position + Vector3.right * (MAGIC_CONSTANTX - 0.02f), transform.position - Vector3.up * (MAGIC_CONSTANT * 1.1f) + Vector3.right * (MAGIC_CONSTANTX - 0.02f), (1 << 8) + (1 << 9)).transform != null ||
            Physics2D.Linecast(transform.position + Vector3.left * (MAGIC_CONSTANTX - 0.02f), transform.position - Vector3.up * (MAGIC_CONSTANT * 1.1f) + Vector3.left * (MAGIC_CONSTANTX - 0.02f), (1 << 8) + (1 << 9)).transform != null)
        {
            if (deathFall == true)
                dead = true;
            isGrounded = true;
        }
        else
            isGrounded = false;

        //jump
        if (Input.GetButtonDown("Jump") && !isGUIOpen && isGrounded && rigidbody2D.velocity.y <= 0)
        {
            if (JumpSounds.Length > 0)
            {
                this.audio.clip = JumpSounds[Random.Range(0, JumpSounds.Length )];
                this.audio.Play();
            }
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
        if (isOnLadder == true)
            deathFall = false;
        if (Input.GetAxis("Vertical") > 0 && isOnLadder && !isGUIOpen)
        {
           
            this.transform.position = new Vector3(ladderX.x, transform.position.y);
            y = 1;
        }
        else if (Input.GetAxis("Vertical") < 0 && isOnLadder && !isGUIOpen)
        {
            this.transform.position = new Vector3(ladderX.x, transform.position.y);
            y = -1;
        }
        else if (isOnLadder && this.rigidbody2D.gravityScale == 0)
            y = 0;
        else if (isOnLadder)
            this.rigidbody2D.gravityScale = 0;
        else
            this.rigidbody2D.gravityScale = 1;

        if (x != 0 && walk != null && !this.audio.isPlaying && isGrounded)
        {
            this.audio.clip = walk;
            this.audio.Play();
        }
		lastVelocity = new Vector2 (x, y);
		rigidbody2D.velocity = lastVelocity;

        animCharacter();
        setAnim();
	}

    //some private fcts to make the update() readable...
    public void onDeath()
    {
        if (deathAnim)
        {
            deathAnim = false;
            anim.Play("Death");
        }
        
        if (!eye.GetComponent<Animator>().GetBool("dead"))
            eye.GetComponent<Animator>().SetBool("dead", true);
        if (eye.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (death != null && this.audio.clip != death)
        {
            this.audio.clip = death;
            this.audio.Play();
        }
        //transform.position = spawnPos;
        rigidbody2D.velocity = new Vector2(0, -jumpVelocity);
        currentDir = e_dir.LEFT;
        deathFall = false;
        //dead = false;


    }

    private void animCharacter()
    {
        animState.jumpOver = false;
        if (Mathf.Abs(rigidbody2D.velocity.x) > maxVelocityX / 10)
            animState.isMoving = true;
        else
            animState.isMoving = false;

        if (isOnLadder)
        {
            animState.isFalling = false;
            animState.isJumping = false;
            animState.isMoving = false;
            animState.jumpOver = false;
            animState.isClimbing = true;
        }
        else
        {
            animState.isClimbing = false;
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

        animState.attack = false;
        if (inventory.attack)
            animState.attack = true;

        animState.dream = false;
        if (inventory.dream)
            animState.dream = true;
    }

    private void setAnim()
    {
        anim.SetBool("isMoving", animState.isMoving);
        anim.SetBool("isJumping", animState.isJumping);
        anim.SetBool("isFalling", animState.isFalling);
        anim.SetBool("jumpOver", animState.jumpOver);
        anim.SetBool("holdMatch", animState.fire);
        anim.SetBool("holdWindMill", animState.wind);
        anim.SetBool("isAttacking", animState.attack);
        anim.SetBool("dream", animState.dream);
        anim.SetBool("isClimbing", animState.isClimbing);
    }

    /* void OnDrawGizmos()
     {
         Gizmos.color = Color.yellow;
         Gizmos.DrawLine(transform.position + Vector3.right * (MAGIC_CONSTANTX - 0.02f), transform.position - Vector3.up * (MAGIC_CONSTANT * 1.1f) + Vector3.right * (MAGIC_CONSTANTX - 0.02f));
     }*/
}
