using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    int _generation = 0;

    public void SetGeneration(int gen)
    {
        _generation = gen;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _generation + "";
    }
}
