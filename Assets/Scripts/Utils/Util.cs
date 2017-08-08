using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Util
    {
        public static Vector3 DirectionToVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.LEFT:
                    return Vector3.left;
                case Direction.RIGHT:
                    return Vector3.right;
                case Direction.TOP:
                    return Vector3.up;
                case Direction.BOTTOM:
                    return Vector3.down;
                default:
                    break;
            }
            return Vector3.zero;
        }
    }
}