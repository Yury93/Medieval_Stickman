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

    private void Start()
    {
        EnemiesService.instance.SpawnSystem.AllEnemies.ForEach(e => e.OnEnemyDeath += RefreshInfoLevel);
        RefreshInfoLevel(null);
    }

    private void RefreshInfoLevel(Enemy enemy)
    {
        var currentExp = StickmanUpgrader.GetExpStickman();

        ExpLevels.Levels currentLevel = levels.LastOrDefault(l => l.startExp <= currentExp);
        ExpLevels.Levels futureLevel = levels.LastOrDefault(l => l.startExp <= currentExp + 1);

        currentLevelText.text = currentLevel.Level.ToString(); 
        if(futureLevel!= null)
        {
            futureLevelText.text = futureLevel.Level.ToString();
            if (currentExp != 0)
            expFillAmount.fillAmount = currentExp/futureLevel.startExp;
         
        }
        else
        {
            futureLevelText.text = " ";
        }

        
    }
}
