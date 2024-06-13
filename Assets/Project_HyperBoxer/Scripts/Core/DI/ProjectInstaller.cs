using Assets.Project_HyperBoxer.Scripts.Test;
using Assets.Project_HyperBoxer.Scripts.UI;
using Reflex.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private UIDocument _uiDocument;
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        var container = Instantiate(new GameObject("ProjectInstallers"));
        DontDestroyOnLoad(container);

        var document = Instantiate(_uiDocument, container.transform);

        containerBuilder.AddSingleton(document);
    }
}
