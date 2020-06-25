using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLocal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSinglePlayer()
    {
        GameManager.instance.Invoke("LoadSingle", 0.5f);
    }

    public void LoadMultiPlayer()
    {
        GameManager.instance.Invoke("LoadMulti", 0.5f); ;
    }
}
