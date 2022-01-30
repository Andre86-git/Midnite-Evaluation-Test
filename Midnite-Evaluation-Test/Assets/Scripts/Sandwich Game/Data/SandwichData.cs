using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSandwichData", menuName = "Sandwich Game/SandwichData", order = 1)]
public class SandwichData : ScriptableObject
{
    public SandwichIngredientData breadIngredient;
    public List<SandwichIngredientData> additionaIngredients;
}
