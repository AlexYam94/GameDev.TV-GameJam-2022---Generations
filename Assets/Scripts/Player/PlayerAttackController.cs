using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    PlayerAnimationController _anim;
    PlayerAbilityController _abiltyController;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<PlayerAnimationController>();
        _abiltyController = GetComponent<PlayerAbilityController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            PlayerPrefs.DeleteAll();
        }
        if (_abiltyController.canRestore && Input.GetKeyDown(KeyCode.J))
        {
            _anim.Attack();
        }
    }
}
