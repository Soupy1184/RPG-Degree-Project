// code written by Afieqha Mieza

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Barbarian : Enemy
{
    [Header("Target")]
    public Transform target;
    public LayerMask playerLayer;
    // public Transform homePosition;

    [Header("Enemy")]
    public Animator anim;
    private Rigidbody2D myRigidbody;

    [Header("Attack Point")]
    public Transform attackPoint_left;
    public Transform attackPoint_right;
    public Transform attackPoint_up;
    public Transform attackPoint_down;
    public float attackRadius; 
    public float chaseRadius;
    public int attackDamage; 

    public SpriteRenderer[] spriteRenderers;

    

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
        //check distance between player in every frame rate
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
            // Debug.Log("\ndistance: " + Vector3.Distance(target.position, transform.position));
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
        // attack if player is within attack radius
        else if (Vector3.Distance(target.position, 
                                    transform.position) <= chaseRadius 
                    && Vector3.Distance(target.position, 
                                        transform.position) <= attackRadius)
        {
            // Debug.Log(Vector3.Distance(target.position, 
            //                         transform.position));
            if (currentState == EnemyState.walk
                    && currentState != EnemyState.stagger)
            {
                // Debug.Log("In here");
                // launch attack animation
                StartCoroutine(AttackCo());
            }
        } 
        // goes back to sleep if player is out of range
        else if (Vector3.Distance(target.position, 
                                    transform.position) > chaseRadius)
        {
            // enemy goes back to sleep
            anim.SetBool("awake", false);
        }
    }

    // coroutine to launch attack animation
    public IEnumerator AttackCo(){
        //setting current state to attack
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);

        // target.gameObject.GetComponent<PlayerController>().Hurt(5);

        // wait before next attack
        yield return new WaitForSeconds(1f);

        // set back to chase mode
        currentState = EnemyState.walk;
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

    // function to perform state change of enemy
    private void ChangeState(EnemyState newState){
        if (currentState != newState){
            currentState = newState;
        }
    }

    // function to check which attack point will be used
    private Transform checkAttackPoint(Vector2 direction){
       if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) )
        {
            if (direction.x > 0)
            {
                return attackPoint_right;
            }else if (direction.x < 0)
            {
                return attackPoint_left;
            }
        } else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                return attackPoint_up;
            }else if (direction.y < 0)
            {
                return attackPoint_down;
            }   
        }

        return attackPoint_down;
    }

    // function to attack and give damage to player
    private IEnumerator AttackEnum(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        // check which Attack point to use        
        Vector3 temp = Vector3.MoveTowards(transform.position, 
                                                target.position, 
                                                moveSpeed * Time.deltaTime);

        bool alreadyDamaged = false;
        Collider2D[] hitPlayers;
        Transform attackPoint_use = checkAttackPoint(temp - transform.position);
        // Debug.Log("Attack Point: " + attackPoint_use);
        // Debug.Log("Already Damage: " + alreadyDamaged);

        //Detect target in attack radius
        hitPlayers = Physics2D.OverlapCircleAll(attackPoint_use.position, attackRadius, playerLayer);
        

        // Debug.Log("start");
        // Debug.Log("attackPoint_use: " + attackPoint_use);
        // Debug.Log("attackRadius: " + attackRadius);
        // Debug.Log("playerLayer: " + playerLayer);
        // foreach (Collider2D item in hitPlayers)
        // {
        //     Debug.Log("hit p;ayer: " + item);
        // }
        // Debug.Log("end");
        
        //deal damage to the detected target and flashes it red
        foreach (Collider2D player in hitPlayers) {
            // Debug.Log("player: " + player);
            // Debug.Log("Already Damage: " + alreadyDamaged);
            if (alreadyDamaged == false) {
                alreadyDamaged = true;
                // Debug.Log("Playeerr take damage");
                player.GetComponent<PlayerController>().Hurt(attackDamage);

                // hurt color
                foreach (SpriteRenderer renderer in spriteRenderers)
                {
                    renderer.material.color = new Color(255, 0, 0);;
                }
                StartCoroutine(FixColour());
            }
        }
    }

    // coroutine to flashes red to target
    private IEnumerator FixColour() {
        yield return new WaitForSeconds(0.1f);
        
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.material.color = new Color(255, 255, 255);
        }

        // renderer.material.color = new Color(255, 255, 255);
 
        // yield return new WaitForSeconds(hurtDuration);
     }

    // draw the circle on editor
    void OnDrawGizmosSelected() {
        Transform attackPoint_use = checkAttackPoint(transform.position);

        if (attackPoint_use == null) {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint_use.position, attackRadius);
    }

}
