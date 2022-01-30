using UnityEngine;

public class SandwichLevelManager : MonoBehaviour
{
    public SandwichData ingredientsData;
    public SandwichLevelGenerator levelGenerator;
    public int gridSize = 4;

    [Space()]
    public float ingredientsVerticalOffset = 0.2f;
    public float timeBetweenMoves = 1.0f;

#if UNITY_EDITOR
    [Space()]
    public bool printDebug;
#endif

    private SandwichGameGrid gameGrid;
    private SandwichGameGridSection currentTouchedSection;
    private SandwichLevelData levelData;

    private bool readingInputs;
    private float inputTimeout;

    private void Start()
    {
        if (levelGenerator != null)
        {
            levelData = levelGenerator.GenerateLevelData(ingredientsData);
        }

        gameGrid = new SandwichGameGrid(gridSize, ingredientsVerticalOffset);
        gameGrid.InitWithData(levelData);

        readingInputs = true;
    }

    private void Update()
    {
        if (inputTimeout > 0)
        {
            inputTimeout -= Time.deltaTime;
            if (inputTimeout <= 0)
            {
                inputTimeout = 0;
                readingInputs = true;
            }
        }
    }

    public void OnTouchEvent(Vector2 data)
    {
        if (readingInputs)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(data);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                SandwichIngredientIstance ingredientInstance = hit.transform.GetComponent<SandwichIngredientIstance>();
                if (ingredientInstance != null)
                {
                    currentTouchedSection = ingredientInstance.currentSection;
                }
            }

#if UNITY_EDITOR
            if (printDebug)
            {
                Debug.Log("Touch on position: " + data.x + ", " + data.y);

                if (currentTouchedSection != null)
                {
                    string ingredientsStr = "";
                    for (int i = 0; i < currentTouchedSection.ingredientsStack.Count; i++)
                    {
                        ingredientsStr += currentTouchedSection.ingredientsStack[i].ingredientData.ingredientName + ",";
                    }
                    Debug.Log("Touched ingredients: " + ingredientsStr);
                }
            }
#endif
        }
    }

    public void OnSwipeEvent(Vector2 data)
    {
        if (readingInputs)
        {
            Vector2Int fromIndex = currentTouchedSection.sectionIndex;
            Vector2Int toIndex = new Vector2Int(fromIndex.x + (int)data.x, fromIndex.y + (int)data.y);
            gameGrid.TransferIngredients(fromIndex, toIndex);

            inputTimeout = timeBetweenMoves;
            readingInputs = false;

#if UNITY_EDITOR
            if (printDebug)
            {
                Debug.Log("Swipe on: " + toIndex.x + ", " + toIndex.y);
            }
#endif
        }
    }
}
