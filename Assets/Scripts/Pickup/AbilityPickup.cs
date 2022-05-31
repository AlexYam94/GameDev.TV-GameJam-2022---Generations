using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerAbilityController;

public class AbilityPickup : MonoBehaviour
{
    [SerializeField] AbilityEnum _type;
    [SerializeField] string _abilityUnlockMsg;
    [SerializeField] TextMeshProUGUI _text;

    private void Start()
    {
        switch (_type)
        {
            case AbilityEnum.restart:
                if (PlayerPrefs.GetInt("canRestart") == 1) Destroy(gameObject);
                break;
            case AbilityEnum.dash:
                if (PlayerPrefs.GetInt("canDash") == 1) Destroy(gameObject);
                break;
            case AbilityEnum.doubleJump:
                if (PlayerPrefs.GetInt("canDoubleJump") == 1) Destroy(gameObject);
                break;
            case AbilityEnum.restore:
                if (PlayerPrefs.GetInt("canRestore") == 1) Destroy(gameObject);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<PlayerAbilityController>()?.UnlockAbility(_type);
        _text.text = _abilityUnlockMsg;
        _text.gameObject.SetActive(true);
        Destroy(gameObject);
    }

}
