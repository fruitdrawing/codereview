using NaughtyAttributes;
using Shapes;
using UnityEngine;

namespace DefaultNamespace.MyScripts.GraphicRelated
{
    public class DrawLineManager : MonoBehaviour
    {
        [Button]
        public void TEST()
        {
            var go = new GameObject();
            var line = go.AddComponent<Line>();
            line.Thickness = 0.2f;
            line.Start = Vector3.zero;
            line.End = Vector2.one;
            line.Color = Color.red;
            
        }

        [Button]
        public void DrawRectangluar()
        {
            
        }
    }
}