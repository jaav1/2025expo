using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Transform door; // Asigna aqu� la puerta en el Inspector
    public float openAngle = 90f; // �ngulo al que se abrir� la puerta
    public float speed = 2f; // Velocidad de apertura/cierre
    private bool isOpen = false; // Estado de la puerta
    private bool isInTrigger = false; // Verifica si el jugador est� cerca

    private Quaternion closedRotation; // Rotaci�n cerrada
    private Quaternion openRotation; // Rotaci�n abierta

    void Start()
    {
        // Guarda la rotaci�n inicial de la puerta
        closedRotation = door.localRotation;

        // Calcula la rotaci�n abierta
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
    }

    void Update()
    {
        // Si el jugador est� cerca y pulsa "E"
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen; // Alterna el estado de la puerta
        }

        // Movimiento suave de la puerta
        if (isOpen)
        {
            door.localRotation = Quaternion.Slerp(door.localRotation, openRotation, Time.deltaTime * speed);
        }
        else
        {
            door.localRotation = Quaternion.Slerp(door.localRotation, closedRotation, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador entra en el trigger
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador sale del trigger
        {
            isInTrigger = false;
        }
    }
}