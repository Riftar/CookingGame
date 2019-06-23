#define notEndlessMode
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cekdefine : MonoBehaviour
{
    [SerializeField]
    GameObject[] customer;
    bool active = true;

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
        if (Input.GetKeyDown(KeyCode.A))
        {
            spawn(0, active);
            active = !active;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            spawn(1, active);
            active = !active;
        }
    }

    void spawn(int index, bool active)
    {
        customer[index].SetActive(active);
    }

    public void spawn1()
    {
        customer[0].SetActive(active);
        active = !active;
    }

    public void spawn2()
    {
        customer[1].SetActive(active);
        active = !active;
    }

}
