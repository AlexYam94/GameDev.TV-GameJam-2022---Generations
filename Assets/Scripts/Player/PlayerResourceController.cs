using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerResourceController : MonoBehaviour, ISavable
{
    [SerializeField] int _maxResources;
    [SerializeField] TextMeshProUGUI _resourceText;

    private int _currentResource;


    // Start is called before the first frame update
    void Start()
    {
        _currentResource = _maxResources;
        Load();
        GetComponent<PlayerDeathController>().OnDie += ConsumeResource;

    }
    
    void Update(){
        //TODO:update resource ui
        _resourceText.text = "Resource: " + _currentResource + "/" + _maxResources;
    }
    
    public void ConsumeResource(){
        _currentResource -= 1;
    }

    public void RestoreResource(){
        if (_currentResource + 1 <= _maxResources)
        {
            _currentResource += 1;
        }
    }

    public void Refill()
    {
        _currentResource = _maxResources;
    }

    public void IncreaseMaxResource(){
        _maxResources += 1;
        _currentResource += 1;
        Save();
    }

    public int GetCurrentResource()
    {
        return _currentResource;
    }

    public void Load()
    {
        int temp = _maxResources;
        //_currentResource = PlayerPrefs.GetInt("CurrentResource", _maxResources);
        _maxResources = PlayerPrefs.GetInt("MaxResource", _maxResources);
        _currentResource = _maxResources;

    }

    public void Save()
    {
        //PlayerPrefs.SetInt("CurrentResource", _currentResource);
        int temp = PlayerPrefs.GetInt("MaxResource");
        if (temp <= 0 || temp < _maxResources)
        {
            PlayerPrefs.SetInt("MaxResource", _maxResources);
        }
    }
}
