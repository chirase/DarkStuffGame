using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSprite : MonoBehaviour
{
    public GameObject parent;

    public void ShowSword() {
        parent.GetComponent<Player>().ShowSword();
    }

    public void HideSword()
    {
        parent.GetComponent<Player>().HideSword();
    }
}
