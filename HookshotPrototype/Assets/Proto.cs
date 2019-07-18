using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto : MonoBehaviour
{
    public CameraState m_CurrentViewMode;
    private CameraControls m_CameraControls;
    private FirstPersonMovement m_FirstPersonMovement;
    private ThirdPersonMovement m_ThirdPersonMovement;

    private void Awake()
    {
        m_FirstPersonMovement = GetComponent<FirstPersonMovement>();
        m_ThirdPersonMovement = GetComponent<ThirdPersonMovement>();
    }

    private void Start()
    {
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
                break;
            case CameraState.FirstPerson:
                m_FirstPersonMovement.enabled = true;
                m_ThirdPersonMovement.enabled = false;
                break;
            case CameraState.Cutscene:
                break;
            default:
                break;
        }
    }
}

