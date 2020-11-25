using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HideAndShowCollection : MonoBehaviour
{

    public SpriteRenderer[] spritesToHide;
    public Light2D[] lightsToHide;
    public GameObject[] gameObjectsToDisable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAll() {
        foreach (SpriteRenderer spriteRenderer in spritesToHide)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }

        }

        foreach (Light2D light in lightsToHide)
        {
            if (light != null)
            {
                light.enabled = true;
            }
        }

        foreach (GameObject gb in gameObjectsToDisable)
        {
            if (gb != null)
            {
                gb.SetActive(true);
            }
        }
    }

    public void HideAll() {
        foreach (SpriteRenderer spriteRenderer in spritesToHide)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }

        }

        foreach (Light2D light in lightsToHide)
        {
            if (light != null)
            {
                light.enabled = false;
            }
        }

        foreach (GameObject gb in gameObjectsToDisable)
        {
            if (gb != null)
            {
                gb.SetActive(false);
            }
        }
    }
}
