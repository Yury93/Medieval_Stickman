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
    [SerializeField] private TextMeshProUGUI currentLevelText, futureLevelText, expText;
    [SerializeField] private Image expFillAmount;
    public int CurrentExp { get; private set; }
   public ExpLevels.Levels CurrentLevel { get; private set; }
    public ExpLevels.Levels FutureLevel { get; private set; }
    public Action<int> OnPlusExp;

    public void Init()
    {
        EnemiesService.instance.SpawnSystem.AllEnemies.ForEach(e => e.OnEnemyDeath += RefreshInfoLevel);
        CurrentExp = StickmanSaveUpgrader.GetExpStickman();
        RefreshInfoLevel(null);

        EnemiesService.instance.SpawnSystem.EnemySpawners.ForEach(s => s.OnEnemySpawn += OnEnemySpawn);

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



        currentLevelText.text = CurrentLevel.Level.ToString();
        if (FutureLevel != null)
        {
            expText.text = CurrentExp + "/" + FutureLevel.startExp;
            futureLevelText.text = FutureLevel.Level.ToString();
            if (CurrentExp != 0)
            {
                expFillAmount.fillAmount = (CurrentExp - CurrentLevel.startExp) / (float)FutureLevel.startExp;
            }

        }
        else
        {
            futureLevelText.text = " ";
            expText.text = CurrentExp + "/" + CurrentExp;
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
