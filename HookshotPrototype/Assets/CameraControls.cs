using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControls : MonoBehaviour
{
    public CameraState m_CurrentCamState;

    public event Action<CameraState> Evt_CamStateChanged = delegate { };

    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private Transform m_TargetToFollow;
    [SerializeField] private Transform m_FirstPersonCameraTransform;
    [SerializeField] private Transform m_ThirdPersonCameraTransform;

    [SerializeField] private float m_FollowSpeed;
    [SerializeField] private float m_RotateSpeed;
    [SerializeField] private float m_PitchSpeed;

    private Vector3 m_StartingOffset;
    private float m_RightStickHorizontal;
    private float m_RightStickVertical;

    [SerializeField] private GameObject m_CamParentObject;
    [SerializeField] private GameObject m_CamPitchObject;

    private void Awake()
    {
        m_MainCamera = Camera.main;
    }

    private void Start()
    {
        SwitchToThirdPerson();
    }

    public void SetCameraTarget(Transform _target)
    {
        m_TargetToFollow = _target;
    }

    public Transform GetCameraParent()
    {
        return m_CamParentObject.transform;
    }

    private void Update()
    {
        DetectInput();
        if (m_CurrentCamState == CameraState.ThirdPerson)
        {
            UpdateYaw();
            UpdatePitch();
        }
        if (m_CurrentCamState == CameraState.FirstPerson)
        {
            LookAround();
        }
    

    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 lerpedPosition = Vector3.Lerp(m_CamParentObject.transform.position, m_TargetToFollow.position, Time.deltaTime * m_FollowSpeed);
        m_CamParentObject.transform.position = lerpedPosition;
    }

    private void DetectInput()
    {
        m_RightStickHorizontal = Input.GetAxis("R_Horizontal");
        m_RightStickVertical = Input.GetAxis("R_Vertical");

        if (Input.GetKeyDown(KeyCode.F))
        {
           SwitchToFirstPerson();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
           SwitchToThirdPerson();
        }

    }

    private void SwitchToFirstPerson()
    {
        m_CurrentCamState = CameraState.FirstPerson;
        m_MainCamera.transform.SetParent(m_FirstPersonCameraTransform);
        m_MainCamera.transform.localPosition = Vector3.zero;
        m_MainCamera.transform.localRotation = Quaternion.identity;
        Evt_CamStateChanged( m_CurrentCamState );
    }

    private void SwitchToThirdPerson()
    {
        m_CurrentCamState = CameraState.ThirdPerson;

        m_MainCamera.transform.SetParent(m_ThirdPersonCameraTransform);
        m_MainCamera.transform.localPosition = Vector3.zero;
        m_MainCamera.transform.localRotation = Quaternion.identity;
        Evt_CamStateChanged(m_CurrentCamState);
    }


    private void LookAround()
    {
        transform.Rotate(Vector3.up, m_RotateSpeed * Time.deltaTime * Input.GetAxis("R_Horizontal"), Space.Self);
        m_MainCamera.transform.Rotate(Vector3.right, m_RotateSpeed * Time.deltaTime * Input.GetAxis("R_Vertical"), Space.Self);
    }

    private void UpdateYaw()
    {
        m_CamParentObject.transform.Rotate(Vector3.up, m_RotateSpeed * Time.deltaTime * m_RightStickHorizontal, Space.Self);
    }

    private void UpdatePitch()
    {
        m_CamPitchObject.transform.Rotate(Vector3.right, -m_PitchSpeed * Time.deltaTime * m_RightStickVertical, Space.Self);
    }
}

public enum CameraState
{
    None,
    ThirdPerson,
    FirstPerson,
    Cutscene
}
