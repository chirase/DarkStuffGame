using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateMovable : MonoBehaviour
{

    public bool makeNoises;
    public Rigidbody2D rb;
    public GameObject ps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().enabled = makeNoises;
        ps.SetActive(makeNoises);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && Mathf.Abs(rb.velocity.x) > 1f)
        {
            makeNoises = true;
        }
        else {
            makeNoises = false;
        }
    }
}
