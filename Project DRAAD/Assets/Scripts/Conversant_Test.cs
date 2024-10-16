using TMPro;
using UnityEngine;

public class Conversant_Test : MonoBehaviour, IConversant
{
    [SerializeField] private string thisName;

    [SerializeField] private Color thisColor;

    [SerializeField] private TMP_FontAsset thisFont;

    [SerializeField] private Dialogue thisDialogue;

    [SerializeField] private DialogueManager dialogueManager;

    [HideInInspector] public string conversantName => thisName;
    [HideInInspector] public Color conversantTextColor => thisColor;
    [HideInInspector] public TMP_FontAsset conversantFont => thisFont;
    [HideInInspector] public Dialogue conversantDialogue => thisDialogue;

    public bool Converse(PlayerConversant playerConversant)
    {
        if (InventoryManager.Instance.heldItem != null)
        {
            if (InventoryManager.Instance.heldItem.name == "key_house")
                dialogueManager.OpenDialogue(dialogueManager.dialogues[0]);

            return true;
        }

        dialogueManager.OpenDialogue(thisDialogue);
        return true;
    }
}