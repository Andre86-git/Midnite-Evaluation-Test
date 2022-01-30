using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichFixed4x4LevelGenerator : SandwichLevelGenerator
{
    [Space()]
    public string ingredientAt_0_0;
    public string ingredientAt_1_0;
    public string ingredientAt_2_0;
    public string ingredientAt_3_0;
    public string ingredientAt_0_1;
    public string ingredientAt_1_1;
    public string ingredientAt_2_1;
    public string ingredientAt_3_1;
    public string ingredientAt_0_2;
    public string ingredientAt_1_2;
    public string ingredientAt_2_2;
    public string ingredientAt_3_2;
    public string ingredientAt_0_3;
    public string ingredientAt_1_3;
    public string ingredientAt_2_3;
    public string ingredientAt_3_3;

    public override SandwichLevelData GenerateLevelData(SandwichData sandwichData)
    {
        SandwichIngredientData[,] ingredientsGridData = new SandwichIngredientData[4, 4];
        ingredientsGridData[0, 0] = GetIngredient(sandwichData, ingredientAt_0_0);
        ingredientsGridData[1, 0] = GetIngredient(sandwichData, ingredientAt_1_0);
        ingredientsGridData[2, 0] = GetIngredient(sandwichData, ingredientAt_2_0);
        ingredientsGridData[3, 0] = GetIngredient(sandwichData, ingredientAt_3_0);
        ingredientsGridData[0, 1] = GetIngredient(sandwichData, ingredientAt_0_1);
        ingredientsGridData[1, 1] = GetIngredient(sandwichData, ingredientAt_1_1);
        ingredientsGridData[2, 1] = GetIngredient(sandwichData, ingredientAt_2_1);
        ingredientsGridData[3, 1] = GetIngredient(sandwichData, ingredientAt_3_1);
        ingredientsGridData[0, 2] = GetIngredient(sandwichData, ingredientAt_0_2);
        ingredientsGridData[1, 2] = GetIngredient(sandwichData, ingredientAt_1_2);
        ingredientsGridData[2, 2] = GetIngredient(sandwichData, ingredientAt_2_2);
        ingredientsGridData[3, 2] = GetIngredient(sandwichData, ingredientAt_3_2);
        ingredientsGridData[0, 3] = GetIngredient(sandwichData, ingredientAt_0_3);
        ingredientsGridData[1, 3] = GetIngredient(sandwichData, ingredientAt_1_3);
        ingredientsGridData[2, 3] = GetIngredient(sandwichData, ingredientAt_2_3);
        ingredientsGridData[3, 3] = GetIngredient(sandwichData, ingredientAt_3_3);

        return new SandwichLevelData(ingredientsGridData, sandwichData.breadIngredient.ingredientName);
    }

    private SandwichIngredientData GetIngredient(SandwichData sandwichData, string ingredientName)
    {
        if (ingredientName.Equals(sandwichData.breadIngredient.ingredientName))
        {
            return sandwichData.breadIngredient;
        }
        else
        {
            foreach (var ingredient in sandwichData.additionaIngredients)
            {
                if (ingredientName.Equals(ingredient.ingredientName))
                {
                    return ingredient;
                }
            }

            return null;
        }
    }
}
