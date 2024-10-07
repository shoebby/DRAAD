using UnityEngine;
using TMPro;

public class PlayerConversant : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactibleMask = 1 << 6;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private GameObject PromptUI;
    [SerializeField] private TextMeshProUGUI promptText;

    private readonly Collider[] colliders = new Collider[3]; //can be raised if there are more interactibles in a single scene
    [SerializeField] private int numFound;

    private IConversant conversant;
    private IInteractable interactable;

    private void Start()
    {
        PromptUI.SetActive(false);
    }

    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactibleMask);

        if (numFound > 0)
        {
            conversant = colliders[0].GetComponent<IConversant>();

            if (conversant != null)
            {
                if (!PromptUI.activeSelf)
                {
                    promptText.text = "[E] Speak";
                    PromptUI.SetActive(true);
                } 

                if (Input.GetKeyDown(interactKey))
                    conversant.Converse(this);
            }

            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!PromptUI.activeSelf)
                {
                    promptText.text = "[E] Interact";
                    PromptUI.SetActive(true);
                }

                if (Input.GetKeyDown(interactKey))
                    interactable.Interact(this);
            }
        }
        else
        {
            if (conversant != null)
                conversant = null;

            if (interactable != null)
                interactable = null;

            if (PromptUI.activeSelf)
                PromptUI.SetActive(false);
        }
    }
}
