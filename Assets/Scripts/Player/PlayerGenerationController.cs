using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGenerationController : MonoBehaviour, ISavable
{
    [SerializeField] TextMeshProUGUI _text;

    private int _generation = 0;

    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _generation + "";    
    }

    public void NewGeneration()
    {
        _generation++;
        Save();
    }

    public int GetGeneration()
    {
        return _generation;
    }

    public void Save()
    {
        int temp = PlayerPrefs.GetInt("GenerationCount");
        if (temp <= 0 || temp < _generation)
        {
            PlayerPrefs.SetInt("GenerationCount", _generation);
        }
    }

    public void Load()
    {
        _generation = PlayerPrefs.GetInt("GenerationCount", 0);
    }
}
