using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLoad : MonoBehaviour
{
    static string levelToLoad;

    public AsyncOperation op;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoToScene", 2.5f);
        //op.allowSceneActivation = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadLevel(string level)
    {
        levelToLoad = level;
        SceneManager.LoadScene("Load");
    }

    public void GoToScene()
    {
        op = SceneManager.LoadSceneAsync(levelToLoad);
    }
}
