using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raton : MonoBehaviour
{
    public RectTransform canvasRectTransform; // Referencia al RectTransform del Canvas
    public RectTransform pointRectTransform; // Referencia al RectTransform del objeto que seguirá al ratón

    void Update()
    {
        // Obtener la posición del ratón en la pantalla
        Vector2 posicionRaton = Input.mousePosition;

        // Convertir la posición del ratón en la pantalla a una posición en el RectTransform del Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, posicionRaton, null, out Vector2 posicionEnCanvas);

        // Asignar la posición en el RectTransform del Canvas al objeto que seguirá al ratón
        pointRectTransform.localPosition = posicionEnCanvas;
    }
}
