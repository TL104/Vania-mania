using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBotMovement : Enemy
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int randomStop = 0;


    Rigidbody2D myRigidBody;
    Animator myAnimator;

    bool walk;

    public Transform attackPosFront;
    public Transform attackPosBack;
    public LayerMask whatIsPlayer;
    public int damage;
    public float attackRange;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        walk = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);

        }

        if (walk)
        {
            Walk();
        }
        Attack();
        
    }

    private void Stop()
    {
        walk = false;
        myRigidBody.velocity = new Vector2(0, 0);
    }

    private void Attack()
    {
        randomStop = Random.Range(0, 100);
        if (randomStop > 98)
        {
            Stop();            
            StartCoroutine(DoAnimation());
        }
    }

IEnumerator DoAnimation()
    {
        myAnimator.SetBool("attack", true);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosFront.position, attackRange, whatIsPlayer);
        Collider2D[] enemiesToDamageBack = Physics2D.OverlapCircleAll(attackPosBack.position, attackRange, whatIsPlayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Player>().TakeDamage(damage);
        }
        for (int i = 0; i < enemiesToDamageBack.Length; i++)
        {
            enemiesToDamageBack[i].GetComponent<Player>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(1.167f);
        myAnimator.SetBool("attack", false);
        walk = true;
    }

    private void Walk()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosFront.position, attackRange);
        Gizmos.DrawWireSphere(attackPosBack.position, attackRange);

    }

}


