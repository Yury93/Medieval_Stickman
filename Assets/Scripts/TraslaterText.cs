using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TraslaterText : MonoBehaviour
{
    [TextArea(5,10)]
    [SerializeField] private string ru, eng;
    [SerializeField] private TextMeshProUGUI text;
    void Start()
    {
        LanguageSystem.instance.OnChangeLanguage += ChangeLang;
        text = GetComponent<TextMeshProUGUI>();
        ChangeLang();
    }

 
    private void ChangeLang()
    {
        if(LanguageSystem.instance.isEnglish == true)
        {
            text.text = eng;
        }
        else
        {
            text.text = ru;
        }
    }
    private void OnDestroy()
    {
        LanguageSystem.instance.OnChangeLanguage -= ChangeLang;
    }
}
