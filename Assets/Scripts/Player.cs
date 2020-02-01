using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    [SerializeField] float thrust = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float jumpForce = 100f;
    [SerializeField] float direction = 1f;
    [SerializeField] Tilemap interactables, foregroundTriggers;

    //Cached references
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    //BoxCollider2D myFeetCollider;
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
        //myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        startingGravityScale = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Climb();
    }

    private void Run()
    {
        // TO DO: sostituire con il nuovo input system
        var movement = new Vector2(direction * thrust, myRigidbody.velocity.y);
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


    private IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(.1f);
        myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, jumpForce);
    }

    //private void Jump()
    //{
    //    //Debug.Log(IsInLadderSpace());
    //    if (!(IsOnGround() || IsInLadderSpace())) { return; }

    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        Debug.Log("Jump!");
    //        myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, jumpForce);
    //    }
    //}

    private void FlipSprite()
    {
        if (PlayerHasHorizontalSpeed())
        {
            mySpriteRenderer.flipX = myRigidbody.velocity.x < 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsInInteractableSpace() == "SPA-Ladder")
        {
            Debug.Log("Should trigger the jump");
            StartCoroutine(JumpCoroutine());
        }

        if (IsInInteractableSpace() == "SPA_Rock_Grass_Water_29")
        {
            Debug.Log("Should make player go back");
            direction = -1 * direction;
        }

    }

    private bool IsOnGround()
    {
        //return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Terrain"));
        return false;
    }

    private bool IsInLadderSpace()
    {
        //return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders"));
        return false;
    }

    private bool IsInJumpSpace()
    {
        return myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Jumpable"));
    }

    private string IsInInteractableSpace()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Interactable")))
        {
            Debug.Log(transform.position);
            Debug.Log(interactables.WorldToCell(transform.position));
            return interactables.GetTile<Tile>(interactables.WorldToCell(transform.position)).name;
        }
        else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Terrain")))
        {
            Debug.Log(transform.position);
            Debug.Log(interactables.WorldToCell(transform.position));
            return foregroundTriggers.GetTile<Tile>(foregroundTriggers.WorldToCell(transform.position)).name;
        }
        else
            return null;
    }
}
