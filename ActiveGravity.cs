using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGravity : MonoBehaviour
{

    public Rigidbody2D[] elements;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    public void ActivateGravity()
    {
        foreach (Rigidbody2D element in elements)
        {
            if (element != null) {       
            element.simulated = true;
            }
        }

        Destroy(gameObject);
    }
}
