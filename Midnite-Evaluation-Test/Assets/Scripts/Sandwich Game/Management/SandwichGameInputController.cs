using UnityEngine;

public class SandwichGameInputController : MonoBehaviour
{
    public SandwichGameLevelManager levelManager;

    public float timeBetweenMoves = 1.0f;

    private SandwichGameGridSection currentTouchedSection;

    private bool readingInputs;
    private float inputTimeout;
    private bool inputBlocked;

#if UNITY_EDITOR
    [Space()]
    public bool printDebug;
#endif

    void Start()
    {
        readingInputs = true;
    }

    void Update()
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

    public void SetInputBlocked(bool blocked)
    {
        inputBlocked = blocked;
    }

    public void OnTouchEvent(Vector2 data)
    {
        if (readingInputs && !inputBlocked)
        {
            currentTouchedSection = GetPointedSection(data);

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
        if (readingInputs && !inputBlocked)
        {
            Vector2Int fromIndex = currentTouchedSection.sectionIndex;
            Vector2Int toIndex = new Vector2Int(fromIndex.x + (int)data.x, fromIndex.y + (int)data.y);
            levelManager.MakeMove(fromIndex, toIndex);

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

    private SandwichGameGridSection GetPointedSection(Vector2 screenSpacePoint)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenSpacePoint);
        //if (Physics.Raycast(ray, out hit, 100f))
        if (Physics.SphereCast(ray, 0.4f, out hit, 100f))
        {
            SandwichIngredientIstance ingredientInstance = hit.transform.GetComponent<SandwichIngredientIstance>();
            if (ingredientInstance != null)
            {
                return ingredientInstance.currentSection;
            }
        }

        return null;
    }
}