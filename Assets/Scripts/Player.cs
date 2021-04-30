using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] public float health = 10f;
    [SerializeField] Vector2 hitKnockback = new Vector2(-.002f, 0f);
    public int maxJumps;
    private int numJumps = 1;
    public List<string> items;
    

    Renderer rend;
    Color c;



    //State
    bool isAlive = true;

    // Cache component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet;

    // Messages then methods       
    void Start()
    {
        items = new List<string>();
        maxJumps = 1;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isAlive) { return; }
        if (health <= 0)
        {
            Die();
        }
        Run();

        Jump();
        FlipSprite();
        Hit();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

    }

    private void Jump()
    {

        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        { 
            numJumps = 1; 
        }
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) || (numJumps < maxJumps))
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidBody.velocity += jumpVelocityToAdd;
                numJumps += 1;
            }
        }
       
    }

    public void Hit()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Lightning")))
        {

            TakeDamage(5);
            myRigidBody.velocity = hitKnockback;
            print("hit!");
            StartCoroutine(GetInvulnerable());

        }
    }
    IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreLayerCollision(11, 9, true);
        c.a = 0.5f;
        rend.material.color = c;
        yield return new WaitForSeconds(3f);
        Physics2D.IgnoreLayerCollision(11, 9, false);
        c.a = 1f;
        rend.material.color = c;
    }


    private void Die()
    {
        myAnimator.SetTrigger("Die");
        isAlive = false;
    }


    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            string itemType = collision.gameObject.GetComponent<PowerupScript>().itemType;
            items.Add(itemType);
            if (items.Contains("DoubleJump"))
            {
                maxJumps = 2;
                print("YEP!");
            }
            Destroy(collision.gameObject);



        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Health.health -= damage;
        Debug.Log("damage Taken");
    }

}
