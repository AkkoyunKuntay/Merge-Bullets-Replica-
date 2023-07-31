using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CanvasTypes { merge, game, win, fail }
public class CanvasManager : MonoBehaviour
{
    [SerializeField] CanvasVisibilityController MergeCanvas;
    [SerializeField] CanvasVisibilityController GameCanvas;
    [SerializeField] CanvasVisibilityController WinCanvas;
    [SerializeField] CanvasVisibilityController FailCanvas;


    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Debug")]
    [SerializeField] CanvasVisibilityController activeCanvas;

    private void Start()
    {

        scoreText.text = "Money : " +  ResourceManager.instance.GetResourceAmount().ToString();


        LevelManager.instance.BlockRaceIsOnEvent += OnBlockRaceBegin;
        GameManager.instance.LevelFailedEvent += OnLevelFailed;
        GameManager.instance.LevelSuccessEvent += OnLevelSuccessfull;
        ResourceManager.instance.ResourceChangedEvent += OnResourceChanged;
    }

    private void OnResourceChanged()
    {
        scoreText.text = "Money : " + ResourceManager.instance.GetResourceAmount().ToString();
    }

    private void OnBlockRaceBegin()
    {
        SetActiveCanvas(CanvasTypes.game);
    }

    private void OnLevelSuccessfull()
    {
        SetActiveCanvas(CanvasTypes.win);
    }

    private void OnLevelFailed()
    {
        SetActiveCanvas(CanvasTypes.fail);
    }

    

    private void SetActiveCanvas(CanvasTypes type)
    {
        activeCanvas.Hide();
        switch (type)
        {
            case CanvasTypes.merge:
                activeCanvas = MergeCanvas;
                break;
            case CanvasTypes.game:
                activeCanvas = GameCanvas;
                break;
            case CanvasTypes.win:
                activeCanvas = WinCanvas;
                break;
            case CanvasTypes.fail:
                activeCanvas = FailCanvas;
                break;
            default:
                break;
        }
        activeCanvas.Show();
    }

}
