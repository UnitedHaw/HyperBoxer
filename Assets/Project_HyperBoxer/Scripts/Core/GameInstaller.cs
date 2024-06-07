using Assets.Project_HyperBoxer.Scripts.Combat;
using Assets.Project_HyperBoxer.Scripts.Controllers;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private CombatBase _playerCombat;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = new PlayerController();
        _playerController.Setup(_playerCombat);
    }

    private void OnDisable()
    {
        _playerController.Dispose();
    }
}
