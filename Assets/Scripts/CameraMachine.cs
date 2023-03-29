using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMachine : MonoBehaviour
{
    [SerializeField] private Stickman stickman;
    [SerializeField] private Tower towen;
    
    public  CinemachineVirtualCamera virtualCamera;
    public static CameraMachine instance;
    private void Start()
    {
        instance = this;
    }
    public void ShowStickman()
    {
        if (stickman == null) return;
        virtualCamera.LookAt= stickman.transform;
        virtualCamera.Follow = stickman.transform;
    }
    public void ShowTower()
    {
        if (towen == null) return;
        virtualCamera.LookAt = towen.transform;
        virtualCamera.Follow = towen.transform;
    }
}
