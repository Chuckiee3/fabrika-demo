using System;

namespace _GAME_.Scripts.Actions
{
    public static class GameFlow
    {
        public static Action stageStarted;
        public static Action playerArrivedStageEnd;
        public static Action playerArrivedLevelStart;
        public static Action stagePassed;
        public static Action stageFailed;
        public static Action levelCreated;
        public static Action levelStarted;
        public static Action levelCompleted;
        public static Action levelFailed;
        public static Action itemCollected;
        public static Action playerArrivedLevelTransition;
        public static Action levelRestarted;
        public static void LevelRestarted()
        {
            levelRestarted?.Invoke();
        }
        public static void PlayerArrivedLevelTransition()
        {
            playerArrivedLevelTransition?.Invoke();
        }
        public static void PlayerArrivedStageEnd()
        {
            playerArrivedStageEnd?.Invoke();
        }
        public static void PlayerArrivedLevelStart()
        {
            playerArrivedLevelStart?.Invoke();
        }
        public static void StageStarted()
        {
            stageStarted?.Invoke();
        }
        public static void StagePassed()
        {
            stagePassed?.Invoke();
        }
        public static void StageFailed()
        {
            stageFailed?.Invoke();
        }
        public static void LevelCreated()
        {
            levelCreated?.Invoke();
        }
        public static void LevelStarted()
        {
            levelStarted?.Invoke();
        }
        public static void LevelCompleted()
        {
            levelCompleted?.Invoke();
        }
        public static void LevelFailed()
        {
            levelFailed?.Invoke();
        }

        public static void ItemCollected()
        {
            itemCollected?.Invoke();
        }
    }
}
