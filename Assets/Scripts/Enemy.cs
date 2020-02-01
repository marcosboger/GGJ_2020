using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    [SerializeField] float thrust = 5f;
    Rigidbody2D myRigidbody;
    SpriteRenderer mySpriteRenderer;
    BoxCollider2D myBodyCollider;
    public float direction = 1f;
    [SerializeField] Tilemap interactables, foregroundTriggers;
    private Vector2 initialPosition;
    private float originalDirection;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        initialPosition = gameObject.transform.position;
        originalDirection = direction;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f))
        {
            Debug.Log("Did Hit");
            direction = direction * -1f;
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
    }
    public void Reset()
    {
        gameObject.transform.position = new Vector2(initialPosition.x, initialPosition.y);
        direction = originalDirection;
    }
    private void Run()
    {
        var movement = new Vector2(direction * thrust, myRigidbody.velocity.y);
        myRigidbody.velocity = movement;
    }

    private void FlipSprite()
    {
        if (EnemyHasHorizontalSpeed())
        {
            mySpriteRenderer.flipX = myRigidbody.velocity.x > 0;
        }
    }

    private bool EnemyHasHorizontalSpeed()
    {
        return Mathf.Abs(myRigidbody.velocity.x) >= Mathf.Epsilon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Exit")
            return;
        direction = direction * -1f;
    }

}
