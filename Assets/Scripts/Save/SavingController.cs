using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SavingController : MonoBehaviour
{
    public void Save()
    {
        ISavable[] objs = FindObjectsOfType<MonoBehaviour>().OfType<ISavable>().ToArray();
        foreach (ISavable obj in objs){
            obj.Save();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
