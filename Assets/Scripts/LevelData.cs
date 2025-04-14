using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "MemoryGame/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    [Range(2, 6)]
    public int columns = 4;

    [Range(3, 6)]
    public int rows = 4;

    public string levelName = "Level 1";
}