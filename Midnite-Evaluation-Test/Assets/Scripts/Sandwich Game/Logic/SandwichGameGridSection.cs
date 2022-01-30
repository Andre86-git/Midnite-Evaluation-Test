using System.Collections.Generic;
using UnityEngine;

public class SandwichGameGridSection
{
    public readonly List<SandwichIngredientIstance> ingredientsStack;
    public readonly Vector3 sectionPosition;
    public readonly Vector2Int sectionIndex;

    public bool IsEmpty { get { return ingredientsStack.Count == 0; } }
    public bool IsCompleted { get { return ingredientsStack.Count > 0 && ingredientsStack[0].isBread && ingredientsStack[ingredientsStack.Count -1].isBread; } }

    public SandwichGameGridSection(Vector2Int sectionIndex, Vector3 sectionPosition)
    {
        ingredientsStack = new List<SandwichIngredientIstance>();
        this.sectionPosition = sectionPosition;
        this.sectionIndex = sectionIndex;
    }

    public void InitWithIngredient(SandwichIngredientData ingredientData, bool isBread)
    {
        foreach (var ingredient in ingredientsStack)
        {
            GameObject.Destroy(ingredient.gameObject);
        }
        ingredientsStack.Clear();

        if (ingredientData != null)
        {
            GameObject ingedientIstanceObj = GameObject.Instantiate(ingredientData.ingredientObject);
            SandwichIngredientIstance ingredientIstance = InitIngredientIstanceObject(ingredientData, ingedientIstanceObj);
            ingredientIstance.isBread = isBread;
            ingredientsStack.Add(ingredientIstance);            
        }
    }

    public void TransferIngredientsTo(SandwichGameGridSection other, float vOffset)
    {
        SandwichIngredientIstance ingredient;
        int vOffsetIndex = other.ingredientsStack.Count;
        while (ingredientsStack.Count > 0)
        {
            ingredient = ingredientsStack[ingredientsStack.Count - 1];
            other.ingredientsStack.Add(ingredientsStack[ingredientsStack.Count - 1]);

            ingredient.transform.position = new Vector3(other.sectionPosition.x, vOffset * vOffsetIndex, other.sectionPosition.z);
            vOffsetIndex++;
            ingredient.currentSection = other;

            ingredientsStack.RemoveAt(ingredientsStack.Count - 1);
        }
    }

    private SandwichIngredientIstance InitIngredientIstanceObject(SandwichIngredientData ingredientData, GameObject ingedientIstanceObj)
    {
        ingedientIstanceObj.transform.localScale = new Vector3(ingredientData.scaleBy, ingredientData.scaleBy, ingredientData.scaleBy);
        ingedientIstanceObj.transform.position = sectionPosition;

        Collider collider = ingedientIstanceObj.GetComponent<Collider>();
        if (collider == null)
        {
            ingedientIstanceObj.AddComponent<MeshCollider>();
        }

        SandwichIngredientIstance ingredientIstance = ingedientIstanceObj.GetComponent<SandwichIngredientIstance>();
        if (ingredientIstance == null)
        {
            ingredientIstance = ingedientIstanceObj.AddComponent<SandwichIngredientIstance>();
        }

        ingredientIstance.ingredientData = ingredientData;
        ingredientIstance.currentSection = this;

        return ingredientIstance;
    }
}
