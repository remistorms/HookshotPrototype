using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] private float m_StrafeSpeed;
    [SerializeField] private float m_RotateSpeed;

    private CharacterController m_CharacterController;
    private Vector3 m_MoveDirection = Vector3.zero;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        DetectInput();
        MoveProto();
        RotateProto();
    }

    //Detects input from Horizontal and Vertical Axis and stores it as a local variable
    private void DetectInput()
    {
        m_MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Make sure its based on camera
        m_MoveDirection = Camera.main.transform.TransformDirection(m_MoveDirection);

        m_MoveDirection = m_MoveDirection.normalized;

        //Normalizes move direction
        if (m_MoveDirection.magnitude <= 0.2f)
            m_MoveDirection = Vector3.zero;

    }

    //Use this to update animator based on input
    private void UpdateAnimator()
    {
       // m_ProtoAnimator.SetFloat("MoveSpeed", m_MoveDirection.magnitude);
    }

    private void MoveProto()
    {
        m_CharacterController.SimpleMove(m_MoveDirection * m_StrafeSpeed);
    }

    private void RotateProto()
    {
        transform.Rotate( 0, Input.GetAxis("R_Horizontal") * Time.deltaTime * m_RotateSpeed, 0 );
    }
}

