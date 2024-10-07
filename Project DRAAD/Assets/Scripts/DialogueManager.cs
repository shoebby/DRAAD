using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private TextMeshProUGUI speakerNameTextmesh;
    [SerializeField] private TextMeshProUGUI dialogueTextmesh;
    [SerializeField] private Image speakerPortrait;
    [SerializeField] private GameObject dialogueContainer;

    [Header("Current Dialogue Index")]
    [SerializeField] private int currentDialogueIndex;

    [Header("Dialogue")]
    [SerializeField] private Dialogue currentDialogue;
    [SerializeField] private Speaker currentSpeaker;

    void Start()
    {
        dialogueContainer.SetActive(false);
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
        speakerPortrait.sprite = currentSpeaker.speakerPortrait;

        currentDialogueIndex += 1;
        
    }

    public void CloseDialogue()
    {
        playerController.TogglePlayerMovement();
        playerLook.ToggleLook();

        dialogueContainer.SetActive(false);

        currentDialogue = null; currentDialogueIndex = 0;
    }

    public void OpenDialogue(Dialogue dialogue)
    {
        playerController.TogglePlayerMovement();
        playerLook.ToggleLook();

        currentDialogue = dialogue; currentDialogueIndex = 0;

        dialogueContainer.SetActive(true);

        AdvanceDialogue();
    }
}
