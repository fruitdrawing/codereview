using SharedData;
using UnityEngine;

namespace UtilClasses
{
    public static class MyUtilManager
    {
        public static Rect CalculateOriginOfPivot
            (PivotType pivotType,Vector2 v2Size,Vector2 origin)
        {
            Rect rect = new();
            Vector2 offset = Vector2.zero;
            
            switch (pivotType)
            {
                case PivotType.TopLeft:
                    offset = new Vector2(v2Size.x / 2, -v2Size.y / 2);
                    break;
                case PivotType.TopCenter:
                    offset = new Vector2(0, -v2Size.y / 2);
                    break;
                case PivotType.TopRight:
                    offset = new Vector3(-v2Size.x / 2, -v2Size.y / 2);
                    break;
                case PivotType.MiddleLeft:
                    offset = new Vector3(v2Size.x/2, 0);
                    break;
                case PivotType.Center:
                    rect.center = origin;
                    // offset = new Vector3(-boxSize.x / 2, boxSize.y / 2, 0);
                    break;
                case PivotType.MiddleRight:
                    offset = new Vector3(-v2Size.x/2, 0);
                    break;
                case PivotType.BottomLeft:
                    offset = new Vector3(v2Size.x / 2, v2Size.y / 2, 0);
                    break;
                case PivotType.BottomCenter:
                    offset = new Vector3(0, v2Size.y / 2, 0);
                    break;
                case PivotType.BottomRight:
                    offset = new Vector3(-v2Size.x / 2, v2Size.y / 2, 0);
                    break;
            }
            // rect.center = pivot + offset;
            var newPos = origin + offset;

            // rect.xMin = newPos.x;
            rect.Set(newPos.x, newPos.y,v2Size.x,v2Size.y);

            if (pivotType == PivotType.Center)
            {
                rect.center = origin;
            }
            return rect;
        } 
        // public LayerMask GetLayerMaskBy
    }
}