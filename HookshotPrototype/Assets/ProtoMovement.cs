using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoMovement : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed;
    [SerializeField] private float m_RotateSpeed;
    private Transform camTransform;

    private Animator m_ProtoAnimator;
    private Rigidbody m_ProtoRigidbody;
    [SerializeField] private Vector3 m_MoveDirection = Vector3.zero;

    private void Awake()
    {
        m_ProtoAnimator = GetComponentInChildren<Animator>();
        m_ProtoRigidbody = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        DetectInput();
        UpdateAnimator();
    }

    private void LateUpdate()
    {
        RotateProto();
    }

    private void FixedUpdate()
    {
        MoveProto();
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
        m_ProtoRigidbody.MovePosition(transform.position + m_MoveDirection * m_MovementSpeed * Time.deltaTime);
    }

    private void RotateProto()
    {
        if (m_MoveDirection.magnitude >= 0.2f)
            transform.forward = m_MoveDirection;
    }
}
