using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Instancia única para acceso global
    public List<string> inventory = new List<string>(); // Lista para almacenar los objetos
    [SerializeField] private Text inventoryText; // Texto de la UI asignado desde el Inspector

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        UpdateInventoryUI(); // Actualiza la UI cada vez que se añade un objeto
        Debug.Log($"Objeto añadido: {itemName}");
    }

    public void UpdateInventoryUI()
    {
        if (inventoryText != null)
        {
            inventoryText.text = "Inventario:\n";
            foreach (string item in inventory)
            {
                inventoryText.text += $"- {item}\n";
            }
        }
        else
        {
            Debug.LogWarning("Texto de inventario no asignado en el Inspector.");
        }
    }
}