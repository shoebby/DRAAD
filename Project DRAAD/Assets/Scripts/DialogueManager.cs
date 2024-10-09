using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private PlayerConversant playerConversant;
    [SerializeField] private TextMeshPro speakerNameTextmesh;
    [SerializeField] private TextMeshPro dialogueTextmesh;
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdvanceDialogue();
        }

        if (playerConversant.conversant == null)
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
        dialogueTextmesh.color = currentSpeaker.speakerTextColor;

        currentDialogueIndex += 1;
        
    }

    public void CloseDialogue()
    {
        //playerController.TogglePlayerMovement();
        //playerLook.ToggleLook();

        dialogueContainer.SetActive(false);

        currentDialogue = null; currentDialogueIndex = 0;
    }

    public void OpenDialogue(Dialogue dialogue)
    {
        //playerController.TogglePlayerMovement();
        //playerLook.ToggleLook();

        currentDialogue = dialogue; currentDialogueIndex = 0;

        dialogueContainer.SetActive(true);

        AdvanceDialogue();
    }
}
