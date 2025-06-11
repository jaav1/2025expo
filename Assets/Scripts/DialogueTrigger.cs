using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entró al trigger: " + other.name); // AÑADE ESTO

        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado. Iniciando diálogo..."); // Y ESTO TAMBIÉN

            DialogueLine line = new DialogueLine
            {
                npcText = "¿Qué haces aquí?",
                playerResponses = new string[]
                {
                "Estoy buscando a alguien.",
                "Solo pasaba por aquí.",
                "Eso no es asunto tuyo."
                }
            };

            dialogueManager.StartDialogue(line);
        }
    }

}
