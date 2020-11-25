using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillHeightLimit : MonoBehaviour
{

    public Transform player;
    public string thisScene;

    // Start is called before the first frame update
    void Start()
    {
        thisScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y <= transform.position.y) {
            KillPlayer();
        }
    }

    void KillPlayer() {
        SceneManager.LoadScene(thisScene);
    }
}
