using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;


public class ListBox : MonoBehaviour
{
    public Sprite BoxImage;
    public Text Text;
    public GameObject Weapon;
    public Vector3 Position;
    public GameObject Prefab;

    public ListBox(Sprite _Image,string _Text,GameObject _Weapon,Vector3 _Position)
    {
        Prefab = this.gameObject;
        BoxImage = _Image;
        Text.text = _Text;
        Position = _Position;
        try
        {
            Weapon = _Weapon;
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
}
