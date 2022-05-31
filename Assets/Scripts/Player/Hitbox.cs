using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitbox : MonoBehaviour
{
    [SerializeField] AudioSource _slapEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            GetComponentInParent<PlayerBodiesController>().RemoveBody(collision.gameObject);
            GetComponentInParent<PlayerResourceController>().RestoreResource();
            _slapEffect.Play();
            Destroy(collision.gameObject);
        }
    }
}
