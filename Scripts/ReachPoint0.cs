using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachPoint0 : MonoBehaviour
{
    public static ReachPoint0 instance;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isSinglePLayer)
        {
            if (index >= 2)
            {
                index = 0;
                PairManagerSingle.instance.Permission();
                

            }
        }
        else
        {
            if (index >= 2)
            {
                index = 0;
                PairManager.instance.Permission();
                PairManager.instance.TrocaTurno();

            }
        }

    }
}
