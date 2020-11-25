using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    public float smoothness;
    public float decalGround;
    public float decalBase;


    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (target != null)
        {

            Vector3 newPosition = target.transform.position;
            newPosition.z = -10;

            newPosition.y += decalBase;

            if (target.GetComponent<Player>().onGround)
            {
                newPosition.y += decalGround;
                transform.position = Vector3.Lerp(transform.position, newPosition, smoothness);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, newPosition, smoothness / 2);
            }
        }
    }
}
