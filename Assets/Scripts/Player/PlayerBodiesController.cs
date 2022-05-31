using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodiesController : MonoBehaviour
{
    [SerializeField] GameObject[] _playerBodies;

    //TODO: Create a new class for holding player body position and is body floating
    List<Vector2> _bodiesPosition = new List<Vector2>();
    List<GameObject> _spawnedBodies = new List<GameObject>();
    PlayerDeathController _playerDeathController;

    Vector2 _newBodyPos;
    bool _isBodySnapShot;
    // Start is called before the first frame update
    void Start()
    {
        //Load();
        _playerDeathController = GetComponent<PlayerDeathController>();
        _playerDeathController.BeforeDie += SnapShotNewBodyPos;
        _playerDeathController.OnDie += AddBody;
        _playerDeathController.OnGameOver += ClearBodies;
        _isBodySnapShot = false;
        //TODO: Instantiate body object in bodies position
        SpawnBodies();
    }

    private void Update()
    {
    }

    public void SnapShotNewBodyPos()
    {
        if (_isBodySnapShot) return;
        _newBodyPos = transform.position;
        _isBodySnapShot = true;
    }

    public void SnapShotNewBodyPos(Vector2 pos)
    {
        if (_isBodySnapShot) return;
        _newBodyPos = pos;
        _isBodySnapShot = true;
    }

    //TODO: Call when player died
    public void AddBody(){
        if (!_isBodySnapShot) return;
        _isBodySnapShot = false;
        _bodiesPosition.Add(_newBodyPos);
        //TODO: SpawnBody
        System.Random rand = new System.Random();
        int bodyIdx = rand.Next(0, _playerBodies.Length);
        SpawnBody(_playerBodies[bodyIdx], _newBodyPos);
    }

    private void SpawnBodies(){
        System.Random rand = new System.Random();
        foreach(var pos in _bodiesPosition){
            int bodyIdx = rand.Next(0,_playerBodies.Length);
            SpawnBody(_playerBodies[bodyIdx], pos);
        }
    }

    private void SpawnBody(GameObject body, Vector2 position){
        GameObject obj = Instantiate(body, position, body.transform.rotation);
        _spawnedBodies.Add(obj);
        obj.GetComponent<Body>().SetGeneration(GetComponent<PlayerGenerationController>().GetGeneration());
    }

    //Call when clear each level
    //Call when resouce < 0
    public void ClearBodies(){
        foreach (var body in _spawnedBodies)
        {
            Destroy(body);
        }
        _spawnedBodies.Clear();
        _bodiesPosition.Clear();
        PlayerPrefs.DeleteKey("PlayerBodiesPosition");
    }

    public void RemoveBody(GameObject body) { 
       _bodiesPosition.Remove(body.transform.position);
    }

    //public void Load()
    //{
    //    //Load Player body Position base64 string from PlayerPref and convert to base64 byte array, each 3 bytes represent x,y,z
    //    string base64 = PlayerPrefs.GetString("PlayerBodiesPosition", null);
    //    if (base64 == null || base64 == "") return;
    //    byte[] arr = Convert.FromBase64String(base64);
    //    _bodiesPosition = ByteArrayConverter.ToPositions(arr);

    //}

    //public void Save()
    //{
    //    //Convert Player bodies position to byte array, each 3 bytes represent x,y. Convert the byte array into base64 encoded string
    //    byte[] arr = ByteArrayConverter.ToByteArray(_bodiesPosition);
    //    string base64 = Convert.ToBase64String(arr);
    //    PlayerPrefs.SetString("PlayerBodiesPosition", base64);
    //}
}
