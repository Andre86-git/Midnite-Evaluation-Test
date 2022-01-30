using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SandwichData : ScriptableObject
{
    public SandwichIngredientData breadIngredient;
    public List<SandwichIngredientData> additionaIngredients;
}
