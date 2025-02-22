using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamaraControll : MonoBehaviour
{

    TimeController timeController;
    public GameObject cameraLookPoint;
    public float distanciaZ;

    public Vector3 sensibility;

    public float limit;

    public GameObject pos1;
    public GameObject pos2;

    Vector3 posicionInicial;

    public bool freezeCam;

    private void Start()
    {
       posicionInicial = transform.position;
       timeController = GameObject.Find("GameManager").GetComponent<TimeController>();
    }
    private void Update()
    {
        if (!freezeCam)
        {
            // Obtener la posición del ratón en la pantalla
            Vector3 posicionRaton = Input.mousePosition;

            // Convertir la posición del ratón en la pantalla a una posición en el mundo
            Vector3 posicionEnMundo = Camera.main.ScreenToWorldPoint(new Vector3(posicionRaton.x, posicionRaton.y, distanciaZ)); // La distancia Z determina la profundidad en la que quieres que el objeto siga el ratón

            // Actualizar la posición del objeto para que coincida con la posición del ratón en el mundo
            cameraLookPoint.transform.position = posicionEnMundo;

            MovimientoCam();
        }
        
    }

    public void MovimientoCam()
    {
        float ejeX = Input.GetAxis("Mouse X");
        float ejeY = Input.GetAxis("Mouse Y");

        if (ejeX != 0 || ejeY != 0)
        {
            float angleX = (transform.localEulerAngles.x - ejeY * sensibility.y + 360) % 360;
            float angleY = (transform.localEulerAngles.y - ejeX * (-sensibility.x) + 360) % 360;

            if (angleX > 180) { angleX -= 360; }
            if (angleY > 180) { angleY -= 360; }

            angleX = Mathf.Clamp(angleX, -limit, limit);
            angleY = Mathf.Clamp(angleY, -limit, limit);

            transform.localEulerAngles = new Vector3(angleX, angleY, 0f);
        }

        Vector3 offsetUp = new Vector3(0,ejeY,0);

        if (timeController.tiempoAcelerado) { speed = 0.1f; }
        else { speed = 0.3f; }


        if (ejeX > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos1.transform.position , Time.deltaTime * speed);
        }
        else if (ejeX < -0.1) 
        {
            transform.position = Vector3.MoveTowards(transform.position, pos2.transform.position , Time.deltaTime * speed);
        }
        
        if (transform.localPosition.y < 0.05 && transform.localPosition.y > -0.05)
        {
            if (ejeY > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + offsetUp, Time.deltaTime * speed);
            }
            if (ejeY < -0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + offsetUp, Time.deltaTime * speed);
            }
        }



    }

    public float speed;

    public void LookAt(Transform target)
    {
        /*

        // Calculamos la dirección hacia el objetivo
        Vector3 direction = target.position - transform.position;
        // Calculamos la rotación necesaria para mirar hacia el objetivo
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // Suavizamos la rotación actual hacia la rotación del objetivo
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);

        */
    }
}


