using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollisionLayer : MonoBehaviour
{
    public LayerMask collideLayer;

    // Start is called before the first frame update
    void Start(){
        Physics2D.SetLayerCollisionMask(gameObject.layer, collideLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
