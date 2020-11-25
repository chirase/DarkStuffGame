using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictDamageTrigger : MonoBehaviour
{

    public int damage;
    public Vector2 force;
    public Collider2D triggerElm;

    bool triggerActivated;

    // Start is called before the first frame update
    void Start()
    {
        triggerActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        triggerElm.enabled = triggerActivated;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") {

            Vector2 modifiedForce = force;
            if (collision.transform.position.x < transform.position.x) {
                modifiedForce.x *= -1;
            } 
            
            if (collision.transform.position.y < transform.position.y) {
                modifiedForce.y *= -1;
            }

            collision.gameObject.GetComponent<Player>().ReceiveDamage(damage, modifiedForce);

        } else if (collision.tag == "breakable") {

            collision.gameObject.GetComponent<Breakable>().Break();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            Vector2 modifiedForce = force;
            if (collision.transform.position.x < transform.position.x)
            {
                modifiedForce.x *= -1;
            }

            if (collision.transform.position.y < transform.position.y)
            {
                modifiedForce.y *= -1;
            }

            collision.gameObject.GetComponent<Player>().ReceiveDamage(damage, modifiedForce);

        }
        else if (collision.tag == "breakable")
        {

            collision.gameObject.GetComponent<Breakable>().Break();

        }
    }

    public void enableTrigger() {
        triggerActivated = true;
    }

    public void disableTrigger() {
        triggerActivated = false;
    }
}
