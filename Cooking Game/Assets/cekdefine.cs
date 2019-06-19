#define notEndlessMode
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cekdefine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("called normally");

#if !endlessMode
        Debug.Log("called in define");
#endif

#if notEndlessMode
        Debug.Log("called in define notEndless");
#endif


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
