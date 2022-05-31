using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionSaver : MonoBehaviour
{
    //public void Load()
    //{
    //    string positionStr = PlayerPrefs.GetString("PlayerPosition", null);
    //    if(positionStr == null || positionStr == "") return;
    //    byte[] positionByte = Convert.FromBase64String(positionStr);
        
    //    Vector2 position = ByteArrayConverter.ToPosition(positionByte);
        
    //}

    //public void Save()
    //{
    //    byte[] positionBytes = ByteArrayConverter.ToByteArray(transform.position);
    //    string base64 = Convert.ToBase64String(positionBytes);
    //    PlayerPrefs.SetString("PlayerPosition", base64);
    //}

    // Start is called before the first frame update
    void Start()
    {
        //Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
