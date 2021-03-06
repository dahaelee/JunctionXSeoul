﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class today_todayManager : MonoBehaviour
{
    public UI_manager UImanager;
    public Text hashcode;
    public Image UIbackground;
    public GameObject card;

    // card related
    string pickedCard;

    private void Awake()
    {
        dataLoad();
    }

    void Start()
    {
        UIbackground.gameObject.SetActive(false);

        UImanager.allUIoff();
        hashcode.GetComponent<Text>().text = user.hashcode.ToString();

        StartCoroutine("SpreadCards");
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

    public bool checkDoubleClick(string tempCard)
    {
        if(pickedCard == tempCard)
        {
            // Tell the card to be picked up
            return true;
        }
        else
        {
            if (pickedCard != null)
            {
                // put the previous card back into it's place
                GameObject temp = GameObject.Find(pickedCard);
                temp.GetComponent<RectTransform>().localPosition -= Vector3.down * 150.0f;
            }

            pickedCard = tempCard;
            return false;
        }
    }

    public void renewCardDeck()
    {
        if (pickedCard != null)
        {
            // put the previous card back into it's place
            GameObject temp = GameObject.Find(pickedCard);
            temp.GetComponent<RectTransform>().localPosition -= Vector3.down * 150.0f;

            pickedCard = null;
        }
    }
        
    IEnumerator SpreadCards()
    {
        yield return new WaitForSeconds(0.8f);
        GameObject CardDeck = GameObject.Find("CardPack");

        float i = 0.0f;
        float rate = 1.3f;

        while (i < 1.0)
        {
            yield return new WaitForEndOfFrame();
            i = i + (Time.deltaTime * rate)*1.7f;
            CardDeck.GetComponent<GridLayoutGroup>().spacing = Vector2.Lerp(new Vector2(-120,0), new Vector2(15,0), i);
        }
    }

        public void dataLoad()
    {
        user.hashcode = PlayerPrefs.GetString("hashcode", user.hashcode);
    }
}
