using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TijerasController : MonoBehaviour
{
    public GameObject tijerasUI;
    public GameObject tijerasPoint;

    public GameObject tijeraAbierta;
    public GameObject tijeraCerrada;

    public bool usandoTijeras;

    public bool hojaTrigger;

    public NotificationController notificationController;

    void Start()
    {
        SoltarTijeras();
    }

    void Update()
    {
        if (usandoTijeras)
        {
            if (!hojaTrigger)
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    SoltarTijeras();
                }
            }
        }
    }

    public void CojerTijeras()
    {
        tijerasPoint.SetActive(true);
        tijerasUI.SetActive(false);
        usandoTijeras = true;
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    public void AbrirTijeras()
    {
        tijeraAbierta.SetActive(true);
        tijeraCerrada.SetActive(false);
    }

    public void CerrarTijeras()
    {
        tijeraAbierta.SetActive(false);
        tijeraCerrada.SetActive(true);
    }

    public void SoltarTijeras()
    {
        tijerasPoint.SetActive(false);
        tijerasUI.SetActive(true);
        usandoTijeras = false;

        CerrarTijeras();

        EventSystem.current.SetSelectedGameObject(null);
    }
}
