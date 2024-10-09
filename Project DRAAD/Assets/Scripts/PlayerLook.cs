using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] public float sensX;
    [SerializeField] public float sensY;

    private Camera cam;

    private float mouseX;
    private float mouseY;

    private float xRotation;
    private float yRotation;

    private float clampMin = -85f;
    private float clampMax = 85f;

    private bool inDialogue;

    private void Awake()
    {
        inDialogue = false;
    }

    private void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (inDialogue) return;
        
        CamInput();

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void CamInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * Time.deltaTime;
        xRotation -= mouseY * sensY * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, clampMin, clampMax);
    }

    public void ToggleLook()
    {
        inDialogue = !inDialogue;

        Cursor.visible = !Cursor.visible;

        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
    }
}
