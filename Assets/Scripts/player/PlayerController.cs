using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using ColonyBuilder.UI.game;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] GameObject nickNameGameObject;
    [SerializeField] TMP_Text nickNameText;

    float verticalLookRotation;
    bool isGrounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    bool isEsc = false;

    Rigidbody rigidbody;

    PhotonView photonView;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        nickNameText.text = photonView.Owner.NickName;
        if (!photonView.IsMine)
        {            
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rigidbody);                        
        }
        else if (photonView.IsMine){
            nickNameGameObject.SetActive(false);
            nickNameText.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        if (!isEsc)
            {
                GuiMenager.Instance.escPanelSet(true);
                isEsc = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        else
            {
                GuiMenager.Instance.escPanelSet(false);
                isEsc = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (!isEsc) {
            Look();
            Move();
            Jump();
        }
              
    }
    void Look() {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
    void Move() {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }
    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody.AddForce(transform.up * jumpForce);
        }
    }
    public void setGroundedState(bool _isGrounded) {
        isGrounded = _isGrounded;
    }
    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
