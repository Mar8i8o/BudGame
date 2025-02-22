using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raton : MonoBehaviour
{
    public RectTransform canvasRectTransform; // Referencia al RectTransform del Canvas
    public RectTransform pointRectTransform; // Referencia al RectTransform del objeto que seguir� al rat�n

    void Update()
    {
        // Obtener la posici�n del rat�n en la pantalla
        Vector2 posicionRaton = Input.mousePosition;

        // Convertir la posici�n del rat�n en la pantalla a una posici�n en el RectTransform del Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, posicionRaton, null, out Vector2 posicionEnCanvas);

        // Asignar la posici�n en el RectTransform del Canvas al objeto que seguir� al rat�n
        pointRectTransform.localPosition = posicionEnCanvas;
    }
}
