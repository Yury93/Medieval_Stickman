using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMachine : MonoBehaviour
{
   private Tower towen;
    private Stickman stickman;
    public  CinemachineVirtualCamera virtualCamera;
 
    public void Init()
    {
        stickman = CoreEnivroment.Instance.activeStickman;
        towen = CoreEnivroment.Instance.tower;
        ShowStickman();
        stickman.OnDeathStickman += StickmanDeath;
    }

    private void StickmanDeath()
    {
        StartCoroutine(CorGameOver());
      IEnumerator CorGameOver()
        {
            yield return new WaitForSeconds(2f);
            ShowTower();
            yield return new WaitForSeconds(1f);
            Time.timeScale = 5f;
        }
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
