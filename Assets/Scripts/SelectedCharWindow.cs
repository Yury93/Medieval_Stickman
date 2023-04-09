using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class SelectedCharWindow : Window
{
    public enum  Characters{ stickman, stickGirl }
    [SerializeField] private Button right, left, select, exit;
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    [SerializeField] private GameObject stickman, stickGirl;
    [SerializeField] private TextMeshProUGUI selectedCharText;
    [SerializeField] private Characters character = Characters.stickman;
    public const string ID_CHARACTERS = "ID_CHAR";

    void Start()
    {
        right.onClick.AddListener(OnClickRightButton);
        left.onClick.AddListener(OnClickLeftButton);
        exit.onClick.AddListener(Close);
        select.onClick.AddListener(SelectCharacters);
    }

    private void SelectCharacters()
    {
        if(character == Characters.stickGirl)
        {
            PlayerPrefs.SetInt(ID_CHARACTERS , 1);
        }
        else
        {
            PlayerPrefs.SetInt(ID_CHARACTERS, 0);
        }
        selectedCharText.enabled = true;
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        stickman.gameObject.SetActive(true);
        stickGirl.gameObject.SetActive(true);
        if (InterstateObject.instance.GetStickmanId() == 0)
        {
            OnClickLeftButton();
        }
        else
        {
            OnClickRightButton();
        }
        base.Open();
        selectedCharText.enabled = false;
    }
    public override void Close()
    {
        gameObject.SetActive(false);
        stickman.gameObject.SetActive(false);
        stickGirl.gameObject.SetActive(false);
        base.Close();
    }
    

    private void OnClickLeftButton()
    {
        left.interactable = false;
        right.interactable = true;
        cinemachine.LookAt = stickman.transform;
        cinemachine.m_Follow = stickman.transform;
        character = Characters.stickman;
    }

    private void OnClickRightButton()
    {
        right.interactable = false;
        left.interactable = true;
        cinemachine.LookAt = stickGirl.transform;
        cinemachine.m_Follow = stickGirl.transform;
        character = Characters.stickGirl;
    }

}
