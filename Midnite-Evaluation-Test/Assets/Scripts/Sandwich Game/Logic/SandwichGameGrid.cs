using System.Collections.Generic;
using UnityEngine;

public class SandwichGameGrid
{
    private SandwichGameGridSection[,] gridSections;
    private int size;
    float vOffset;

    public SandwichGameGrid(int size, float vOffset)
    {
        gridSections = new SandwichGameGridSection[size, size];
        for (int i=0; i<size; i++)
        {
            for (int j=0; j<size; j++)
            {
                gridSections[i, j] = new SandwichGameGridSection(new Vector2Int(i, j), new Vector3(i + 0.5f, 0, j + 0.5f));
            }
        }

        this.size = size;
        this.vOffset = vOffset;
    }

    public void InitWithData(SandwichLevelData levelData)
    {
        if (levelData != null)
        {
            SandwichIngredientData ingredientData;
            for (int i=0; i<size; i++)
            {
                for (int j=0; j< size; j++)
                {
                    ingredientData = levelData.ingredientsGridData[i, j];
                    gridSections[i, j].InitWithIngredient(ingredientData, (ingredientData != null) && ingredientData.ingredientName.Equals(levelData.breadIngredientName));
                }
            }
        }
    }

    public List<SandwichIngredientIstance> GetIngredientsAt(int x, int y)
    {
        List<SandwichIngredientIstance> result = null;
        if (x >= 0 && x < size && y >= 0 && y < size)
        {
            if (!gridSections[x,y].IsEmpty)
            {
                result = new List<SandwichIngredientIstance>();
                result.AddRange(gridSections[x, y].ingredientsStack);
            }
        }

        return result;
    }

    public bool TransferIngredients(Vector2Int from, Vector2Int to)
    {
        if ((from.x >= 0) && (from.x < size) &&
            (from.x >= 0) && (from.y < size) &&
            (to.x >= 0) && (to.x < size) &&
            (to.y >= 0) && (to.y < size) && 
            (from.x != to.x || from.y != to.y))
        {
            SandwichGameGridSection fromSection = gridSections[from.x, from.y];
            SandwichGameGridSection toSection = gridSections[to.x, to.y];

            if (!fromSection.IsEmpty && !toSection.IsEmpty)
            {
                fromSection.TransferIngredientsTo(toSection, vOffset);

                return true;
            }
        }

        return false;
    }
}
