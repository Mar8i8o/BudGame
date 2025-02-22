using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OjoController : MonoBehaviour
{
    // Límites en los ejes horizontal y vertical
    public float leftLimit = -100f;
    public float rightLimit = 100f;
    public float upperLimit = 100f;
    public float lowerLimit = -100f;

    // Velocidad de movimiento
    public float moveSpeedX = 5f;
    public float moveSpeedY = 5f;

    // Posición inicial del objeto en el canvas
    private Vector3 initialPosition;

    void Start()
    {
        // Guardar la posición inicial del objeto en el canvas
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Obtener la posición del ratón en el canvas
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Asegurar que esté en el plano de la cámara

        // Convertir la posición del ratón a coordenadas del canvas
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), mousePosition, null, out localPoint);

        // Limitar la posición en los ejes x e y dentro de los límites establecidos respecto a la posición inicial
        float targetX = Mathf.Clamp(localPoint.x, initialPosition.x + leftLimit, initialPosition.x + rightLimit);
        float targetY = Mathf.Clamp(localPoint.y, initialPosition.y + lowerLimit, initialPosition.y + upperLimit);

        // Interpolar suavemente entre la posición actual y la nueva posición
        float newX = Mathf.Lerp(transform.localPosition.x, targetX, Time.deltaTime * moveSpeedX);
        float newY = Mathf.Lerp(transform.localPosition.y, targetY, Time.deltaTime * moveSpeedY);

        // Asignar la nueva posición al objeto en el canvas
        transform.localPosition = new Vector3(newX, newY, initialPosition.z);
    }

}

