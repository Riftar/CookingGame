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
    public bool ItemKuah { get; set; }
    public bool ItemJeroan { get; set; }
    public bool ItemKubis { get; set; }
    public bool ItemKoya { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ItemNasi = false;
        ItemAyam = false;
        ItemTelur = false;
        ItemKuah = false;
        ItemJeroan = false;
        ItemKubis = false;
        ItemKoya = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(string name)
    {
        namaItem.text = name;

        GameObject item = GameObject.FindGameObjectWithTag(name);
        Debug.Log(name);
        Image myImage = item.GetComponent<Image>();
        Color tempColor = myImage.color;
        tempColor.a = 1f;                                                  //munculin gambar sesuai item
        myImage.color = tempColor;
        simpanItem(name, true);                                            //set bool item ke true

        if(name == "ItemKuah")
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
        
        if(ItemNasi && ItemAyam && ItemKoya && !ItemKubis && !ItemJeroan && !ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[0], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //makanan[0].SetActive(true);
            //Debug.Log("nasi, ayam, koya");
        }
        else if (ItemNasi && ItemAyam && ItemKoya && ItemKubis && !ItemJeroan && !ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[1], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //Debug.Log("nasi, ayam, koya, kubis");
        }
        else if(ItemNasi && ItemAyam && ItemKoya && !ItemKubis && ItemJeroan && !ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[2], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //Debug.Log("nasi, ayam, koya, jeroan");
        }
        else if(ItemNasi && ItemAyam && ItemKoya && !ItemKubis && !ItemJeroan && ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[3], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //Debug.Log("nasi, ayam, koya, telur");
        }
        else if(ItemNasi && ItemAyam && ItemKoya && ItemKubis && ItemJeroan && !ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[4], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //Debug.Log("nasi, ayam, koya, kubis, jeroan");
        }
        else if(ItemNasi && ItemAyam && ItemKoya && ItemKubis && !ItemJeroan && ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[5], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //Debug.Log("nasi, ayam, koya, kubis, telur");
        }
        else if (ItemNasi && ItemAyam && ItemKoya && !ItemKubis && ItemJeroan && ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[6], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //Debug.Log("nasi, ayam, koya, jeroan, telur");
        }
        else if (ItemNasi && ItemAyam && ItemKoya && ItemKubis && ItemJeroan && ItemTelur)
        {
            GameObject MakananJadi = Instantiate(makanan[7], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
            //Debug.Log("komplit");
        }
        else
        {
            GameObject MakananJadi = Instantiate(makanan[8], transform);
            MakananJadi.transform.SetParent(dragCell.transform);
        }
        resetOpacity();
    }

    void resetOpacity()
    {
        foreach (Transform child in transform)                      //akses semua child dr piring, yg mana semua item makanan
        {
            //Debug.Log(child.gameObject.name);
            Image myImage = child.gameObject.GetComponent<Image>();
            Color tempColor = myImage.color;
            tempColor.a = 0f;                                                  //ngilangin gambar sesuai item
            myImage.color = tempColor;

            ItemNasi = false;
            ItemAyam = false;
            ItemTelur = false;
            ItemKuah = false;
            ItemJeroan = false;
            ItemKubis = false;
            ItemKoya = false;
        }
    }
}
