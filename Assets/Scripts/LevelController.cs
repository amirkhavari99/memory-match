using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<LevelData> levelDatas;
    [SerializeField] private Grid gridController;
    [SerializeField] private Text levelText;
    [SerializeField] private Text movesCountText;
    
    public bool IsBusy
    {
        get;
        private set;
    }
    private Card _firstFlippedCard = null;
    private Card _secondFlippedCard = null;
    private int _currentLevelIndex = 0;
    private int _totalPairs;
    private int _matchedPairs = 0;
    private int _movesCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Init(levelDatas[_currentLevelIndex]);
    }

    private void Init(LevelData levelData)
    {
        if (levelData != null)
        {
            int cols = levelData.columns;
            int rows = levelData.rows;
            levelText.text = levelData.levelName;
            
            _totalPairs = (cols * rows) / 2;
            _matchedPairs = 0;
            _movesCount = 0;
            SetMovesCount();
            gridController.Init(rows, cols, this);   
        }
    }
    
    public void OnCardFlipped(Card card)
    {
        if (_firstFlippedCard == null)
        {
            _firstFlippedCard = card;
        }
        else if (_secondFlippedCard == null)
        {
            _secondFlippedCard = card;
            StartCoroutine(CheckMatch());
        }

        _movesCount++;
        SetMovesCount();
    }

    IEnumerator CheckMatch()
    {
        IsBusy = true;

        yield return new WaitForSeconds(0.5f);

        if (_firstFlippedCard.IsMatch(_secondFlippedCard))
        {
            _firstFlippedCard.MarkAsMatched();
            _secondFlippedCard.MarkAsMatched();
            
            _matchedPairs++;
            if (_matchedPairs >= _totalPairs)
            {
                OnLevelComplete();
            }
        }
        else
        {
            _firstFlippedCard.FlipBack();
            _secondFlippedCard.FlipBack();
        }

        _firstFlippedCard = null;
        _secondFlippedCard = null;
        IsBusy = false;
    }

    private void SetMovesCount()
    {
        movesCountText.text = $"Moves: {_movesCount}";
    }
    
    void OnLevelComplete()
    {
        Debug.Log("🎉 Level Complete!");
        levelText.text = "Level Completed!";
        // Load next level here
        StartCoroutine(LoadNextLevel());
    }
    
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        // Load next LevelData and regenerate grid
        _currentLevelIndex++;
        if (_currentLevelIndex < levelDatas.Count)
        {
            Start(); // Reset game
        }
        else
        {
            Debug.Log("🚀 All levels complete!");
            levelText.text = "All Levels Completed!";
            
        }
    }
}
