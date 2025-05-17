using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entr� al trigger: " + other.name); // A�ADE ESTO

        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado. Iniciando di�logo..."); // Y ESTO TAMBI�N

            DialogueLine line = new DialogueLine
            {
                npcText = "�Qu� haces aqu�?",
                playerResponses = new string[]
                {
                "Estoy buscando a alguien.",
                "Solo pasaba por aqu�.",
                "Eso no es asunto tuyo."
                }
            };

            dialogueManager.StartDialogue(line);
        }
    }

}
