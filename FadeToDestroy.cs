using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToDestroy : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", 2f);    
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1, Mathf.Lerp(alpha, 0, 0.3f));
    }

    void Die() {
        Destroy(gameObject);
    }
}
