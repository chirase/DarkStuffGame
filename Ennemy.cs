using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public LayerMask collideLayer;
    public float health;
    public float damage;
    public float speed;
    public float range;
    public float jumpForce;


    public LayerMask groundLayer;
    public bool onGround;

    public GameObject target;
    public float distanceWithTarget;
    public float meleeRange;

    public Animator an;
    public Rigidbody2D rb;

    public LayerMask viewMask;

    public bool hurt;

    public float startHealth;
    public GameObject lifeBar;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

        lifeBar.transform.localScale = new Vector3((health / startHealth), lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.56f, groundLayer);
        onGround = hit.collider != null;

        Physics2D.SetLayerCollisionMask(gameObject.layer, collideLayer);

        if (target == null)
        {
            FindTarget();
        }
        else {

            if (transform.position.x > target.transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }

            distanceWithTarget = Vector2.Distance(transform.position, target.transform.position);

            if (distanceWithTarget <= range)
            {
                if (distanceWithTarget <= meleeRange)
                {
                    Attack();
                }
                else
                {
                    if (!hurt) { 
                    MoveTowardPlayer();
                    }
                }
            }
            else {
                FindTarget();
            }
        }
    }


    void Attack() {

        rb.velocity = new Vector2((speed / 5) * transform.localScale.x, rb.velocity.y);

        an.SetBool("walk", false);
        an.SetBool("attack", true);
    }

    void MoveTowardPlayer() {

        an.SetBool("attack", false);
        an.SetBool("walk", true);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0.33f * transform.localScale.x, 0, 0), target.transform.position - transform.position, 0.1f, viewMask);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Ground" || hit.collider.tag == "Crate")
            {
                Jump();

            }
        }
        else {
            hit = Physics2D.Raycast(transform.position + new Vector3(transform.localScale.x / 2.5f, -1, 0), transform.position - transform.position - new Vector3(transform.localScale.x / 2.5f, -1, 0), 0.5f);
            Debug.DrawRay(transform.position + new Vector3(transform.localScale.x / 2.5f, -1, 0), transform.position - transform.position - new Vector3(transform.localScale.x / 2.5f, -1, 0));
            if (hit.collider != null || !onGround){
                rb.velocity = new Vector2(speed * transform.localScale.x, rb.velocity.y);
            }
            else {
                TinyJump();
            }
        }
    }

    void Jump()
    {
        if (onGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }


    void TinyJump()
    {
        if (onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x * 1.2f, jumpForce / 1.2f);
        }
    }

    public void Knocked() {
        if (onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce / 4);
        }
        else {
            rb.velocity = new Vector2(-transform.localScale.x / 5, rb.velocity.y);
        }
    }




    void FindTarget() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float minDistance = range + 1;
        GameObject nearestPlayer = null;

        foreach (GameObject player in players) {
            float distanceWithPlayer = Vector2.Distance(transform.position, player.transform.position);
            if ( distanceWithPlayer < minDistance) {
                minDistance = distanceWithPlayer;
                nearestPlayer = player;
            }
        }

        target = nearestPlayer;
    }

    public void ReceiveDamage(float damage, Vector2 force)
    {
        
        Knocked();
        hurt = true;
        Invoke("Unhurt", 0.3f);

        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        health -= damage;

        GetComponent<PopDamageText>().PopIt(transform.position,damage);

        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

  
    public void Unhurt() {
        hurt = false;
    }

    
}
