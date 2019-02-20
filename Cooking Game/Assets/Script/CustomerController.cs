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
    
    [SerializeField]                        // dibedakan di tiap prefab customer berdasarkan level
    private int maxLevelMakanan;            //makin gede makin komplit

    private GameObject player;
    private DragAndDropCell dragCell;
    private DragAndDropItem dragItem;
    private GameController gameCont;
    private float timeLeft = 15f;

    private string currentMenu;

    // Start is called before the first frame update
    void Start()
    {
        timer.maxValue = timeLeft;
        gameCont = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = transform.parent.gameObject;
        int rand = Random.Range(0, maxLevelMakanan);
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
            gameCont.nyawaKurang();
            Destroy(player);

        }
    }



    public void cekOrder(string name)
    {
        Debug.Log(name);
        if(currentMenu == name)
        {
            //order bener
            resultText.text = "Hore!";
            resultText.color = Color.green;
            dragCell = gameObject.GetComponent<DragAndDropCell>();                //get drag cell component form this object
            dragCell.descPublic.sourceCell.gameObject.SetActive(false);           //access sourceCell and then deactive it
            dragCell.transform.GetChild(0).gameObject.SetActive(false);           //hapus child (item)
            gameCont.customerDone();

            StartCoroutine(WaitforFunction("destroy", 1f));
        }
        else
        {
            //order salah
            gameCont.nyawaKurang();
            resultText.text = "YEK!";
            resultText.color = Color.red;
            dragCell = gameObject.GetComponent<DragAndDropCell>();                //get drag cell component form this object
            dragCell.descPublic.sourceCell.gameObject.SetActive(false);           //access sourceCell and then deactive it
            dragCell.transform.GetChild(0).gameObject.SetActive(false);           //hapus child (item)
            StartCoroutine(WaitforFunction("destroy", 1f));
        }

    }


    IEnumerator WaitforFunction(string function, float time)
    {
        if(function == "destroy")
        {        
            yield return new WaitForSeconds(time);
            Destroy(player);
            int index = int.Parse(this.transform.parent.parent.gameObject.tag);
            gameCont.spawnPointKosong[index-1] = true;
        }
    }
}
