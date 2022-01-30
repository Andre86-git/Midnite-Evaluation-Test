using System.Collections.Generic;
using UnityEngine;

public class SandwichGameGrid
{
    private SandwichGameGridSection[,] gridSections;
    private int size;
    private float vOffset;
    public readonly Transform elementsRoot;

    public SandwichGameGrid(int size, float vOffset, Transform elementsRoot)
    {
        gridSections = new SandwichGameGridSection[size, size];
        for (int i=0; i<size; i++)
        {
            for (int j=0; j<size; j++)
            {
                gridSections[i, j] = new SandwichGameGridSection(this, new Vector2Int(i, j), new Vector3(i + 0.5f, 0, j + 0.5f));
            }
        }

        this.size = size;
        this.vOffset = vOffset;
        this.elementsRoot = elementsRoot;
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

    public bool CheckGridCompleted()
    {
        foreach (var section in gridSections)
        {
            if (!section.IsEmpty && !section.IsCompleted)
            {
                return false;
            }
        }

        return true;
    }
}
