using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshoot : MonoBehaviour
{
    private Transform camTransform;
    private Proto m_Proto;
    [SerializeField] private float m_HookshotDistance = 30.0f;
    [SerializeField] private float m_HookshotTime = 1.0f;
    private Ray ray;
    private RaycastHit hit;

    private WaitForSeconds m_HookshotDelay;

    private void Awake()
    {
        m_HookshotDelay = new WaitForSeconds( m_HookshotTime );
    }

    private void Start()
    {
        camTransform = Camera.main.transform;
        m_Proto = GetComponent<Proto>();
    }

    private void Update()
    {

        ray = new Ray(camTransform.position, camTransform.forward);

        if (Physics.Raycast( ray, out hit, m_HookshotDistance ))
        {
            Debug.Log( hit.collider.name + " is on sight " );
        }

        if (Input.GetButtonDown("Jump") && m_Proto.m_CurrentViewMode == CameraState.FirstPerson)
        {
            Debug.Log("Pressed Button");
        }

        if (Input.GetButton("Jump") && m_Proto.m_CurrentViewMode == CameraState.FirstPerson)
        {
            Debug.Log("Hold Down Button");
        }

        if (Input.GetButtonUp("Jump") && m_Proto.m_CurrentViewMode == CameraState.FirstPerson )
        {
            Debug.Log("Released Button");
            StartCoroutine(HookshotRoutine());
        }
    }

    IEnumerator HookshotRoutine()
    {
        m_Proto.DisableFirstPersonMovement();
        yield return m_HookshotDelay;
        m_Proto.EnableFirstPersonMovement();
    }

}
