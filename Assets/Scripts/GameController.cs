using System.Collections;
using Blocks;
using Blocks.Databases;
using InputControllers;
using Michsky.UI.ModernUIPack;
using UI;
using UnityEngine;
using Grid = Blocks.Grid;

/// <summary>
/// GodClass that handle game turns and binds all thins together
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 2)]
    private float stepDelay = 0.75f;
    [SerializeField]
    [Range(0, 1)]
    private float speedUpCoef = 0.33f;
    [SerializeField]
    [Range(0, 5)]
    private float linesRemoveTime = 1f;
    [SerializeField]
    private AnimationCurve progression;

    [Space]
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private BlockRenderer blockRenderer;
    [SerializeField]
    private ProgressionController progressionController;

    [Space]
    [SerializeField]
    private BlockDatabaseSO blocksDatabase;
    [SerializeField]
    private ColorsDatabaseSO colorsDatabase;
    
    [Space]
    [SerializeField]
    private ModalWindowManager endGameWindow;

    private FlyingBlock currentBlock;
    private FlyingBlock nextBlock;

    private float initialStepTime;
    private float lastStepTime;

    private float currentStepDelay;

    private int shift;
    private bool isRemovingLines;
    private bool isEndGame;

    private void Awake()
    {
        initialStepTime = stepDelay;
        currentStepDelay = stepDelay;

        InputManager.OnSpeedUp += InputManagerOnSpeedUp;
    }

    private void InputManagerOnSpeedUp(bool isSpeedUp)
    {
        currentStepDelay = isSpeedUp ? stepDelay * speedUpCoef : stepDelay;
    }

    private void Update()
    {
        if(isEndGame)
            return;

        if (isRemovingLines)
        {
            lastStepTime += Time.deltaTime;
            return;
        }

        if (Time.time - lastStepTime > currentStepDelay)
        {
            ExecuteStep();
            lastStepTime = Time.time;
        }
    }

    private void ExecuteStep()
    {
        if (currentBlock != null)
            currentBlock.transform.localPosition -= new Vector3(0, 1, 0);

        if (currentBlock == null)
            CreateNewBlock();

        if (grid.CheckCollision(currentBlock.Position, currentBlock.BlockStruct, out var byBoundsHorizontal))
        {
            if(!byBoundsHorizontal)
                currentBlock.transform.localPosition += new Vector3(0, 1, 0);

            grid.UpdateGrid(currentBlock.Position, currentBlock.Blocks, currentBlock.BlockStruct, currentBlock.FadeMaterial, out var isOutOfBoundsTop);
            if (isOutOfBoundsTop)
            {
                EndGame();
                return;
            }

            Destroy(currentBlock.gameObject);
            currentBlock = null;

            var lines = grid.CheckLines();
            if (lines != null)
            {
                progressionController.AddScore(lines.Length);
                CalculateSpeedUp();
                StartCoroutine(RemoveLinesFromGrid(lines));
            }
        }
    }

    private void EndGame()
    {
        endGameWindow.OpenWindow();
        isEndGame = true;
    }

    private void CalculateSpeedUp()
    {
        stepDelay = initialStepTime * progression.Evaluate(Mathf.Clamp01(progressionController.CurrentLevel / 10f));
        currentStepDelay = stepDelay;
    }

    private IEnumerator RemoveLinesFromGrid(int[] linesToRemove)
    {
        isRemovingLines = true;

        foreach (var line in linesToRemove)
            grid.SwitchLineMaterial(linesToRemove);

        float startTime = Time.time;
        while (Time.time - startTime < linesRemoveTime)
        {
            colorsDatabase.SetAllTransparentAlpha(1 - (Time.time - startTime) / linesRemoveTime);
            yield return null;
        }

        grid.DestroyLines(linesToRemove);

        colorsDatabase.SetAllTransparentAlpha(1);
        isRemovingLines = false;
    }

    private void CreateNewBlock()
    {
        var materials = colorsDatabase.GetRandom();
        if(nextBlock == null)
            nextBlock = FlyingBlock.Constract(blocksDatabase.GetRandom(), materials.Item1, materials.Item2, grid);
        
        currentBlock = nextBlock;
        currentBlock.gameObject.SetActive(true);
        currentBlock.transform.SetParent(grid.transform);
        currentBlock.transform.localPosition = new Vector3(4, 22);

        materials = colorsDatabase.GetRandom();
        nextBlock = FlyingBlock.Constract(blocksDatabase.GetRandom(), materials.Item1, materials.Item2, grid);
        blockRenderer.CaptureBlock(nextBlock);
        nextBlock.gameObject.SetActive(false);
    }

#if UNITY_EDITOR

    [ContextMenu("AddLevel")]
    private void AddLevel()
    {
        progressionController.AddScore(3);
        CalculateSpeedUp();
    }

#endif
}
