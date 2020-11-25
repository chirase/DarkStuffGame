using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpriteActions : MonoBehaviour
{
    public GameObject attackZone;

    void Start()
    {
        attackZone = transform.parent.Find("AttackZone").gameObject; 
    }

    public void enableTrigger() {
        attackZone.GetComponent<InflictDamageTrigger>().enableTrigger();
    }

    public void disableTrigger() {
        attackZone.GetComponent<InflictDamageTrigger>().disableTrigger();
    }
}
