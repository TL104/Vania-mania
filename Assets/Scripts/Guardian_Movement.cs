using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian_Movement : Enemy
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int randomStop = 0;
    [SerializeField] float stoppingDistance = 7;
    

    private Vector3 MyStartPosition;

    private Transform target;

    Rigidbody2D myRigidBody;
    Animator myAnimator;

    bool walk;

    // Start is called before the first frame update
    void Start()
    {
        MyStartPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
            Move();
        }
        else
        {
            MoveHome();
        }
        
    }

    void Move()
    {
        if ((Vector2.Distance(transform.position, target.position) > 0) && (Vector2.Distance(transform.position, target.position) <= 20))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            Reset();
        }
    }

    void MoveHome()
    {
        if (transform.position != MyStartPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, MyStartPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            walk = true;
        }

    }

    public void Reset()
    {
        walk = false;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Reset();
    }
}
