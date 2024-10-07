using UnityEngine;

public class Conversant_Test : MonoBehaviour, IConversant
{
    [SerializeField] private string thisName;

    [SerializeField] private Sprite thisSprite;

    [SerializeField] private Dialogue thisDialogue;

    [SerializeField] private DialogueManager dialogueManager;

    [HideInInspector] public string conversantName => thisName;
    [HideInInspector] public Sprite conversantSprite => thisSprite;
    [HideInInspector] public Dialogue conversantDialogue => thisDialogue;

    public bool Converse(PlayerConversant playerConversant)
    {
        dialogueManager.OpenDialogue(thisDialogue);
        return true;
    }
}
