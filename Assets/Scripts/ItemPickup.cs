using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Text interactionText; // Texto de interacci�n en la UI
    public string itemName = "Objeto"; // Nombre del objeto

    private void Start()
    {
        if (interactionText == null)
        {
            Debug.LogError("El texto de interacci�n no est� asignado en el Inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.text = $"Presiona E para recoger {itemName}";
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"Has recogido: {itemName}");
            InventoryManager.instance.AddItem(itemName); // A�adir el objeto al inventario
            Destroy(gameObject); // Elimina el objeto recogido
            interactionText.gameObject.SetActive(false); // Ocultar texto
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}