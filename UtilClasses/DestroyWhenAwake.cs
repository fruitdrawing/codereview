using UnityEngine;

namespace UtilClasses
{
    public class DestroyWhenAwake : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(this.gameObject);
        }
    }
}