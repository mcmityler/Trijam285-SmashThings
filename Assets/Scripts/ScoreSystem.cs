using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ScoreSystem : MonoBehaviour
{    
    private int _gameScore = 0;
    [SerializeField] private TMP_Text _scoreText;
    
    private int _timesClicked = 0;
    [SerializeField] private TMP_Text _timesClickedText;

    [SerializeField] private TMP_Text _timeElapsedText;
    private float _minsPassed = 0f;
    private float _secondsPassed = 0;

    private bool _isPlaying = true; // if the game is being played then count the amount of clicks

    private void FixedUpdate()
    {
        _secondsPassed += Time.deltaTime;
        if (_secondsPassed >= 60)
        {
            _minsPassed++;
            _secondsPassed = 0;
        }
        _timeElapsedText.text =   "Time Passed: " +string.Format("{0:00}:{1:00}", _minsPassed, _secondsPassed);

        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isPlaying == true)
        {
            ClickedCounter();
        }
    }

    public void BrokeItem(int m_itemWorth)
    {
        _gameScore += m_itemWorth;
        _scoreText.text = "Break Score: " + _gameScore.ToString();
    }

    public void ClickedCounter()
    {
        _timesClicked++;
        _timesClickedText.text = "Times Clicked: " + _timesClicked.ToString();

    }
}
