
public class SandwichLevelData
{
    public readonly SandwichIngredientData[,] ingredientsGridData;
    public readonly string breadIngredientName;

    public SandwichLevelData(SandwichIngredientData[,] ingredientsGridData, string breadIngredientName)
    {
        this.ingredientsGridData = ingredientsGridData;
        this.breadIngredientName = breadIngredientName;
    }
}
