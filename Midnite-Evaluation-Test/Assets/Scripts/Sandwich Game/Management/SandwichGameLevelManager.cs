using UnityEngine;

public class SandwichGameLevelManager : MonoBehaviour
{
    public SandwichData ingredientsData;
    public SandwichGameLevelGenerator levelGenerator;
    public int gridSize = 4;
    public float ingredientsVerticalOffset = 0.2f;
    public SandwichGameInputController inputController;
    public Transform elementsRoot;

    [Space()]
    public GameEvent levelStartedEvent;
    public GameEvent levelWonEvent;

#if UNITY_EDITOR
    [Space()]
    public bool printDebug;
#endif

    private SandwichGameGrid gameGrid;
    private SandwichGameGridSection currentTouchedSection;
    private SandwichLevelData levelData;

    private bool levelEnded;

    private void Start()
    {
        NewLevel();
    }

    public void NewLevel()
    {
        SetInputBlocked(true);

        ClearLevel();

        if (levelGenerator != null)
        {
            levelData = levelGenerator.GenerateLevelData(ingredientsData, gridSize);
        }

        gameGrid = new SandwichGameGrid(gridSize, ingredientsVerticalOffset, elementsRoot);
        gameGrid.InitWithData(levelData);

        levelEnded = false;

        levelStartedEvent.Raise();

        SetInputBlocked(false);
    }

    public void RestartLevel()
    {
        if (levelEnded)
        {
            return;
        }

        SetInputBlocked(true);

        ClearLevel();

        gameGrid.InitWithData(levelData);

        SetInputBlocked(false);
    }

    private void ClearLevel()
    {
        if (elementsRoot != null)
        {
            for (int i = 0; i < elementsRoot.childCount; i++)
            {
                GameObject.Destroy(elementsRoot.GetChild(i).gameObject);
            }
        }
        else
        {
            SandwichIngredientIstance[] toDestroyObjs = GameObject.FindObjectsOfType<SandwichIngredientIstance>();
            for (int i=0; i< toDestroyObjs.Length; i++)
            {
                GameObject.Destroy(toDestroyObjs[i].gameObject);
            }
        }
    }

    public void MakeMove(Vector2Int fromIndex, Vector2Int toIndex)
    {
        gameGrid.TransferIngredients(fromIndex, toIndex);

        if (gameGrid.CheckGridCompleted())
        {
            SetInputBlocked(true);
            levelEnded = true;

            levelWonEvent.Raise();
        }
    }

    private void SetInputBlocked(bool blocked)
    {
        if (inputController != null)
        {
            inputController.SetInputBlocked(blocked);
        }
    }
}
