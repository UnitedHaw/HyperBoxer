using Assets.Project_HyperBoxer.Scripts.Combat;
using Assets.Project_HyperBoxer.Scripts.UI;
using Reflex.Attributes;
using Reflex.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Loader : MonoBehaviour
{
    [Inject] private UIDocument _uiDocument;
    private void Start()
    {
        DontDestroyOnLoad(_uiDocument);

        var scene = SceneManager.LoadScene("TestCombat", new LoadSceneParameters(LoadSceneMode.Single));
        ReflexSceneManager.PreInstallScene(scene, builder => builder.AddSingleton(new GameplayWindow(_uiDocument)));
    }
}
