using System.Collections.Generic;
using UnityEngine;

public class SandwichRandomLevelGenerator : SandwichGameLevelGenerator
{
    private static readonly Vector2Int[] DIRECTIONS = new Vector2Int[] {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, 1)
    };

    public int minAdditiveIngredients;
    public int maxAdditiveIngredients;

    public override SandwichLevelData GenerateLevelData(SandwichData sandwichData, int gridSize)
    {
        SandwichIngredientData[,] ingredientsGridData = new SandwichIngredientData[gridSize, gridSize];

        List<Vector2Int> avaibleExpandingPositions = new List<Vector2Int>();

        Vector2Int position = new Vector2Int(Random.Range(0, gridSize), Random.Range(0, gridSize));
        ingredientsGridData[position.x, position.y] = sandwichData.breadIngredient;
        avaibleExpandingPositions.Add(position);

        position = GetNextPosition(ingredientsGridData, gridSize, avaibleExpandingPositions);
        ingredientsGridData[position.x, position.y] = sandwichData.breadIngredient;
        avaibleExpandingPositions.Add(position);

        List<SandwichIngredientData> avaibleIngredients = GetAvaibleIngredients(sandwichData);
        int ingretdientsNum = (maxAdditiveIngredients > minAdditiveIngredients)
            ? Random.Range(minAdditiveIngredients, maxAdditiveIngredients + 1)
            : minAdditiveIngredients;

        int addedIngredients = 0;
        while ((addedIngredients < ingretdientsNum) && (avaibleIngredients.Count > 0) && (avaibleExpandingPositions.Count > 0))
        {
            position = GetNextPosition(ingredientsGridData, gridSize, avaibleExpandingPositions);
            avaibleExpandingPositions.Add(position);

            int ingredientIndex = Random.Range(0, avaibleIngredients.Count);
            ingredientsGridData[position.x, position.y] = avaibleIngredients[ingredientIndex];
            avaibleIngredients.RemoveAt(ingredientIndex);

            if (!CheckPositionAvaibility(position + DIRECTIONS[0], ingredientsGridData, gridSize) &&
                !CheckPositionAvaibility(position + DIRECTIONS[1], ingredientsGridData, gridSize) &&
                !CheckPositionAvaibility(position + DIRECTIONS[2], ingredientsGridData, gridSize) &&
                !CheckPositionAvaibility(position + DIRECTIONS[3], ingredientsGridData, gridSize))
            {
                avaibleExpandingPositions.Remove(position);
            }

            addedIngredients++;
        }

        return new SandwichLevelData(ingredientsGridData, sandwichData.breadIngredient.ingredientName); ;
    }

    private List<SandwichIngredientData> GetAvaibleIngredients(SandwichData sandwichData)
    {
        List<SandwichIngredientData> result = new List<SandwichIngredientData>();
        foreach (var ingredient in sandwichData.additionaIngredients)
        {
            result.Add(ingredient);
        }

        return result;
    }

    private Vector2Int GetNextPosition(SandwichIngredientData[,] ingredientsGridData, int gridSize, List<Vector2Int> avaibleNextStartPositions)
    {
        int nextPosIndex = Random.Range(0, avaibleNextStartPositions.Count);
        Vector2Int nextStartPos = avaibleNextStartPositions[nextPosIndex];

        int directionIndex = Random.Range(0, 4);
        Vector2Int direction = DIRECTIONS[directionIndex];
        Vector2Int nextPosition = nextStartPos + direction;

        int tries = 0;
        while (!CheckPositionAvaibility(nextPosition, ingredientsGridData, gridSize))
        {
            directionIndex = (directionIndex + 1) % 4;
            direction = DIRECTIONS[directionIndex];
            nextPosition = nextStartPos + direction;

            tries++;
            if (tries >= 4)
                break;
        }

        return nextPosition;
    }

    private bool CheckPositionAvaibility(Vector2Int position, SandwichIngredientData[,] ingredientsGridData, int gridSize)
    {
        return position.x >= 0 && position.x < gridSize
            && position.y >= 0 && position.y < gridSize
            && ingredientsGridData[position.x, position.y] == null;
    }
}