using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;

    public Animator myAnimator;

    [SerializeField] int damage = 5;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                myAnimator.SetBool("Attack", true);
                StartCoroutine(RunAttackAnimation());
                
                timeBtwAttack = startTimeBtwAttack;
           

            }

            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    IEnumerator RunAttackAnimation ()
    {
        yield return new WaitForSeconds(.8f);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
        myAnimator.SetBool("Attack", false);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

    }
}
