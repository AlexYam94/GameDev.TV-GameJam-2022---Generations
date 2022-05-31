using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] float _deathAnimationDuration = 1f;

    public delegate void OnEventHappenedHandler();
    public event OnEventHappenedHandler BeforeDie;
    public event OnEventHappenedHandler OnDie;
    public event OnEventHappenedHandler OnGameOver;

    private Vector3 _spawnPosition;
    private PlayerResourceController _playerResourceController;
    private bool _isDying;

    public bool canInput { get; set; }

    void Start()
    {
        _spawnPosition = transform.position;
        canInput = true;
        _isDying = false;
        _playerResourceController = GetComponent<PlayerResourceController>();
    }

    private void Update()
    {
        if (!canInput) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
    }

    public void Respawn(){
        transform.position = _spawnPosition;
        Trap[] traps = FindObjectsOfType<Trap>();
        foreach(var trap in traps)
        {
            trap.ResetTrap();
        }
    }

    public void GameOver()
    {
        OnGameOver();
        SceneController.GetInstance().GameOver();
    }

    public void SetRespawnPoint(Transform transform){
        _spawnPosition = transform.position;
    }

    public Vector3 GetRespawnPoint()
    {
        return _spawnPosition;
    }

    public void Die(){
        if (!_isDying)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        _isDying = true;
        //TODO: 
        //Disable player Input
        GetComponent<PlayerMovementController>().enabled = false;
        GetComponent<PlayerAttackController>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //fade in black screen behind player
        BeforeDie();
        GetComponent<PlayerAnimationController>().Death();
        yield return new WaitForSeconds(_deathAnimationDuration);
        //Play player death animation
        //Instantiate body
        OnDie();
        GetComponent<PlayerGenerationController>().NewGeneration();
        GetComponent<PlayerMovementController>().enabled = true;
        GetComponent<PlayerAttackController>().enabled = true;
        if (_playerResourceController.GetCurrentResource() >= 0)
        {
            Respawn();
        }
        else
        {
            GameOver();
        }

        _isDying = false;
    }
}
