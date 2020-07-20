﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] customerPrefab;            //masukan semua level prefab customer, bedanya di range makanan yg dipesan
    [SerializeField]
    private GameObject[] playerHolder;
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private GameObject[] panelNyawa;

    [SerializeField]
    private GameObject[] customer;

    [SerializeField]
    private Text custText;
    [SerializeField]
    private Text duitText;

    private int maxCustomer = 1;
    public int currentCustomer = 0;
    private float levelUpTime = 50f;

    public bool[] spawnPointKosong = new bool[3];
    public int nyawa = 5;
    int custDone = 0;
    public float duit = 0f;
    public int currentlevelCustomer = 0;


    [SerializeField]
    private Text customerLevelText;
    [SerializeField]
    private Text maxCustomerText;

    void Awake()
    {
        InvokeRepeating("tambahSusah", levelUpTime, levelUpTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPointKosong[0] = true;
    }

    /// <summary>
    /// obsolete. bcs intantiating create a problem on csutomer scale/position.
    /// cek if spawn point kosong, intantiate as a current level customer & set parent, spawn point !kosong
    /// </summary>
    public void spawn()
    {
        //ienumerator wait to spawn
        if (spawnPointKosong[0] == true)
        {
           currentCustomer++;         
           GameObject Customer = Instantiate(customerPrefab[currentlevelCustomer], playerHolder[0].transform.position, Quaternion.identity);
           Debug.Log("Spawn " + currentCustomer);
           Customer.transform.SetParent(playerHolder[0].transform, true);
           spawnPointKosong[0] = false;
        }
        
    }

    /// <summary>
    /// new function. instead of using instantiate, im using active/deactive
    /// </summary>
    void activateCustomer()
    {
        int rand = Random.Range(0, 2);
        if (spawnPointKosong[0] == true)
        {
            currentCustomer++;
            customer[rand].SetActive(true);
            spawnPointKosong[0] = false;
        }
    }

    void Update()
    {        
        customerLevelText.text = "Customer level: " + customerPrefab[currentlevelCustomer].name.ToString();
        maxCustomerText.text = "maxCsutomer: " + maxCustomer.ToString();
        //duitText.text = string.Format("{0:C}",duit);
        //duitText.text = "Rp. " + string.Format("{0:N2}",duit);
        duitText.text = duit.ToString();
        //spawn();
        activateCustomer();
       

        if(Input.GetKeyDown(KeyCode.D))
        {
                spawn();
        }

        if (nyawa <= 0)
        {
            //game over
            gameOverCanvas.SetActive(true);
        }
    }

    public void nyawaKurang() //parsing jumlah duit kurang
    {
        duit -= 50;
        nyawa -= 1;
        currentCustomer--;
        //Debug.Log(nyawa);
        panelNyawa[nyawa].SetActive(false);      
    }

    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void tambahSusah()
    {
        if (currentlevelCustomer < customerPrefab.Length - 1)
        {
            currentlevelCustomer++;
        }

        if (maxCustomer <4)                    //dibikin if biar ada maksimalnya
        {
            maxCustomer++;
           // custTime = custTime * 1.5f;
            //Debug.Log(maxCustomer);
        }
    }

    public void customerDone()
    {
        //kalo pesenan bener
        custDone++;
        //duit += 1000f;
        custText.text = "Customer Done: " + custDone.ToString();
       
    }
}
