using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(dialogue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.CloseDialogue(); // Cierra al alejarse
        }
    }
}
