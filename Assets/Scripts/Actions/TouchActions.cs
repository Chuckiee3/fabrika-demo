using System;
using UnityEngine;

namespace _Reusable.Actions
{
    public static class TouchActions
    {
        //Tap Actions
        public static Action<Vector2> touchDown;
        public static Action<Vector2> touchUp;
        public static Action<Vector2> touchUpdate;
        //Tap and Hold actions
        public static Action tapAndHoldComplete;
        //Drag actions
        public static Action<Vector3> touchDragMovement;
        
        public static void TouchUp(Vector2 position)
        {
            touchUp?.Invoke(position);
        }
        public static void TouchDown(Vector2 position)
        {
            touchDown?.Invoke(position);
        }
        public static void TouchUpdate(Vector2 position)
        {
            touchUpdate?.Invoke(position);
        }

        public static void TapAndHoldCompleted()
        {
            tapAndHoldComplete?.Invoke();
        }

        public static void TouchDragMovement(Vector3 touchMovementDx)
        {
            touchDragMovement?.Invoke(touchMovementDx);
        }
    }
}
