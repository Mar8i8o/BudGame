using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OjoController : MonoBehaviour
{
    // L�mites en los ejes horizontal y vertical
    public float leftLimit = -100f;
    public float rightLimit = 100f;
    public float upperLimit = 100f;
    public float lowerLimit = -100f;

    // Velocidad de movimiento
    public float moveSpeedX = 5f;
    public float moveSpeedY = 5f;

    // Posici�n inicial del objeto en el canvas
    private Vector3 initialPosition;

    void Start()
    {
        // Guardar la posici�n inicial del objeto en el canvas
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Obtener la posici�n del rat�n en el canvas
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Asegurar que est� en el plano de la c�mara

        // Convertir la posici�n del rat�n a coordenadas del canvas
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), mousePosition, null, out localPoint);

        // Limitar la posici�n en los ejes x e y dentro de los l�mites establecidos respecto a la posici�n inicial
        float targetX = Mathf.Clamp(localPoint.x, initialPosition.x + leftLimit, initialPosition.x + rightLimit);
        float targetY = Mathf.Clamp(localPoint.y, initialPosition.y + lowerLimit, initialPosition.y + upperLimit);

        // Interpolar suavemente entre la posici�n actual y la nueva posici�n
        float newX = Mathf.Lerp(transform.localPosition.x, targetX, Time.deltaTime * moveSpeedX);
        float newY = Mathf.Lerp(transform.localPosition.y, targetY, Time.deltaTime * moveSpeedY);

        // Asignar la nueva posici�n al objeto en el canvas
        transform.localPosition = new Vector3(newX, newY, initialPosition.z);
    }

}

