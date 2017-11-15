using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuntManager : MonoBehaviour
{

    PlayerEventHandler events;

    bool grounded;
    int totalScore;//Total score of the stunt
    float SpinAmtHoriz;//Keeps track of how much the player has spun horizontally
    float SpinAmtVert;//Keeps Keeps track of how much the player has spun vertically
    float lastAngleV = 0, lastAngleH = 0;//Player angle of the last frame
    int comboMeter;//Combo meter
    float stuntTime;
    int totalSpins = 0;

    void OnEnable()
    {
        events = GetComponent<PlayerEventHandler>();
        Listeners(true);
    }

    void OnDisable()
    {
        Listeners(false);
    }

    void Listeners(bool state)
    {
        if (state)
        {
            events.OnWipeout += OnWipeout;
            events.OnLanding += OnLanding;
           events.OnLiftoff += OnLiftoff;
        } else
        {
            events.OnWipeout -= OnWipeout;
            events.OnLanding -= OnLanding;
            events.OnLiftoff -= OnLiftoff;
        }
    }

    void OnLanding()
    {
        if (totalScore != 0)//update the player's score upon stunt completion aka landing good
        {
            Score.UpdateScore(totalScore, comboMeter);
            totalSpins = 0;
        }
        comboMeter = 1;
        totalScore = 0;
        grounded = true;
    }


    void OnWipeout()//Reset everything on wipeouts
    {
        comboMeter = 1;
        totalScore = 0;
        grounded = true;
        totalSpins = 0;
    }

    void OnLiftoff(RaycastHit hit)
    {
        grounded = false;
    }

    void Update()
    {
        if (Input.GetAxis("HorizontalSpin") != 0 && !grounded)//Detects spins
        {
            float currentAngleH = transform.localEulerAngles[1];
            float currentAngleV = transform.localEulerAngles[2];
            SpinAmtHoriz += Mathf.DeltaAngle(currentAngleH, lastAngleH);//calculating angle difference from last frame
            SpinAmtVert += Mathf.DeltaAngle(currentAngleV, lastAngleV);//calculating angle difference from last frame
            lastAngleH = currentAngleH;
            lastAngleV = currentAngleV;
            if (SpinAmtHoriz > 180 || SpinAmtHoriz < -180)
            {
                totalSpins += 180;
                if (SpinAmtHoriz < 0)
                {
                    Score.ShowTrick("FS" + totalSpins.ToString());
                } else
                {
                    Score.ShowTrick("BS" + totalSpins.ToString());
                }
                totalScore += 50;
                SpinAmtHoriz = 0;
                updateCombo();
            } else if (SpinAmtVert > 180 || SpinAmtVert < -180)
            {
                totalSpins += 180;
                if (SpinAmtVert < 0)
                {
                    Score.ShowTrick("Backflip" + totalSpins.ToString());
                }
                else
                {
                    Score.ShowTrick("Frontflip" + totalSpins.ToString());
                }
                totalScore += 50;
                SpinAmtVert = 0;
                updateCombo();
            }
        }
        /** adds to score when grinding a rail, doesn't work at the moment, need to add
        public bool getGrind()
        {
            return grind;
        } to RailGrind to work
        if (GetComponent<RailGrind>().getGrind() && !grounded)
        {
            totalScore += 2;
            stuntTime += Time.deltaTime;
            if (stuntTime == 3)
            {
                updateCombo();
                stuntTime = 0;
            }
        }
        */
        if (Input.GetButtonDown("Jump") && grounded)//Detects jumps
        {
            Score.ShowTrick("Ollie");
            totalScore += 10;
        }
        if (totalScore != 0)//shows score while stunt score is not 0
        {
            Score.ShowStuntScore(totalScore);
        }
    }

    void updateCombo()
    {
        comboMeter++;
        Score.ShowCombos(comboMeter);
    }
}
