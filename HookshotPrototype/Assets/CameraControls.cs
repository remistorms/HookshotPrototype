using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_RotateSpeed;
    [SerializeField] private float m_PitchSpeed;

    private Vector3 m_StartingOffset;
    private float m_RightStickHorizontal;
    private float m_RightStickVertical;

    private GameObject m_CamParentObject;
    private GameObject m_CamPitchObject;
    private GameObject m_ThirdPersonPosition;

    private void Awake()
    {
        InitializeCamera();
    }

    public void InitializeCamera()
    {
        m_CamParentObject = new GameObject("Cam Parent");
        m_CamPitchObject = new GameObject("Cam Pitch");
        m_ThirdPersonPosition = new GameObject("Third Person Position");

        m_CamParentObject.transform.position = Vector3.zero;
        m_CamPitchObject.transform.position = Vector3.zero;
        m_ThirdPersonPosition.transform.position = transform.position;

        //Parents Objects
        transform.SetParent(m_CamPitchObject.transform);
        m_ThirdPersonPosition.transform.SetParent(m_CamPitchObject.transform);
        m_CamPitchObject.transform.SetParent(m_CamParentObject.transform);

    }

    public Transform GetCameraParent()
    {
        return m_CamParentObject.transform;
    }

    private void Update()
    {
        DetectInput();
        UpdateYaw();
        UpdatePitch();
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 lerpedPosition = Vector3.Lerp(m_CamParentObject.transform.position, target.position, Time.deltaTime * m_MoveSpeed);
        m_CamParentObject.transform.position = lerpedPosition;
    }

    private void DetectInput()
    {
        m_RightStickHorizontal = Input.GetAxis("R_Horizontal");
        m_RightStickVertical = Input.GetAxis("R_Vertical");

        if (Input.GetKeyDown(KeyCode.F))
        {
           // SwitchToFirstPerson();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
           // SwitchToThirdPerson();
        }

    }

    private void UpdateYaw()
    {
        m_CamParentObject.transform.Rotate(Vector3.up, m_RotateSpeed * Time.deltaTime * m_RightStickHorizontal, Space.Self);
    }

    private void UpdatePitch()
    {
        m_CamPitchObject.transform.Rotate(Vector3.right, m_PitchSpeed * Time.deltaTime * m_RightStickVertical, Space.Self);
    }
}

public enum CameraState
{
    None,
    ThirdPerson,
    FirstPerson,
    Cutscene
}
