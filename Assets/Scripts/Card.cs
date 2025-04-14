using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    [SerializeField] private Color backColor;
    [SerializeField] private Color frontColor;
    [SerializeField] private Color matchedColor;
    [SerializeField] private Button button;

    private int _cardNumber;
    private bool _isFlipped;
    private bool _isMatched;
    private LevelController _levelController;
    
    public void Init(int number, LevelController levelController)
    {
        _cardNumber = number;
        _levelController = levelController;
        text.text = _cardNumber.ToString();
        text.enabled = false;
        image.color = backColor;
        _isFlipped = false;
    }

    public void OnClick()
    {
        if (_isFlipped || _isMatched || _levelController.IsBusy)
            return;

        Flip();
        _levelController.OnCardFlipped(this);
    }
    
    
    private void Flip()
    {
        _isFlipped = true;
        OnFlipChanged();
    }

    public void FlipBack()
    {
        _isFlipped = false;
        OnFlipChanged();
    }
    
    private void OnFlipChanged()
    {
        image.color = _isFlipped ? frontColor : backColor;
        text.enabled = _isFlipped;
    }

    public bool IsMatch(Card other)
    {
        return _cardNumber == other._cardNumber;
    }
    
    public void MarkAsMatched()
    {
        _isMatched = true;
        image.color = matchedColor;
        button.interactable = false;
    }
}
