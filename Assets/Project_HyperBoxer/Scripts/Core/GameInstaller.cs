using Assets.Project_HyperBoxer.Scripts.Combat;
using Assets.Project_HyperBoxer.Scripts.Controllers;
using Assets.Project_HyperBoxer.Scripts.UI;
using Assets.Project_HyperBoxer.Scripts.UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private CombatBase _playerCombat;
    private PlayerController _playerController;
    private GameplayWindow _sceneWindow;
    private void Awake()
    {
        _sceneWindow = new GameplayWindow(_uiDocument);

        _playerController = new PlayerController(_sceneWindow.GetWindow<CombatControlWindow>());
        _playerController.Setup(_playerCombat);
    }

    private void OnDisable()
    {
        _playerController.Dispose();
    }
}
