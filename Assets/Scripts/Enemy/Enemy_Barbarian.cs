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

    [Header("Attack Point")]
    public Transform attackPoint_left;
    public Transform attackPoint_right;
    public Transform attackPoint_up;
    public Transform attackPoint_down;
    public float attackRange; // same as attack Radius
    public LayerMask playerLayer;
    public int attackDamage; 

    

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
        } else if (Vector3.Distance(target.position, 
                                    transform.position) <= chaseRadius 
                    && Vector3.Distance(target.position, 
                                        transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk
                    && currentState != EnemyState.stagger)
            {
                // launch attack animation
                StartCoroutine(AttackCo());
            }
        } else if (Vector3.Distance(target.position, 
                                    transform.position) > chaseRadius)
        {
            // enemy goes back to sleep
            anim.SetBool("awake", false);
        }
    }

    // coroutine to launch attack animation
    public IEnumerator AttackCo(){
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);
        // target.gameObject.GetComponent<PlayerController>().Hurt(5);
        yield return new WaitForSeconds(3f);
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

    private void ChangeState(EnemyState newState){
        if (currentState != newState){
            currentState = newState;
        }
    }

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

    private IEnumerator AttackEnum(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        // check which Attack point to use        
        Vector3 temp = Vector3.MoveTowards(transform.position, 
                                                target.position, 
                                                moveSpeed * Time.deltaTime);

        bool alreadyDamaged = false;
        Collider2D[] hitPlayers;
        Transform attackPoint_use = checkAttackPoint(temp - transform.position);
        Debug.Log("AttackPoint use " + attackPoint_use);

        //Detect enemies in range of attack
        hitPlayers = Physics2D.OverlapCircleAll(attackPoint_use.position, attackRange, playerLayer);

        //deal damage to the detected enemies and flashes it red
        foreach (Collider2D player in hitPlayers) {
            if (alreadyDamaged == false) {
                alreadyDamaged = true;

                Debug.Log("Executioner hit " + player.name);
                player.GetComponent<PlayerController>().Hurt(attackDamage);
                player.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                StartCoroutine(FixColour(player));
            }
        }
    }

    private IEnumerator FixColour(Collider2D enemy) {
          yield return new WaitForSeconds(0.1f);
          Debug.Log("Fixing " + enemy.name + " colour");
          enemy.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
     }

    // draw the circle on editor
     void OnDrawGizmosSelected() {
        Transform attackPoint_use = checkAttackPoint(transform.position);

        if (attackPoint_use == null) {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint_use.position, attackRange);
     }

}
