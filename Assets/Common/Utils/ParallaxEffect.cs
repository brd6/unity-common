using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [System.Serializable]
    public class ParallaxEffectElement
    {
        public GameObject gameObject;
        public Direction direction;
        public float speed;
    }

    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Enable automatic speed calculation for the elements")]
        private bool autoSpeed;

        [SerializeField]
        [Tooltip("Enable automatic direction set for the elements")]
        private bool autoDirection;

        [SerializeField]
        [Tooltip("Use only when speed is set as automatic")]
        private float baseSpeed;

        [SerializeField]
        private float baseSpeedDiviser;

        [SerializeField]
        [Tooltip("Use only when direction is set as automatic")]
        private Direction baseDirection;

        [SerializeField]
        private List<ParallaxEffectElement> elements;

        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private Transform endPosition;

        float elementSpeed;

        private void Start()
        {
            elementSpeed = baseSpeed;
            ComputeElementsAttributs();
        }

        private void ComputeElementsAttributs()
        {
            foreach (var element in elements)
            {
                ComputeAutoDirection(element);
                ComputeAutoSpeed(element);

                element.gameObject.transform.position = startPosition.position;
            }
        }

        private void ComputeAutoDirection(ParallaxEffectElement element)
        {
            if (!autoDirection)
                return;

            element.direction = baseDirection;
        }

        private void ComputeAutoSpeed(ParallaxEffectElement element)
        {
            if (!autoSpeed)
                return;

            element.speed = elementSpeed;

            if (Mathf.Sign(elementSpeed) > 0)
                elementSpeed *= baseSpeedDiviser;
            else
                elementSpeed = 0;
        }

        private void FixedUpdate()
        {
            foreach (var element in elements)
            {
                MoveElement(element);
            }
        }

        private void MoveElement(ParallaxEffectElement element)
        {
            var vector = Util.DirectionToVector(element.direction);

            element.gameObject.transform.Translate(vector * element.speed * Time.deltaTime);

            if (ReachEndPosition(element))
                element.gameObject.transform.position = startPosition.position;
        }

        private bool ReachEndPosition(ParallaxEffectElement element)
        {
            switch (element.direction)
            {
                case Direction.LEFT:
                    return element.gameObject.transform.position.x < endPosition.position.x;
                case Direction.RIGHT:
                    return element.gameObject.transform.position.x > endPosition.position.x;
                case Direction.TOP:
                    return element.gameObject.transform.position.y > endPosition.position.y;
                case Direction.BOTTOM:
                    return element.gameObject.transform.position.y < endPosition.position.y;
                default:
                    break;
            }
            return false;
        }
    }
}