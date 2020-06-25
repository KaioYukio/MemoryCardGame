using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpawn : MonoBehaviour
{
    public GameObject confirmSound;
    public GameObject denySound;
    public GameObject normalSound;

    // Start is called before the first frame update
    void Start()
    {


    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnConfirm()
    {
        Instantiate(confirmSound, transform.position, Quaternion.identity);
        confirmSound.transform.parent = null;
    }

    public void SpawnDeny()
    {
        Instantiate(denySound, transform.position, Quaternion.identity);
        denySound.transform.parent = null;
    }

    public void SpawnNormal()
    {
        Instantiate(normalSound, transform.position, Quaternion.identity);
        normalSound.transform.parent = null;
    }
}
