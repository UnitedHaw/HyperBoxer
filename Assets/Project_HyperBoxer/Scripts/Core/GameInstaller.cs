using Assets.Project_HyperBoxer.Scripts.Combat;
using Assets.Project_HyperBoxer.Scripts.Controllers;
using Assets.Project_HyperBoxer.Scripts.Test;
using Assets.Project_HyperBoxer.Scripts.UI;
using Assets.Project_HyperBoxer.Scripts.UI.Base;
using Reflex.Attributes;
using Reflex.Core;
using UnityEngine;
using UnityEngine.UIElements;
using CharacterController = Assets.Project_HyperBoxer.Scripts.Controllers.CharacterController;

public class GameInstaller : MonoBehaviour
    //, IInstaller
{
    [SerializeField] private Player _pfPlayer;
    [SerializeField] private EnemySpawner _enemySpawner;
    [Inject] private GameplayWindow _sceneWindow;
    [Inject] private UIDocument _uiDocument;
    private PlayerController _playerController;

    //public void InstallBindings(ContainerBuilder containerBuilder)
    //{
    //    _playerController = new PlayerController(_pfPlayer, _sceneWindow.GetWindow<CombatControlWindow>());

    //    containerBuilder
    //        .AddSingleton(_playerController, typeof(CharacterController))
    //        .AddSingleton(_enemySpawner);
    //}

    private void Start()
    {
        
    }

    private void OnDisable()
    {
        _playerController.Dispose();
    }
}
