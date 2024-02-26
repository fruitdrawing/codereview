using System;
using NaughtyAttributes;
using Pathfinding;
using UnityEngine;

namespace CharacterUnitRelated.MoveRelated
{
    public class AIPathHelper : MonoBehaviour
    {
        public AIPath path;

        [Button]
        public void DOSOMETHING()
        {
            var boo = path.hasPath;
            Debug.Log($"<color=white>{path.hasPath}</color>",path);
        }
        private void Awake()
        {
            
        }
    }
}