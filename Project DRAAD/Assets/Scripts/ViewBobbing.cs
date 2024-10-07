using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewBobbing : MonoBehaviour
{
    [SerializeField] private Transform headCamTransform;
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CapsuleCollider capsuleCollider;

    [SerializeField] private float activeBobbingSpeed = 20f;
    [SerializeField] private float activeBobbingAmount = 10f;

    [SerializeField] private float idleBobbingSpeed = 5f;
    [SerializeField] private float idleBobbingAmount = 10f;

    [SerializeField] private bool underObject;
    [SerializeField] private bool standAsap;

    [SerializeField] private float stepTimerCurrent;
    [SerializeField] private float stepTimerMax;
    [SerializeField] private AudioClip stepClip;
    [SerializeField] private AudioSource stepSource;

    private float standingCollHeight = 4f;
    private Vector3 standingCollCenter = new Vector3(0, 0, 0);
    private float standingPosY = 3f;

    private float crouchingCollHeight = 1.5f;
    private Vector2 crouchingCollCenter = new Vector3(0, -1, 0);
    private float crouchingPosY = 1.5f;

    private float defaultPosY = 3f;
    private float timer = 0f;

    private void Start()
    {
        defaultPosY = headCamTransform.localPosition.y;
    }

    private void Update()
    {
        if (playerController.isGrounded == false)
        {
            timer = 0f;
            return;
        }

        underObject = Physics.Raycast(transform.position, Vector3.up, 2.1f);

        if (Input.GetKeyDown(KeyCode.LeftControl))
            SetHeight(crouchingCollHeight, crouchingCollCenter, crouchingPosY);
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            standAsap = true;

        if (standAsap && !underObject)
        {
            SetHeight(standingCollHeight, standingCollCenter, standingPosY);
            standAsap = false;
        }
        
        if (rbPlayer.velocity.magnitude <= 1f)
        {
            timer += Time.deltaTime * idleBobbingSpeed;
            headCamTransform.localPosition = new Vector2(transform.localPosition.x, defaultPosY - Mathf.Sin(timer) * idleBobbingAmount);
            stepTimerCurrent = 0f;

            return;
        }

        timer += Time.deltaTime * activeBobbingSpeed;
        headCamTransform.localPosition = new Vector2(transform.localPosition.x, defaultPosY - Mathf.Sin(timer) * activeBobbingAmount);

        if (stepTimerCurrent <= 0f)
        {
            stepSource.PlayOneShot(stepClip);
            stepSource.pitch = Random.Range(.9f,1.1f);
            stepTimerCurrent = stepTimerMax;
        }
        else
            stepTimerCurrent -= Time.deltaTime;

    }

    void SetHeight(float collHeight, Vector3 collCenter, float posY)
    {
        capsuleCollider.height = collHeight;
        capsuleCollider.center = collCenter;

        defaultPosY = posY;
        headCamTransform.localPosition = new Vector2(transform.localPosition.x, defaultPosY);
    }


}
