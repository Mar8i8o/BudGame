 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextoExplicacion : MonoBehaviour
{
    public string textoInfo;

    public TextMeshProUGUI txt;
    public GameObject informacionGO;

    void Start()
    {
        informacionGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MostrarInfo()
    {
        informacionGO.SetActive(true);
        txt.text = textoInfo;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OcultarInfo()
    {
        informacionGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
