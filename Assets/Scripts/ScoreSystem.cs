using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private int _gameScore = 0;
    private int _timesClicked = 0;
    [SerializeField] private TMP_Text _timeClickedText;

    public void BrokeItem(int m_itemWorth)
    {
        _gameScore += m_itemWorth;
        _scoreText.text = "Break Score: " + _gameScore.ToString();
        ClickedCounter();
    }

    public void ClickedCounter()
    {
        _timesClicked++;
        _timeClickedText.text = "Times Clicked: " + _timesClicked.ToString();

    }
}
