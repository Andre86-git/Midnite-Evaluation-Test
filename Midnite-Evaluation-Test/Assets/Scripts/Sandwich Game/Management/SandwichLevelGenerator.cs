using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SandwichLevelGenerator : MonoBehaviour
{
    public virtual SandwichLevelData GenerateLevelData(SandwichData sandwichData)
    {
        return null;
    }
}
