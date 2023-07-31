using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CamType { Merge,BlockRace,Shooting}
public class CameraManager : CustomSingleton<CameraManager>
{
    public CamType camType;

    [SerializeField] CinemachineVirtualCamera MergePhaseCam;
    [SerializeField] CinemachineVirtualCamera BlockRaceCam;
    [SerializeField] CinemachineVirtualCamera ShootingPhaseCam;

    [Header("Debug")]
    [SerializeField] CinemachineVirtualCamera activeCamera;

    private void Start()
    {
        LevelManager.instance.BlockRaceIsOnEvent += OnBlockRaceIsOn;
        LevelManager.instance.ShootingIsOnEvent += OnShootingIsOn;
    }
    private void OnShootingIsOn()
    {
       SetActiveCamera(CamType.Shooting);
    }
    private void OnBlockRaceIsOn()
    {
       SetActiveCamera(CamType.BlockRace);
    }
    public void SetActiveCamera(CamType type)
    {
        activeCamera.Priority = 10;

        switch (type)
        {
            case CamType.Merge:
                activeCamera = MergePhaseCam;
                break;
            case CamType.BlockRace:
                activeCamera = BlockRaceCam;
                break;
            case CamType.Shooting:
                activeCamera = ShootingPhaseCam;
                break;
            default:
                break;
        }

        activeCamera.Priority = 50;

    }

}

