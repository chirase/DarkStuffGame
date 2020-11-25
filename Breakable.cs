using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breakable : MonoBehaviour
{

    public GameObject[] pieces;
    public GameObject[] loot;
    public float lowestYVel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.y < lowestYVel)
        {
            lowestYVel = GetComponent<Rigidbody2D>().velocity.y;
        }
    }

    public void Break() {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        foreach (GameObject piece in pieces)
        {
            piece.GetComponent<FadeToDestroy>().enabled = true;
            piece.transform.SetParent(null);
            piece.GetComponent<SpriteRenderer>().enabled = true;
            piece.GetComponent<BoxCollider2D>().enabled = true;
            piece.transform.GetComponent<Rigidbody2D>().simulated = true;
            piece.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            piece.transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(0, 5), Random.Range(0, 5), 0), ForceMode2D.Impulse);
            piece.transform.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-10, 10));
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.transform.tag == "Ground" || coll.transform.tag == "Crate") { 
            if (lowestYVel < -8)
            {
                Break();
            }
            else {
                lowestYVel = 0;
            }
        }
    }

    
}
