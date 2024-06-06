using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _infoText;

    public void SetText(string text)
    {
        _infoText.text = text;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform, Vector3.up);
    }
}
