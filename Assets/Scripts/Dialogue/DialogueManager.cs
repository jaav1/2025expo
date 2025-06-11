using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public Button[] optionButtons;

    // Referencias a scripts para bloquear/desbloquear cámara y movimiento jugador
    public MonoBehaviour cameraControlScript;
    public MonoBehaviour playerMovementScript;

    private Dialogue currentDialogue;
    private float confianzaNPC = 0f;
    private float sospechaMercado = 0f;

    private bool isDialogueActive = false; // Controla si el diálogo está activo

    void Awake()
    {
        // Forzar que el panel esté desactivado desde el principio
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Permite cerrar el diálogo con Escape si está activo
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Escape))
        {
            EndDialogue();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogError("❌ Error: El diálogo recibido es NULL.");
            return;
        }

        if (speakerText == null || dialogueText == null)
        {
            Debug.LogError("❌ Error: El UI no está asignado correctamente en DialogueManager.");
            return;
        }

        currentDialogue = dialogue;
        isDialogueActive = true;

        // Bloquear control de cámara y movimiento jugador
        if (cameraControlScript != null) cameraControlScript.enabled = false;
        if (playerMovementScript != null) playerMovementScript.enabled = false;

        // Mostrar panel de diálogo
        dialoguePanel.SetActive(true);

        // Mostrar cursor para poder usar botones
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        speakerText.text = currentDialogue.speakerName;
        dialogueText.text = currentDialogue.dialogueText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < currentDialogue.options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                int index = i;
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.options[i].optionText;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectOption(index));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SelectOption(int index)
    {
        DialogueOption selected = currentDialogue.options[index];
        confianzaNPC += selected.confianzaDelta;
        sospechaMercado += selected.sospechaDelta;

        if (selected.nextDialogue != null)
        {
            StartDialogue(selected.nextDialogue);
        }
        else
        {
            EndDialogue(); // termina diálogo si no hay más
        }
    }

    public void EndDialogue()
    {
        CloseDialogue(); // Llama a Close para cerrar correctamente
    }

    public void CloseDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        speakerText.text = "";
        dialogueText.text = "";
        currentDialogue = null;

        // Reactivar control de cámara y movimiento jugador
        if (cameraControlScript != null) cameraControlScript.enabled = true;
        if (playerMovementScript != null) playerMovementScript.enabled = true;

        // Ocultar cursor y bloquearlo para juego normal
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        foreach (Button btn in optionButtons)
        {
            btn.gameObject.SetActive(false);
            btn.onClick.RemoveAllListeners();
        }
    }
}
