using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MakananData", menuName = "Makanan Data", order = 51)]

public class MakananData : ScriptableObject
{
    [SerializeField]
    private string nama;

    [SerializeField]
    private float harga;


    public string Nama
    {
        get { return nama; }
    }

    public float Harga
    {
        get { return harga; }
    }
}
