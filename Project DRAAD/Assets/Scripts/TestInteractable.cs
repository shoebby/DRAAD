using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject lightsContainer;

    public bool Interact(PlayerConversant playerConversant)
    {
        lightsContainer.SetActive(!lightsContainer.activeSelf);

        return true;
    }
}
