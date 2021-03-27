using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Barbarian : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        // enemy will always start at idle state
        currentState = EnemyState.idle;

        // reference of rigidbody
        myRigidbody = GetComponent<Rigidbody2D>();

        // setting anim to be the animator conponent
        anim = GetComponent<Animator>();

        // enemy target is the player
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called on physics call
    void FixedUpdate()
    {
        CheckDistance();
    }

    // check the distance of the enemy and the player
    // enemy will wake up if player is within chase radius
    // goes back to sleep if player is out of chase radius
    void CheckDistance(){

        // check if player is within radius
        if (Vector3.Distance(target.position, 
                            transform.position) <= chaseRadius 
            && Vector3.Distance(target.position, 
                            transform.position) > attackRadius)
        {
            // keep making enemy walking towards player
            if (currentState == EnemyState.idle 
                    || currentState == EnemyState.walk
                    && currentState != EnemyState.stagger)
            {
                // create temporary vector2
                Vector3 temp = Vector3.MoveTowards(transform.position, 
                                                    target.position, 
                                                    moveSpeed * Time.deltaTime);

                // setting to move enemy towards player
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                // wake enemy up
                anim.SetBool("awake", true);
            }
        } 
        // else if (Vector3.Distance(target.position, 
        //                     transform.position) > chaseRadius)
        // {
        //     // enemy goes back to sleep
        //     anim.SetBool("awake", false);
        // }

        else if (Vector3.Distance(target.position, 
                                    transform.position) <= chaseRadius 
                    && Vector3.Distance(target.position, 
                                        transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk
                    && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

    // the attack coroutine
    public IEnumerator AttackCo(){
        // set current state to attack state
        currentState = EnemyState.attack;
        // set attack animation
        anim.SetBool("attack", true);
        // give damage to the player
        target.gameObject.GetComponent<PlayerController>().Hurt(5);
        // wait before next attack
        yield return new WaitForSeconds(10f);
        // set state back to walking
        currentState = EnemyState.walk;
        // stop attack animation
        anim.SetBool("attack", false);
    }

    // setting float to move directions
    private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    // change walking animation of enemy
    private void changeAnim(Vector2 direction){
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) )
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        } else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }   
        }
    }

    private void ChangeState(EnemyState newState){
        if (currentState != newState){
            currentState = newState;
        }
    }
}
