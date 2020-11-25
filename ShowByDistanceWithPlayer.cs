using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShowByDistanceWithPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public Player player;
    public SpriteRenderer[] spritesToHide;
    private double distanceWithPlayer;
    public float distanceToShow;
    public bool debugRay;
    public Light2D[] lightsToHide;
    public GameObject[] gameObjectsToDisable;

    void Start()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceWithPlayer = Vector2.Distance(transform.position, player.transform.position);
        ToggleVisibility(distanceWithPlayer <= distanceToShow);
        if (debugRay) { DebugRay(); }
    }

    void ToggleVisibility(bool valid) {

        foreach (SpriteRenderer spriteRenderer in spritesToHide) {
            if (spriteRenderer != null) { 
                spriteRenderer.enabled = valid; 
            }
           
        }

        foreach (Light2D light in lightsToHide)
        {
            if (light != null)
            {
                light.enabled = valid;
            }
        }

        foreach (GameObject gb in gameObjectsToDisable)
        {
            if (gb != null)
            {
                gb.SetActive(valid);
            }
        }

    }

    void DebugRay() {
        Debug.DrawLine(transform.position,player.transform.position);
        print(distanceWithPlayer);
    }

}
