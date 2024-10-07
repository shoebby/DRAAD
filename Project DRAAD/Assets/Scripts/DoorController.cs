using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] private string thisScene;
    [SerializeField] private Transform exitTransform;

    public bool Interact(PlayerConversant playerConversant)
    {
        StartCoroutine(LoadScene(thisScene));
        return true;
    }

    private IEnumerator LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        yield return null;
    }
}
