using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour, ISavable
{
    public bool canDoubleJump { get; set; } = false;
    public bool canDash { get; set; } = false;
    public bool canRestart { get; set; } = false;
    public bool canRestore { get; set; } = false;

    public void Load()
    {
        //Load ability unlock progress
        if (PlayerPrefs.GetInt("canDoubleJump") == 1) canDoubleJump = true;
        if (PlayerPrefs.GetInt("canDash") == 1) canDash = true;
        if (PlayerPrefs.GetInt("canRestart") == 1) canRestart = true;
        if (PlayerPrefs.GetInt("canRestore") == 1) canRestore = true;
    }

    public void Save()
    {
        //Save ability unlock progress
        PlayerPrefs.SetInt("canDoubleJump", canDoubleJump ? 1 : 0);
        PlayerPrefs.SetInt("canDash", canDash ? 1 : 0);
        PlayerPrefs.SetInt("canRestart", canRestart ? 1 : 0);
        PlayerPrefs.SetInt("canRestore", canRestore ? 1 : 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void UnlockAbility(AbilityEnum abilitiyToUnlock)
    {
        switch (abilitiyToUnlock)
        {
            case AbilityEnum.restart:
                canRestart = true;
                break;
            case AbilityEnum.dash:
                canDash = true;
                break;
            case AbilityEnum.doubleJump:
                canDoubleJump = true;
                break;
            case AbilityEnum.restore:
                canRestore = true;
                break;
            case AbilityEnum.resource:
                GetComponent<PlayerResourceController>().IncreaseMaxResource();
                break;
        }
        Save();
    }

    public enum AbilityEnum
    {
        doubleJump,
        dash,
        restart,
        restore,
        resource

    }
}
