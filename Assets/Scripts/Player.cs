using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float thrust = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    //Cached references
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Rigidbody2D myRigidbody;
    SpriteRenderer mySpriteRenderer;

    float startingGravityScale;

    const string ANIMATOR_ISRUNNING_KEY = "isRunning";
    const string ANIMATOR_ISCLIMBING_KEY = "isClimbing";

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        startingGravityScale = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Climb();
        Jump();
    }

    private void Run()
    {
        // TO DO: sostituire con il nuovo input system
        var directionX = Input.GetAxis("Horizontal");
        var movement = new Vector2(directionX * thrust, myRigidbody.velocity.y);
        //myRigidbody.AddForce(newPos); // molto più lento che non modificare direttamente la velocity
        myRigidbody.velocity = movement;
        myAnimator.SetBool(ANIMATOR_ISRUNNING_KEY, PlayerHasHorizontalSpeed());
    }

    private void Climb()
    {
        if (!IsInLadderSpace())
        {
            myAnimator.SetBool(ANIMATOR_ISCLIMBING_KEY, false);
            myRigidbody.gravityScale = startingGravityScale;
            return;
        }

        var deltaY = Input.GetAxis("Vertical") * climbSpeed;
        myRigidbody.gravityScale = 0f;
        var verticalMovement = new Vector2(myRigidbody.velocity.x, deltaY);
        //myRigidbody.AddForce(newPos); // molto più lento che non modificare direttamente la velocity
        myRigidbody.velocity = verticalMovement;
        myAnimator.SetBool(ANIMATOR_ISCLIMBING_KEY, PlayerHasVerticalSpeed());
    }


    private bool PlayerHasHorizontalSpeed()
    {
        return Mathf.Abs(myRigidbody.velocity.x) >= Mathf.Epsilon;
    }

    private bool PlayerHasVerticalSpeed()
    {
        return Mathf.Abs(myRigidbody.velocity.y) >= Mathf.Epsilon;
    }


    private void Jump()
    {
        //Debug.Log(IsInLadderSpace());
        if (!(IsOnGround() || IsInLadderSpace())) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump!");
            myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, jumpForce);
        }
    }

    private void FlipSprite()
    {
        if (PlayerHasHorizontalSpeed())
        {
            mySpriteRenderer.flipX = myRigidbody.velocity.x < 0;
        }
    }

    private bool IsOnGround()
    {
        return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Terrain"));
    }

    private bool IsInLadderSpace()
    {
        return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders"));
    }

}
