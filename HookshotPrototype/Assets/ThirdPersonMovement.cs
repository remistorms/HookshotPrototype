using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 50f;
    [SerializeField] private float m_MovementSpeed;
    [SerializeField] private float m_RotateSpeed;
    private CharacterController m_CharacterController;
    private CameraControls m_CameraControls;

    private Transform camTransform;
    private Animator m_ProtoAnimator;
    //private Rigidbody m_ProtoRigidbody;
    private Vector3 m_MoveDirection = Vector3.zero;

    private void Awake()
    {
        m_ProtoAnimator = GetComponentInChildren<Animator>();
        m_CharacterController = GetComponent<CharacterController>();
        //m_ProtoRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_CameraControls = FindObjectOfType<CameraControls>();
        camTransform = m_CameraControls.transform;
    }

    private void Update()
    {
        DetectInput();
        UpdateAnimator();

        if (m_CameraControls.m_CurrentCamState == CameraState.ThirdPerson)
        {
            MoveProto();
            RotateProto();
        }

    }

    private void LateUpdate()
    {
        RotateProto();
    }

    //Detects input from Horizontal and Vertical Axis and stores it as a local variable
    private void DetectInput()
    {
        m_MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));

        //Make sure its based on camera
        m_MoveDirection = camTransform.TransformDirection( m_MoveDirection );

        m_MoveDirection = m_MoveDirection.normalized;

        //Normalizes move direction
        if (m_MoveDirection.magnitude <= 0.2f)
            m_MoveDirection = Vector3.zero;

        Debug.DrawLine( transform.position, transform.position + m_MoveDirection, Color.blue );
    }

    //Use this to update animator based on input
    private void UpdateAnimator()
    {
        m_ProtoAnimator.SetFloat("MoveSpeed", m_MoveDirection.magnitude);
    }

    private void MoveProto()
    {
        m_CharacterController.SimpleMove(m_MoveDirection * m_MovementSpeed );
    }

    private void RotateProto()
    {
        if (m_MoveDirection.magnitude >= 0.2f)
            transform.forward = m_MoveDirection;
    }

}
