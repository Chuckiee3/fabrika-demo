using System.Collections;
using _GAME_.Scripts.Actions;
using _Reusable.Actions;
using UnityEngine;

public class TouchEventManager : MonoBehaviour
{
    public bool _touchedDown;
    [SerializeField] private bool _interactionEnabled;
    
    private Timer _interactionDelayTimer;

    public int touchCount;
    private void Awake()
    {
        _interactionDelayTimer = new Timer(.25f);
        _interactionDelayTimer.TimerActive = false;
        _interactionDelayTimer.onTimerEnd = EnableInteraction;
        EnableInteraction();
    }

    private void EnableInteraction()
    {
        _interactionEnabled = true;
    }

    public void TouchDown()
    {
        if(!_interactionEnabled) return;
        if (touchCount == 0)
        {
            touchCount++;
            StartCoroutine(StartLevelWithDelay());
        }
        _touchedDown = true;
        TouchActions.TouchDown(Input.mousePosition);
    }

    private IEnumerator StartLevelWithDelay()
    {
        yield return new WaitForSecondsRealtime(.5f);
        GameFlow.LevelStarted();
        GameFlow.StageStarted();
    }

    private void Update()
    {
        if (!_interactionEnabled)
        {
            if (_interactionDelayTimer.TimerActive)
            {
                _interactionDelayTimer.Tick(Time.deltaTime);
            }
            return;
        }
        if(!_touchedDown) return;
    }

    public void TouchUp()
    {
        if(!_interactionEnabled) return;
        if(!_touchedDown) return;
        _touchedDown = false;
        TouchActions.TouchUp(Input.mousePosition);
    }

    private void StartEnableInteractionTimer()
    {
        _interactionDelayTimer.ResetTimer();
    }
    
    private void DisableInteraction()
    {
        _interactionEnabled = false;
    }
    private void ResetTouchCount()
    {
        touchCount = 0;
    }
    private void OnEnable()
    {
        GameFlow.levelCreated += StartEnableInteractionTimer;
        GameFlow.playerArrivedLevelTransition += ResetTouchCount;
        GameFlow.levelRestarted += ResetTouchCount;
    }

   

    private void OnDisable()
    {
        GameFlow.levelCreated -= StartEnableInteractionTimer;
        GameFlow.playerArrivedLevelTransition -= ResetTouchCount;
        GameFlow.levelRestarted -= ResetTouchCount;
    }

}
