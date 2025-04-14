using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    [SerializeField] private RectTransform gridRect;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private Card cardPrefab;

    public void Init(int rows, int cols)
    {
        int numOfCards = rows * cols;
        // Safety check
        if ((numOfCards % 2) != 0)
        {
            Debug.LogError("Grid must have an even number of cards!");
            return;
        }
        
        // Clear old cards
        foreach (RectTransform child in gridRect)
        {
            Destroy(child.gameObject);
        }
        
        gridLayoutGroup.constraintCount = cols;
        // Adjust cell size dynamically based on screen size
        float width = gridRect.rect.width / cols;
        float height = gridRect.rect.height / rows;
        gridLayoutGroup.cellSize = new Vector2(width - gridLayoutGroup.spacing.x, height - gridLayoutGroup.spacing.y);

        List<int> numbers = new List<int>();
        int numOfPairs = numOfCards / 2;
        for (int i = 0; i < numOfPairs; i++)
        {
            numbers.Add(i + 1);
            numbers.Add(i + 1);
        }
        // Shuffle the numbers
        ShuffleList(numbers);
        
        // Instantiate cards and assign matching pairs
        for (int i = 0; i < numOfCards; i++)
        {
            Card generatedCard = Instantiate(cardPrefab, gridRect);
            generatedCard.Init(numbers[i]);
        }
    }
    
    private static void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}
