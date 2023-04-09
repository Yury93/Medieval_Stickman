using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSystem : MonoBehaviour
{
    public static LanguageSystem instance;
    [Serializable]
    public struct KeyValueLocalization
    {
        [TextArea(2,10)]
        public string ru;
        [TextArea(2, 10)]
        public string eng;
    }
    [SerializeField] List<KeyValueLocalization> keyValueLocalizations;
    [SerializeField] private Dictionary<string, string> translater;
    public Dictionary<string, string> Translater => translater;
    public bool isEnglish;

    public Action OnChangeLanguage;

  
    private const string ENG_PREFS = "ENGPREFS";

    public void Awake()
    {
        instance = this;

        if (PlayerPrefs.GetInt(ENG_PREFS) != 1)
            ChangeRussian();
        else
            ChangeEnglish();
    
    }
    public void ChangeEnglish()
    {
        isEnglish = true;
        translater = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var keyValue in keyValueLocalizations)
        {
            translater.Add(keyValue.ru, keyValue.eng);
        }
   
        OnChangeLanguage?.Invoke();
        PlayerPrefs.SetInt(ENG_PREFS, 1);
    }
    public void ChangeRussian()
    {
        
        translater = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
      
            foreach (var keyValue in keyValueLocalizations)
            {
                translater.Add(keyValue.ru, keyValue.ru); // сделано так потому что изначально все ключи на русском
            }
        
        isEnglish = false;
        OnChangeLanguage?.Invoke();
        PlayerPrefs.SetInt(ENG_PREFS, 0);
    }
}
