#define endlessMode     //define apakah game ini endless
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menuPrefab;
    [SerializeField]
    private GameObject menuHolder;
    [SerializeField]
    private Text resultText;
    [SerializeField]
    private Slider timer;
    [SerializeField]
    private Image sliderImage;

    [SerializeField]                        // dibedakan di tiap prefab customer berdasarkan level
    private int minLevelMakanan;            //makin gede range makin komplit
    [SerializeField]                        // dibedakan di tiap prefab customer berdasarkan level
    private int maxLevelMakanan;            //makin gede range makin komplit
    [SerializeField]
    private Animator anim;

    private Makanan makanan;
    private GameObject player;
    private DragAndDropCell dragCell;
    private DragAndDropItem dragItem;
    private GameController gameCont;
    private float timeLeft = 40f;

    private string currentMenu;

    // Start is called before the first frame update
    void Start()
    {
        timer.maxValue = timeLeft;
        gameCont = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = transform.parent.gameObject;
        int rand = Random.Range(minLevelMakanan, maxLevelMakanan);
        GameObject Order = Instantiate(menuPrefab[rand], menuHolder.transform.position, Quaternion.identity);
        //Debug.Log(rand);
        currentMenu = menuPrefab[rand].tag.ToString();
        Order.transform.SetParent(menuHolder.transform, false);
    }
    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timer.value = timeLeft;
      
        if (timeLeft < 0)
        {
            //cek if(!isCoroutineStarted)
            //{
            //    StartCoroutine("MyCoroutine");
            //}
            int index = int.Parse(this.transform.parent.parent.gameObject.tag);
            gameCont.spawnPointKosong[index - 1] = true;

#if !endlessMode
            gameCont.nyawaKurang();
#endif

            StartCoroutine(WaitforFunction("destroy", 1f));

        }
    }



    public void cekOrder(string name)
    {
        Debug.Log(name);
        if(currentMenu == name)
        {
            cekTime();

            //order bener
            //code smell duplicate code dgn order salah
            resultText.text = "Hore!";
            resultText.color = Color.green;
            dragCell = gameObject.GetComponent<DragAndDropCell>();                //get drag cell component form this object

            gameCont.duit += dragCell.descPublic.item.GetComponent<Makanan>().makananData.Harga; //nambah duit sesuai harga 
                                                                                  // diambil dari item yg didrag, bisa jg dari item yg digenerate random di script ini
            dragCell.descPublic.sourceCell.gameObject.SetActive(false);           //access sourceCell and then deactive it
            dragCell.transform.GetChild(0).gameObject.SetActive(false);           //hapus child (item)
            gameCont.customerDone();

            StartCoroutine(WaitforFunction("destroy", 1f));
        }
        else
        {
            //order salah
            
            resultText.text = "YEK!";
            resultText.color = Color.red;
            dragCell = gameObject.GetComponent<DragAndDropCell>();                //get drag cell component form this object
            dragCell.transform.GetChild(0).gameObject.SetActive(false);           //hapus child (item)
#if !endlessMode                                                                  // di endless mode tdk perlu dibuang
            dragCell.descPublic.item.GetComponent<Image>().color = Color.red;
#endif
            //StartCoroutine(WaitforFunction("destroy", 1f));                      // gak jadi di destroy karna jadinya masih dikasih kesempatan

#if endlessMode
            dragCell.descPublic.sourceCell.gameObject.SetActive(false);           //access sourceCell and then deactive it
            StartCoroutine(WaitforFunction("destroy", 1f));
#endif
        }

    }

    void cekTime()
    {
        if(timeLeft > 30)
        {
            //dapet bonus
            gameCont.duit += 2000;
            Debug.Log("Nambah bonus 2000");
        }

#if !endlessMode
        if (timeLeft < 10 && timeLeft > 0)
        {
            //dapet teguran
            gameCont.duit -= 500;
            Debug.Log("NYAWA KURANG 500");
        }
#endif

    }

    IEnumerator WaitforFunction(string function, float time)
    {
        if(function == "destroy")
        {        
            yield return new WaitForSeconds(time);
            Destroy(player);
            int index = int.Parse(this.transform.parent.parent.gameObject.tag);
            gameCont.spawnPointKosong[index-1] = true;
            gameCont.currentCustomer--;                                          //biar spawn cust baru
            Debug.Log("Current cust: " + gameCont.currentCustomer);
        }
    }
}
