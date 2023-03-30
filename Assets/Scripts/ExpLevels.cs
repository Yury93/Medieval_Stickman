using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpLevels : MonoBehaviour
{
    [Serializable]
    public class Levels
    {
        public int Level;
        public int startExp;
    }
    public List<Levels> levels;
    [SerializeField] private TextMeshProUGUI currentLevelText,futureLevelText; 
    [SerializeField] private Image expFillAmount;
    public int currentExp;

    private void Start()
    {
        EnemiesService.instance.SpawnSystem.AllEnemies.ForEach(e => e.OnEnemyDeath += RefreshInfoLevel);
        currentExp = StickmanUpgrader.GetExpStickman();
        RefreshInfoLevel(null);

        EnemiesService.instance.SpawnSystem.EnemySpawners.ForEach(s => s.OnEnemySpawn += RefreshInfoLevel);
    }

    private void RefreshInfoLevel(Enemy enemy)
    {
        if (enemy)
        currentExp += enemy.Exp;

        ExpLevels.Levels currentLevel = levels.LastOrDefault(l => l.startExp <= currentExp);
        ExpLevels.Levels futureLevel = levels.LastOrDefault(l => l.Level <= currentLevel.Level + 1);

        currentLevelText.text = currentLevel.Level.ToString(); 
        if(futureLevel!= null)
        {
            futureLevelText.text = futureLevel.Level.ToString();
            if (currentExp != 0)
            {
                expFillAmount.fillAmount = (float)currentExp / (float)futureLevel.startExp;
            }
         
        }
        else
        {
            futureLevelText.text = " ";
        }
        if(expFillAmount.fillAmount == 1)
        {
            IEnumerator CorResetFillAmount()
            {
                yield return new WaitForSeconds(0.2f);
                expFillAmount.fillAmount = 0;
            }
        }
        
    }
  
    private void OnApplicationPause(bool pause)
    {
        if(pause == true)
        StickmanUpgrader.SetExpStickman(currentExp);
    }
    private void OnApplicationQuit()
    {
        StickmanUpgrader.SetExpStickman(currentExp);
    }
    private void OnDestroy()
    {
        StickmanUpgrader.SetExpStickman(currentExp);
    }
}
