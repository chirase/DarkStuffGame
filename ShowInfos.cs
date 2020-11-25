using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfos : MonoBehaviour
{
    public bool owned;
    public LayerMask onlyHeroInteractions;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.SetLayerCollisionMask(15, onlyHeroInteractions);

        if (owned == true)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (owned == true)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && owned == false) { 
        GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "Player" && owned == false)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
