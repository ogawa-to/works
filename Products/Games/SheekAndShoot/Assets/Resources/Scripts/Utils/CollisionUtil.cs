using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CollisionUtil
{
    // 円と円の衝突判定を行う。
    public static bool IsHitBetweenCircle(Vector3 pos1, float r1, Vector3 pos2, float r2)
    {
        // x*x + y*y <= r*rの場合
        if ((pos2 - pos1).sqrMagnitude <= Math.Pow((r1 + r2), 2))
        {
            return true;
        }
        return false;
    }

    // タンケイの衝突判定
    public static bool IsInnerBox(Vector3 point, Vector3 box, Rect rect)
    {

        float width = rect.xMax - rect.xMin;
        float height = rect.yMax - rect.yMin; 
        if ((point.x > box.x - width)
            && (point.x < box.x + width)
            && (point.y > box.y - height)
            && (point.y < box.y + height)
            )
        {
            return true;
        }
        return false;
    }
}
