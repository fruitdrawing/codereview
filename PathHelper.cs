using Pathfinding;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class PathHelper : MonoBehaviour
    {
        
        public AIPath AIPath;
        
        
        public Transform CurrentFinalDestination;
        public Transform CurrentFollowingTarget;
        
        private void Awake()
        {
            
        }

        private void FollowTarget(Transform target)
        {
            CurrentFollowingTarget = target;
        }
    }
}