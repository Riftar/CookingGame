using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiringController : MonoBehaviour
{

    [SerializeField]
    public Text namaItem;
    [SerializeField]
    public GameObject dragCell;

    [SerializeField]
    public GameObject[] makanan;


    public bool ItemNasi { get; set; }
    public bool ItemAyam { get; set; }
    public bool ItemTelur { get; set; }
    public bool ItemKuahKuningBening { get; set; }
    public bool ItemKuahPutihBening { get; set; }
    public bool ItemJeroan { get; set; }
    public bool ItemPerkedel { get; set; }
    public bool ItemDaging { get; set; }
    public bool ItemSoun { get; set; }
    public bool ItemKoya { get; set; }
    public bool ItemEsTeh { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ItemNasi = false;
        ItemAyam = false;
        ItemTelur = false;
        ItemKuahKuningBening = false;
        ItemKuahPutihBening = false;
        ItemJeroan = false;
        ItemPerkedel = false;
        ItemSoun = false;
        ItemKoya = false;
        ItemEsTeh = false;
        SetItem("ItemEsTeh");    //dipanggil di start karna gak pake teko teh
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(string name)
    {
        namaItem.text = name;

        GameObject item = GameObject.FindGameObjectWithTag(name);
        //Debug.Log(name);
        Image myImage = item.GetComponent<Image>();
        Color tempColor = myImage.color;
        tempColor.a = 1f;                                                  //munculin gambar sesuai item
        myImage.color = tempColor;
        simpanItem(name, true);                                            //set bool item ke true

        if(name == "ItemKuahKuningBening" || name=="ItemKuahPutihBening")
        {
            cekMakanan();                                                  //item kuah terakhir buat cek makanan bener/gak
        }
    }

    private void simpanItem(string name, bool value)
    {
        this.GetType().GetProperty(name).SetValue(this, value);
        //Debug.Log(name + value);
    }

    void cekMakanan()
    {
        dragCell.SetActive(true);                               //aktivasi drag cell
        
        if(ItemNasi && ItemAyam && ItemKuahKuningBening && ItemSoun && ItemKoya && ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[0], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //makanan[0].SetActive(true);
            //Debug.Log("nasi, ayam, koya");
        }
        else if (ItemNasi && ItemAyam && ItemKuahPutihBening && ItemSoun && ItemPerkedel && ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[1], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //Debug.Log("nasi, ayam, koya, kubis");
        }
        else if(ItemNasi && ItemDaging && ItemKuahKuningBening && ItemSoun && ItemJeroan && ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[2], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //Debug.Log("nasi, ayam, koya, jeroan");
        }
        else
        {
            GameObject MakananJadi = Instantiate(makanan[3], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
        }
        resetOpacity();
    }

    public void dragGelasOn()
    {
        dragCell.SetActive(true);
        GameObject MakananJadi = Instantiate(makanan[0], transform);
        MakananJadi.transform.SetParent(dragCell.transform);
        resetOpacity();
    }

    void resetOpacity()
    {
        foreach (Transform child in transform)                      //akses semua child dr piring, yg mana semua item makanan
        {
            //Debug.Log(child.gameObject.name);
            if(child.gameObject.GetComponent<Image>() != null)
            {
                Image myImage = child.gameObject.GetComponent<Image>();
                Color tempColor = myImage.color;
                tempColor.a = 0f;                                                  //ngilangin gambar sesuai item
                myImage.color = tempColor;
            }
           


            //TODO ini boiler plate harus diperbaiki, karena childnya gak langsung item
            foreach(Transform child2 in child.transform)
            {
                //Debug.Log(child.gameObject.name);
                if (child2.gameObject.GetComponent<Image>() != null)
                {
                    Image myImage2 = child2.gameObject.GetComponent<Image>();
                    Color tempColor2 = myImage2.color;
                    tempColor2.a = 0f;                                                  //ngilangin gambar sesuai item
                    myImage2.color = tempColor2;

                }

            }

            ItemNasi = false;
            ItemAyam = false;
            ItemTelur = false;
            ItemKuahKuningBening = false;
            ItemKuahPutihBening = false;
            ItemJeroan = false;
            ItemPerkedel = false;
            ItemDaging = false;
            ItemSoun = false;
            ItemKoya = false;
            ItemEsTeh = false;
        }
    }
}
