using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text npcDialogueText; // Texto del NPC
    public Button[] responseButtons; // Botones de respuesta

    private DialogueLine currentLine;

    public void StartDialogue(DialogueLine line)
    {
        currentLine = line;

        // **Mostrar el texto del NPC**
        npcDialogueText.text = line.npcText;

        // **Configurar las opciones de respuesta**
        for (int i = 0; i < responseButtons.Length; i++)
        {
            if (i < line.playerResponses.Length)
            {
                responseButtons[i].gameObject.SetActive(true);
                responseButtons[i].GetComponentInChildren<TMP_Text>().text = line.playerResponses[i];
                int index = i;
                responseButtons[i].onClick.RemoveAllListeners();
                responseButtons[i].onClick.AddListener(() => OnResponseSelected(index));
            }
            else
            {
                responseButtons[i].gameObject.SetActive(false);
            }
        }

        // **Desbloquear y mostrar el cursor cuando inicia el diálogo**
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnResponseSelected(int index)
    {
        Debug.Log("Respuesta seleccionada: " + currentLine.playerResponses[index]);
        EndDialogue();
    }

    private void EndDialogue()
    {
        // **Limpiar el diálogo y desactivar los botones**
        npcDialogueText.text = "";
        foreach (Button btn in responseButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // **Bloquear y ocultar el cursor cuando finaliza el diálogo**
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
