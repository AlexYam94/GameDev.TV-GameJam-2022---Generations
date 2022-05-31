using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Transform _spawnPoint;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            //TODO: save player respawn point to spawn point
            if (other.GetComponentInParent<PlayerDeathController>().GetRespawnPoint() != _spawnPoint.position)
            {
                other.GetComponentInParent<PlayerDeathController>().SetRespawnPoint(_spawnPoint);
                other.GetComponentInParent<PlayerResourceController>().Refill();
                StartCoroutine("ShowCheckPointText");
            }
            FindObjectOfType<SavingController>()?.Save();
        }
    }

    IEnumerator ShowCheckPointText()
    {
        _text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _text.gameObject.SetActive(false);
    }
    
}
