using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerConversant : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private Transform conversingZone;
    [SerializeField] private float interactionPointRadius = 1f;
    [SerializeField] private float conversingPointRadius = 3.5f;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private GameObject lookUI;

    [Header("Snapshot Components")]
    [SerializeField] private SpriteRenderer snapshot_spriteRenderer;
    [SerializeField] private TextMeshPro snapshot_textMeshPro;
    [SerializeField] private Animator snapshot_animator;


    private readonly Collider[] colliders = new Collider[3]; //can be raised if there are more interactibles in a single scene
    [SerializeField] private int numInteractablesFound;
    [SerializeField] private int numConversantsFound;

    public IConversant conversant;
    public IInteractable interactable;
    public IPointable pointable;

    private bool isConversing;

    [SerializeField] private LayerMask convMask;
    [SerializeField] private LayerMask intMask;
    [SerializeField] private LayerMask pointMask;

    private bool snapshotIsVisible;

    private void Start()
    {
        interactUI.SetActive(false);
        lookUI.SetActive(false);
    }

    private void Update()
    {
        numInteractablesFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, intMask);

        if (numInteractablesFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactUI.activeSelf)
                    interactUI.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                    interactable.Interact(this);
            }
        }
        else
        {
            if (interactable != null)
                interactable = null;

            if (interactUI.activeSelf)
                interactUI.SetActive(false);
        }

        numConversantsFound = Physics.OverlapSphereNonAlloc(conversingZone.position, conversingPointRadius, colliders, convMask);

        if (numConversantsFound > 0)
        {
            conversant = colliders[0].GetComponent<IConversant>();

            if (conversant != null && !isConversing)
            {
                isConversing = true;
                conversant.Converse(this);
            }
        }
        else
        {
            if (conversant != null)
                conversant = null;

            isConversing = false;
        }

        if (!snapshotIsVisible)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer == pointMask)
                    pointable = hit.transform.GetComponent<IPointable>();
                else
                {
                    if (pointable != null)
                        pointable = null;

                    if (lookUI.activeSelf)
                        lookUI.SetActive(false);
                }
            }

            if (pointable != null)
            {
                if (!lookUI.activeSelf)
                    lookUI.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    snapshotIsVisible = true;
                    ToggleSnapshot(true);
                }
            }
        } else if (snapshotIsVisible)
        {
            if (pointable != null)
                pointable = null;

            if (lookUI.activeSelf)
                lookUI.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
                snapshotIsVisible = false;
                ToggleSnapshot(false);
            }
        }
    }

    private void ToggleSnapshot(bool state)
    {
        if (state)
        {
            snapshot_spriteRenderer.sprite = pointable.snapshotSprite;
            snapshot_textMeshPro.text = pointable.snapshotText;

            snapshot_animator.Play("snapshot_open");
        }
        else if (!state)
        {
            snapshot_animator.Play("snapshot_close");
        }
    }
}
