using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopDamageText : MonoBehaviour
{

    public GameObject textItem;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopIt(Vector2 basePosition, float damage) {
      GameObject textDamage = Instantiate(textItem, basePosition + new Vector2(Random.Range(-1,1), Random.Range(1,1.3f)), transform.rotation);
      textItem.GetComponent<TextMeshPro>().text = damage.ToString();
    }
}
