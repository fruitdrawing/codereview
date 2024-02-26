using MyDomain.Utils;
using MyDomain.Utils.MyExtensionMethods;
using UniRx;
using UnityEngine;

namespace SharedData
{
    [CreateAssetMenu(fileName = "GraphicDB", menuName = "DB/GraphicDB", order = 0)]
    public class GraphicDB : SingletonScriptableObject<GraphicDB>
    {
        public GenericDictionary<MyColor, ReactiveProperty<Color>> ColorDictionary = new();
        
    }

    public enum MyColor
    {
        EnemyHP,
        EnemyHpBG,
        FriendlyHP,
        FridenlyHPBG,
        
        
    }
}