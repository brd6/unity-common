using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SideTouchInput : MonoBehaviour
    {
        [SerializeField]
        private bool isEnable = false;

        [SerializeField]
        private bool useScreenInput = true;

        [SerializeField]
        private GameObject screenInputGroup;

        public delegate void OnSideTouchDelegate(Direction direction);
        public static event OnSideTouchDelegate OnSideTouchEvent;

        void Start()
        {
            if (screenInputGroup != null)
                screenInputGroup.SetActive(false);

            Input.multiTouchEnabled = true;
        }

        private void FixedUpdate()
        {
            if (!isEnable)
                return;

            if (!useScreenInput)
                SideTouchHandle();
        }

        public void SetState(bool isEnable)
        {
            this.isEnable = isEnable;

            if (this.isEnable && 
                useScreenInput)
                screenInputGroup.SetActive(isEnable);
        }

        private void SideTouchHandle()
        {
            if (Input.touchCount < 1)
                return;

            foreach (Touch touch in Input.touches)
            {
                Debug.LogWarning(touch.fingerId);
                if (touch.phase == TouchPhase.Began)
                {
                    CheckScreenTouch(touch.position);
                }
                else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                {
                    Debug.LogError("Touch Cancel!");
                    //
                }
            }
        }

        #region Button Action (Use by ScreenInputGroup's buttons)
        public void TouchLeftScreenAction()
        {
            InkoveOnSideTouchEvent(Direction.LEFT);
        }

        public void TouchRightScreenAction()
        {
            InkoveOnSideTouchEvent(Direction.RIGHT);
        }
        #endregion

        private void CheckScreenTouch(Vector3 position)
        {
            Direction dir = GetSideDirection(position);
            if (dir != Direction.NONE)
                InkoveOnSideTouchEvent(dir);
        }

        private void InkoveOnSideTouchEvent(Direction direction)
        {
            if (OnSideTouchEvent != null)
                OnSideTouchEvent(direction);
        }

        private Direction GetSideDirection(Vector3 position)
        {
            Vector3 touchScreenPosition = Camera.main.ScreenToWorldPoint(position);

            if (touchScreenPosition.x >= 0)
                return Direction.RIGHT;
            else if (touchScreenPosition.x <= 0)
                return Direction.LEFT;
            return Direction.NONE;
        }
    }
}