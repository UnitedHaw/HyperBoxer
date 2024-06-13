using Reflex.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton("World");
    }
}
