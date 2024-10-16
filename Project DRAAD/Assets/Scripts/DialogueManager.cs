using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private TextMeshPro speakerNameTextmesh;
    [SerializeField] private TextMeshPro dialogueTextmesh;
    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private Animator container_animator;

    [Header("Current Dialogue Index")]
    [SerializeField] private int currentDialogueIndex;

    [Header("Dialogue")]
    [SerializeField] private Dialogue currentDialogue;
    [SerializeField] private Speaker currentSpeaker;

    [Header("Dialogues")]
    public Dialogue[] dialogues;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerConversant.conversant != null)
        {
            AdvanceDialogue();
        }

        if (playerConversant.conversant == null && currentDialogueIndex != 0)
            CloseDialogue();
    }

    public void AdvanceDialogue()
    {
        if (currentDialogueIndex >= currentDialogue.dialogueText.Length)
        {
            CloseDialogue();
            return;
        }

        dialogueTextmesh.text = currentDialogue.dialogueText[currentDialogueIndex];
        currentSpeaker = currentDialogue.speakers[currentDialogue.speakerIDs[currentDialogueIndex]];

        speakerNameTextmesh.text = currentSpeaker.speakerName;
        speakerNameTextmesh.font = currentSpeaker.speakerFont;
        dialogueTextmesh.color = currentSpeaker.speakerTextColor;

        currentDialogueIndex += 1;
        
    }

    public void CloseDialogue()
    {
        container_animator.Play("dialogue_close");
        currentDialogue = null;
        currentDialogueIndex = 0;
    }

    public void OpenDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue; currentDialogueIndex = 0;

        container_animator.Play("dialogue_open");

        AdvanceDialogue();
    }
}
