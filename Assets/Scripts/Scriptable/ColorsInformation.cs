using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Color Information", menuName = "Game Settings/Color Information", order = 0)]
public class ColorsInformation : ScriptableObject
{
    public List<Color> Colors;
    public int playerColorIndex;

    public Color GetPlayerColor()
    {
        if (playerColorIndex < 0)
        {
            return GetRandomColor();
        }

        return Colors[playerColorIndex];
    }

    public Color GetRandomColor()
    {
        return Colors[Random.Range(0, Colors.Count)];
    }
}
