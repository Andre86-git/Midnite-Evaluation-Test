using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SandwichGameLevelGenerator : MonoBehaviour
{
    public virtual SandwichLevelData GenerateLevelData(SandwichData sandwichData, int gridSize)
    {
        return null;
    }
}
