using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        fadeOut = GameObject.Find("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (SceneManager.GetActiveScene().name == "TitleScreen") {
                fadeOut.GetComponent<Animator>().SetTrigger("FadeOut");
                Invoke("LoadHub", 0.5f) ;
            } 
        }
    }

    void LoadHub() {
        SceneManager.LoadScene("Hub");
    }


}
