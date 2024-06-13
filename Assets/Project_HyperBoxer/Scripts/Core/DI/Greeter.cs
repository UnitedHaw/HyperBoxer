using Reflex.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greeter : MonoBehaviour
{
    [Inject] private readonly IEnumerable<string> _strings;

    private void Start()
    {
        Debug.Log(string.Join(", ", _strings));
    }
}
