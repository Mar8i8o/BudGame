using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    public GameObject notificationPrefab;
    public GameObject notificationMaxPrefab;
    public GameObject notificationPoint;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void MostrarNotificacion(float cantidad, Sprite image, bool negativo)
    {
        GameObject instancia = Instantiate(notificationPrefab, notificationPoint.transform.position, notificationPoint.transform.rotation);
        instancia.transform.SetParent(notificationPoint.transform, true);

        NotificationManager notificationManager = instancia.GetComponent<NotificationManager>();

        notificationManager.cantidad = cantidad;
        notificationManager.imagenActual.sprite = image;
        notificationManager.negativo = negativo;

    }

    public void MostrarNotificacionMax(Sprite image)
    {
        GameObject instancia = Instantiate(notificationMaxPrefab, notificationPoint.transform.position, notificationPoint.transform.rotation);
        instancia.transform.SetParent(notificationPoint.transform, true);

        NotificationManager notificationManager = instancia.GetComponent<NotificationManager>();

        //notificationManager.cantidad = cantidad;
        notificationManager.imagenActual.sprite = image;
        notificationManager.negativo = true;
        //notificationManager.negativo = negativo;

    }

}
