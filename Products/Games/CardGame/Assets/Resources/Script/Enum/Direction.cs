using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 方向
public enum Direction
{
    UP = 0
    , DOWN = 1
    , LEFT = 2
    , RIGHT = 3
    , NUM = 4
}

// 方向に関するUtilityクラス
public class DirectionUtil
{
    public static Direction getReverse(Direction d)
    {
        switch(d)
        {
            case Direction.UP:
                return Direction.DOWN;
            case Direction.DOWN:
                return Direction.UP;
            case Direction.LEFT:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.LEFT;
        }
        throw new System.ArgumentException("");
    }
}