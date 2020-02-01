using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    [SerializeField] float thrust = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float jumpForce = 100f;
    [SerializeField] public float direction = 1f;
    [SerializeField] Tilemap interactables, foregroundTriggers;

    //Cached references
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    //BoxCollider2D myFeetCollider;
    Rigidbody2D myRigidbody;
    SpriteRenderer mySpriteRenderer;
    GameObject TileManager;
    GameObject TimeManager;

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
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        startingGravityScale = myRigidbody.gravityScale;
        TileManager = GameObject.Find("Tile Manager");
        TimeManager = GameObject.Find("Game Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    private void Run()
    {
        // TO DO: sostituire con il nuovo input system
        var movement = new Vector2(direction * thrust, myRigidbody.velocity.y);
        myRigidbody.velocity = movement;
        myAnimator.SetBool(ANIMATOR_ISRUNNING_KEY, PlayerHasHorizontalSpeed());
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
        if (myRigidbody.velocity.y < 0)
            myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, 4*jumpForce);
        else
            myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, jumpForce);
    }

    private void Jump()
    {
        if (myRigidbody.velocity.y < -0.1f)
            myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, 2.15f * jumpForce);
        else
            myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, jumpForce);
    }


    private void FlipSprite()
    {
        if (PlayerHasHorizontalSpeed())
        {
            mySpriteRenderer.flipX = myRigidbody.velocity.x < 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Exit")
        {
            if (IsInInteractableSpace(collision.ClosestPoint(gameObject.transform.position)) == "Jump")
            {
                //StartCoroutine(JumpCoroutine());
                Jump();
            }

            if (IsInInteractableSpace(collision.ClosestPoint(gameObject.transform.position)) == "Turn Around")
            {
                direction = -1 * direction;
            }
        }
     }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            TimeManager.GetComponent<TimeManager>().StopTime();
            TileManager.GetComponent<TilemapBehaviour>().activateChanges();
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

    private string IsInInteractableSpace(Vector2 closestPoint)
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Interactable")))
            return interactables.GetTile<Tile>(interactables.WorldToCell(closestPoint)).name;
        else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Terrain")))
            return foregroundTriggers.GetTile<Tile>(foregroundTriggers.WorldToCell(closestPoint)).name;
        else
            return null;
    }
}
