using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Sound_Manager.instance.OnPlayClip(0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Sound_Manager.instance.OnPlayClip(1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Sound_Manager.instance.OnPlayOneShot(2);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Sound_Manager.instance.OnStop(); ;
        }
    }
}
