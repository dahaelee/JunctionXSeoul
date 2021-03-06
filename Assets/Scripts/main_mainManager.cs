﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main_mainManager : MonoBehaviour
{
    public UI_manager UImanager;
    public Text hashcode;
    public Image UIbackground;

    private void Awake()
    {
        dataLoad();
    }

    void Start()
    {
        UIbackground.gameObject.SetActive(false);

        UImanager.allUIoff();
        hashcode.GetComponent<Text>().text = user.hashcode.ToString();
    }

    void Update()
    {
        
    }

    public void setting()
    {
        StartCoroutine(UImanager.UIon(UI_manager.UIstate.setting));
    }

    public void logout()
    {
        PlayerPrefs.DeleteAll();
        UImanager.UIoff(UI_manager.UIstate.setting);
        StartCoroutine("fadeoutAndLogin");
    }

    public void tadayFortune() 
    {
        StartCoroutine("fadeoutAndToday");
    }

    public IEnumerator fadeoutAndLogin()
    {
        float fade_time = 0.5f, start = 0.5f, end = 0.7f, time = 0f;
        Color fade_color = UIbackground.color;

        while (fade_color.a < end)
        {
            time += Time.deltaTime / fade_time;
            fade_color.a = Mathf.Lerp(start, end, time);
            UIbackground.color = fade_color;
            yield return null;
        }

        SceneManager.LoadScene("login");
    }

    public IEnumerator fadeoutAndToday()
    {
        float fade_time = 0.5f, start = 0.5f, end = 0.7f, time = 0f;
        Color fade_color = UIbackground.color;

        while (fade_color.a < end)
        {
            time += Time.deltaTime / fade_time;
            fade_color.a = Mathf.Lerp(start, end, time);
            UIbackground.color = fade_color;
            yield return null;
        }

        SceneManager.LoadScene("today");
    }

    public void dataLoad()
    {
        user.hashcode = PlayerPrefs.GetString("hashcode", user.hashcode);
    }
}
