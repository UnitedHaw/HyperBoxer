using Assets.Project_HyperBoxer.Scripts.Combat;
using Assets.Project_HyperBoxer.Scripts.Controllers;
using Assets.Project_HyperBoxer.Scripts.Test;
using Assets.Project_HyperBoxer.Scripts.UI;
using Assets.Project_HyperBoxer.Scripts.UI.Base;
using Reflex.Attributes;
using Reflex.Core;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private CombatBase _playerCombat;
    [Inject] private GameplayWindow _sceneWindow;
    [Inject] private UIDocument _uiDocument;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = new PlayerController(_sceneWindow.GetWindow<CombatControlWindow>());
        _playerController.Setup(_playerCombat);
    }

    private void Start()
    {
        Debug.Log(_uiDocument.rootVisualElement.name);
    }

    private void OnDisable()
    {
        _playerController.Dispose();
    }
}
