using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] customerPrefab;            //masukan semua level prefab customer, bedanya di range makanan yg dipesan
    private GameObject currentLevelCustomer;
    [SerializeField]
    private GameObject[] playerHolder;
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private GameObject[] panelNyawa;

    [SerializeField]
    private Text custText;
    [SerializeField]
    private Text duitText;

    private int maxCustomer = 1;
    public int currentCustomer = 0;
    private float custTime = 50f;

    public bool[] spawnPointKosong = new bool[4];
    public int nyawa = 5;
    int custDone = 0;
    float duit = 900000f;
    int currentPrefabNo = 0;


    [SerializeField]
    private Text customerLevelText;
    [SerializeField]
    private Text maxCustomerText;

    // Start is called before the first frame update
    void Start()
    {
        currentLevelCustomer = customerPrefab[currentPrefabNo];
        for(int i = 0; i <=3; i++)
        {
            spawnPointKosong[i] = true;
        }

        InvokeRepeating("tambahSusah", custTime, custTime);

    }
    public void spawn()
    {
        //ienumerator wait to spawn

        int rand = Random.Range(0,4);
        if (spawnPointKosong[rand] == true)
        {
           currentCustomer++;         
           GameObject Customer = Instantiate(customerPrefab[currentPrefabNo], playerHolder[rand].transform.position, Quaternion.identity);
           Debug.Log("Spawn " + currentCustomer);
           Customer.transform.SetParent(playerHolder[rand].transform, true);
           spawnPointKosong[rand] = false;
        }
        
    }

    void Update()
    {
        customerLevelText.text = "Customer level: " + customerPrefab[currentPrefabNo].name.ToString();
        maxCustomerText.text = "maxCsutomer: " + maxCustomer.ToString();
        duitText.text = string.Format("{0:C}",duit);
        if (currentCustomer < maxCustomer)
        {
            spawn();
        }

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
        duit -= 500;
        nyawa -= 1;
        currentCustomer--;
        Debug.Log(nyawa);
        panelNyawa[nyawa].SetActive(false);      
    }

    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void tambahSusah()
    {
        if(currentPrefabNo < customerPrefab.Length-1)
        {
            currentPrefabNo++;
        }
       
        if (maxCustomer <4)                    //dibikin if biar ada maksimalnya
        {
            maxCustomer++;
           // custTime = custTime * 1.5f;
            Debug.Log(maxCustomer);
        }
    }

    public void customerDone()
    {
        //kalo pesenan bener
        custDone++;
        duit += 1000f;
        currentCustomer--;
        custText.text = "Customer Done: " + custDone.ToString();
       
    }
}
