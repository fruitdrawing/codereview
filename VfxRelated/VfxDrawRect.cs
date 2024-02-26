using Shapes;
using SharedData;
using UnityEngine;
using UtilClasses;

namespace DefaultNamespace.MyScripts.VfxRelated
{
    [ExecuteAlways]
    public class VfxDrawRect : ImmediateModeShapeDrawer
    {
        public bool OnOff;
        public Vector3 currentPosition;
        public Vector2 Size;
        
        public override void DrawShapes( Camera cam ){

            using( Draw.Command( cam ) )
            {

                if (OnOff == false) return;

                // set up static parameters. these are used for all following Draw.Line calls
                Draw.LineGeometry = LineGeometry.Volumetric3D;
                Draw.ThicknessSpace = ThicknessSpace.Pixels;
                Draw.Thickness = 4; // 4px wide

                // set static parameter to draw in the local space of this object
                Draw.Matrix = transform.localToWorldMatrix;

                // draw lines
                var rect = MyUtilManager.CalculateOriginOfPivot(PivotType.Center, Size, currentPosition);
                Debug.Log($"<color=green>Rect : {rect}</color>");
                // rect.center = currentPosition;
                Draw.Rectangle(currentPosition,rect);
                
            }

        }

        public void TurnOn(Vector3 position)
        {
            currentPosition = position;
            OnOff = true;
        } 
        
        public void TurnOff()
        {
            OnOff = false;
        }
    }
}