using Shapes;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    [ExecuteAlways]
    public class MyShapeTester : ImmediateModeShapeDrawer
    {
        public bool Available;
        
        public override void DrawShapes(Camera cam)
        {
            using (Draw.Command(cam))
            {
                if (Available == false) return;
                // set up static parameters. these are used for all following Draw.Line calls
                Draw.LineGeometry = LineGeometry.Volumetric3D;
                Draw.ThicknessSpace = ThicknessSpace.Pixels;
                Draw.Thickness = 4; // 4px wide

                // set static parameter to draw in the local space of this object
                Draw.Matrix = transform.localToWorldMatrix;

                // using( var p = new PolylinePath() ){
                //     p.BezierTo(new Vector3(-2,1,0), new Vector3(2,2,0), new Vector3(-2,1,0));
                //     p.AddPoint( -2, 0 );
                //     p.AddPoint( 2,  1 );
                //     Draw.Polyline( p, closed:true, thickness:0.1f, Color.red ); // Drawing happens here
                // }
                
            }

        }
    }
}