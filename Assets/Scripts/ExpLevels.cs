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
        public bool isButtonOpened;
    }
    public List<Levels> levels;
    [SerializeField] private TextMeshProUGUI currentLevelText,persPanelLevelText, futureLevelText, expText;
    [SerializeField] private Image expFillAmount;

    public int CurrentExp { get; private set; }
   public ExpLevels.Levels CurrentLevel { get; private set; }
    public ExpLevels.Levels FutureLevel { get; private set; }
    public Action<int> OnPlusExp;

    public void Init()
    {
        CoreEnivroment.Instance.enemiesService.SpawnSystem.AllEnemies.ForEach(e => e.OnEnemyDeath += RefreshInfoLevel);
        CurrentExp = StickmanSaveUpgrader.GetExpStickman();
        RefreshInfoLevel(null);

        CoreEnivroment.Instance.enemiesService.SpawnSystem.EnemySpawners.ForEach(s => s.OnEnemySpawn += OnEnemySpawn);

        CurrentExp = StickmanSaveUpgrader.GetExpStickman();

    }

    private void OnEnemySpawn(Enemy enemy)
    {
        enemy.OnEnemyDeath += RefreshInfoLevel;
    }

    private void RefreshInfoLevel(Enemy enemy)
    {
        if (enemy)
            CurrentExp += enemy.Exp;

        CurrentLevel = levels.LastOrDefault(l => l.startExp <= CurrentExp);
        FutureLevel = levels.LastOrDefault(l => l.Level <= CurrentLevel.Level + 1);


        persPanelLevelText.text = LanguageSystem.instance.Translater.GetValueOrDefault("Уровень") + ": " + CurrentLevel.Level.ToString();
        currentLevelText.text = CurrentLevel.Level.ToString();
        if (FutureLevel != null)
        {
            expText.text = CurrentExp + "/" + FutureLevel.startExp;
            futureLevelText.text = FutureLevel.Level.ToString();
            if (CurrentExp != 0)
            {
                expFillAmount.fillAmount = ((float)CurrentExp  - (float)CurrentLevel.startExp) /
                    ((float)FutureLevel.startExp - (float)CurrentLevel.startExp);
            }
            if (expFillAmount.fillAmount == 1)
            {
                StartCoroutine(CorResetFillAmount());
                IEnumerator CorResetFillAmount()
                {
                    yield return new WaitForSeconds(0.2f);
                    expFillAmount.fillAmount = 0;
                }
            }
        }
        else
        {
            futureLevelText.text = " ";
            expText.text = CurrentExp + "/" + CurrentExp;
           
            expFillAmount.fillAmount = 1;
        }
      
      
        OnPlusExp?.Invoke(CurrentExp);
    }

    private void OnApplicationPause(bool pause)
    {

        if (pause == true)
            StickmanSaveUpgrader.SetExpStickman(CurrentExp);

    }
    private void OnApplicationQuit()
    {

        StickmanSaveUpgrader.SetExpStickman(CurrentExp);

    }
    private void OnDestroy()
    {

        StickmanSaveUpgrader.SetExpStickman(CurrentExp);

    } 
}
