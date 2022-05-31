using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool _isTrapTriggered;

    // Start is called before the first frame update
    void Start()
    {
        _isTrapTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isTrapTriggered)
        {
            _isTrapTriggered = true;
            collision.GetComponentInParent<PlayerBodiesController>().SnapShotNewBodyPos(transform.position);
            //collision.GetComponentInParent<PlayerBodiesController>().AddBody();
            collision.GetComponentInParent<PlayerDeathController>().Die();
        }
    }

    public void ResetTrap()
    {
        _isTrapTriggered = false;
    }

}
