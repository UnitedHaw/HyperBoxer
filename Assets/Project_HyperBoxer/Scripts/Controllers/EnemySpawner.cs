using Assets.Project_HyperBoxer.Scripts.Controllers;
using Reflex.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using CharacterController = Assets.Project_HyperBoxer.Scripts.Controllers.CharacterController;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _pfEnemy;
    private BotController _botController;
    private ObjectPool<Enemy> _enemyPool;
    private Enemy _currentEnemy;
    [Inject] private CharacterController _player;
    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(() => Instantiate(_pfEnemy));
    }

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        _currentEnemy = _enemyPool.Get();
        _currentEnemy.transform.position = _player.Character.transform.position + Vector3.forward * 5;


        if (_botController == null)
        {
            _botController = new BotController(_currentEnemy);
        }else
        {
            _botController.ChangeCharacter(_currentEnemy);
        }
    }
}
