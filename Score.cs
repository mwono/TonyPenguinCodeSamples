using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    static float score = 0f;
    static float fadeTimer = 2f;
    static float timeSinceActive;
    static int stuntScore;
    static Text[] Texts;//{Score,Stunt,Multiplier,Tricks}
    static Image[] Images;//{Score,Stunt,Multiplier,Tricks}

    void Start() {
        Texts = GetComponentsInChildren<Text>();
        Images = GetComponentsInChildren<Image>();
        Images[0].CrossFadeAlpha(0, 0f, false);
        Texts[0].CrossFadeAlpha(0, 0f, false);
        Images[1].CrossFadeAlpha(0, 0f, false);
        Texts[1].CrossFadeAlpha(0, 0f, false);
        Images[2].CrossFadeAlpha(0, 0f, false);
        Texts[2].CrossFadeAlpha(0, 0f, false);
        Images[3].CrossFadeAlpha(0, 0f, false);
        Texts[3].CrossFadeAlpha(0, 0f, false);
        timeSinceActive = 0;
    }
    void Update ()
    {
        timeSinceActive += Time.deltaTime;//calculating time until the UI needs to fade
        if (timeSinceActive >= fadeTimer)//fade away the UI
        {
            Images[0].GetComponent<Image>().CrossFadeAlpha(0,.35f,false);
            Texts[0].GetComponent<Text>().CrossFadeAlpha(0, .4f, false);
            Images[1].GetComponent<Image>().CrossFadeAlpha(0, .35f, false);
            Texts[1].GetComponent<Text>().CrossFadeAlpha(0, .4f, false);
            Images[2].GetComponent<Image>().CrossFadeAlpha(0, .35f, false);
            Texts[2].GetComponent<Text>().CrossFadeAlpha(0, .4f, false);
            Images[3].GetComponent<Image>().CrossFadeAlpha(0, .35f, false);
            Texts[3].GetComponent<Text>().CrossFadeAlpha(0, .4f, false);
        }
    }
    /**
    *@param value: value to update score with
    *@param combo: value to multiply with score, set to 1 if no combo
    */
    public static void UpdateScore (int value, int combo) {
        score += value * combo;//Score gets updated
        Texts[0].text = "Score: " + score;//update text
        Images[0].GetComponent<Image>().CrossFadeAlpha(1, .10f, false);//unfade UI for score
        Texts[0].GetComponent<Text>().CrossFadeAlpha(1, .25f, false);
        timeSinceActive = 0;//reset the time since the UI was active
    }

    public static void ShowStuntScore(int value)//function to show the total score of the stunt on the UI
    {
        stuntScore += value;
        Texts[1].text = value.ToString();//update text
        Images[1].GetComponent<Image>().CrossFadeAlpha(1, .10f, false);//unfade UI for stuntScore
        Texts[1].GetComponent<Text>().CrossFadeAlpha(1, .25f, false);
        timeSinceActive = 0;//reset the time since the UI was active
    }

    public static void ShowCombos(int combo)//function to show the total combo of the stunt on the UI
    {
        Texts[2].text = "X" + combo;//update text
        Images[2].GetComponent<Image>().CrossFadeAlpha(1, .10f, false);//unfade UI for Combo meter
        Texts[2].GetComponent<Text>().CrossFadeAlpha(1, .25f, false);
        timeSinceActive = 0;//reset the time since the UI was active
    }

    public static void ShowTrick(string trick)
    {
        Texts[3].text = trick;
        Images[3].CrossFadeAlpha(1, .10f, false);
        Texts[3].CrossFadeAlpha(1, .25f, false);
        timeSinceActive = 0;
    }
}
