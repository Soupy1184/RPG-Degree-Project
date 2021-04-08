using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Red : Enemy
{
    [Header("Enemy")]
    public Animator anim;
    private Rigidbody2D myRigidbody;

    [Header("Target")]
    public Transform target;
    
    [Header("Attack Point")]
    public float chaseRadius;
    public float attackRadius;
    

    // Start is called before the first frame update
    void Start()
    {
        // enemy will always start at idle state
        currentState = EnemyState.idle;

        //reference of rigidbody
        myRigidbody = GetComponent<Rigidbody2D>();

        //setting anim to be the animator conponent
        anim = GetComponent<Animator>();
        
        // enemy target is the player
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //check distance between player in every frame rate
        CheckDistance();
    }

    // check the distance of the enemy and the player
    // enemy will wake up if player is within chase radius
    // goes back to sleep if player is out of chase radius
    void CheckDistance(){
        // check if player is within chase radius
        // but still outside attack radius
        // make enemy walk towards player
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
                // create temporrary vector2
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                // moving the enemy towards player
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);

                // wake enemy up
                anim.SetBool("awake", true);
            }
        } 
        // goes back to sleep if player is out of radius range
        else if (Vector3.Distance(target.position, 
                            transform.position) > chaseRadius){
            // enemy goes back to sleep
            anim.SetBool("awake", false);
        }
    }

    // setting float to move directions
    private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    // change walking animation of enemy
    // check target direction
    // call setAnimFloat to move the vector position
    private void changeAnim(Vector2 direction)
    {
        // x-axis direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) )
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        } 
        // y-axis directiom
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
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

    // function to perform state change of enemy
    private void ChangeState(EnemyState newState){
        if (currentState != newState){
            currentState = newState;
        }
    }
}
