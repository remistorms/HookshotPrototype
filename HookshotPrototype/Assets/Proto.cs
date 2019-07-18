using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto : MonoBehaviour
{
    public CameraState m_CurrentViewMode;
    private CameraControls m_CameraControls;
    private FirstPersonMovement m_FirstPersonMovement;
    private ThirdPersonMovement m_ThirdPersonMovement;
    private Hookshoot m_Hookshot;

    private void Awake()
    {
        m_FirstPersonMovement = GetComponent<FirstPersonMovement>();
        m_ThirdPersonMovement = GetComponent<ThirdPersonMovement>();
        m_Hookshot = GetComponent<Hookshoot>();
    }

    private void Start()
    {
        m_Hookshot.enabled = false;
        m_CameraControls = FindObjectOfType<CameraControls>();
        m_CameraControls.Evt_CamStateChanged += OnCamStateChanged;
    }

    private void OnCamStateChanged( CameraState _newState )
    {
        m_CurrentViewMode = _newState;

        switch (_newState)
        {
            case CameraState.None:
                break;
            case CameraState.ThirdPerson:
                m_FirstPersonMovement.enabled = false;
                m_ThirdPersonMovement.enabled = true;
                m_Hookshot.enabled = false;
                break;
            case CameraState.FirstPerson:
                m_FirstPersonMovement.enabled = true;
                m_ThirdPersonMovement.enabled = false;
                m_Hookshot.enabled = true;
                break;
            case CameraState.Cutscene:
                break;
            default:
                break;
        }
    }

    public void EnableFirstPersonMovement()
    {
        m_FirstPersonMovement.enabled = true;
    }

    public void DisableFirstPersonMovement()
    {
        m_FirstPersonMovement.enabled = false;
    }

    public void EnableThirdPersonMovement()
    {
        m_ThirdPersonMovement.enabled = true;
    }

    public void DisableThirdPersonMovement()
    {
        m_ThirdPersonMovement.enabled = false;
    }
}

