using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Button button;

    private int _cardNumber;
    private bool _isFlipped;
    
    public void Init(int number)
    {
        _cardNumber = number;
        text.text = _cardNumber.ToString();
        text.enabled = false;
        image.sprite = backSprite;
        _isFlipped = false;
    }

    public void FlipCard()
    {
        _isFlipped = !_isFlipped;
        image.sprite = _isFlipped ? frontSprite : backSprite;
        text.enabled = _isFlipped;
    }

    public void MarkAsMatched()
    {
        button.interactable = false;
    }
}
