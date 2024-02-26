using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.MyScripts.SharedData;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace.MyScripts.UtilClasses
{
    public static class MyExtensionManager
    {
        public static Vector3 GetRandomPositionInsideCollider(this Collider2D col)
        {
            var bounds = col.bounds;
            return new Vector3(
                UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                UnityEngine.Random.Range(bounds.min.z, bounds.max.z));
            
        }
        public static CharacterDirection GetOppositeDirection(this CharacterDirection input)
        {
            if (input == CharacterDirection.Left)
            {
                return CharacterDirection.Right;
            }
            else if (input == CharacterDirection.Right)
            {
                return CharacterDirection.Left;
            }

            return input;
        }
        
     
          // * 리스트들이 충족됐는지 확인하는 메쏘드
        public static void Log(this UnityEngine.Object unityObject, string message)
        {
            Debug.Log($"{unityObject.name} : {message}");
        }
        //
        // public static Ease ConvertToDoTweenEase(this MyEase myEase)
        // {
        //     Ease newEase = (Ease)myEase;
        //     // .ToString().ConvertToEnum<Ease>();
        //     return newEase;
        // }
        // public static Color ConvertDamageTextColor(this DamageTextColor damageTextColor)  
        // {
        //     return damageTextColor switch
        //     {
        //         DamageTextColor.White => Color.white,
        //         DamageTextColor.Red => Color.red,
        //         DamageTextColor.Yellow => Color.yellow,
        //         DamageTextColor.Green=> Color.green,
        //         DamageTextColor.Blue=> Color.blue,
        //         _ => throw new ArgumentOutOfRangeException(nameof(damageTextColor), 
        //             damageTextColor, 
        //             null)
        //     };
        // }
          
        public static bool IsValidIndex<T>(this List<T> list, int index)
        {
            return index >= 0 && index < list.Count;
        }

        public static UnitTeam GetOppositeTeam(this UnitTeam team)
        {
            if (team == UnitTeam.EnemyTeam) return UnitTeam.PlayerTeam;
            else if (team == UnitTeam.PlayerTeam) return UnitTeam.EnemyTeam;
            return team;
            
        }

        public static Vector3 GenerateRandomPointInPolygon(this PolygonCollider2D polyCollider)
        {
            
            // Get the bounds of the polygon
            Bounds bounds = polyCollider.bounds;

            // Generate random x and y values within the bounds of the polygon
            float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

            // Create a point with the random x and y values
            Vector2 point = new Vector2(x, y);

            // If the point is inside the polygon, return it
            if (polyCollider.OverlapPoint(point))
            {
                return point;
            }

            Debug.Log($"<color=red>ERROR</color>");
            return point;
        }

        public static bool CompareAnyTags(this Collider2D collider2d,string[] tagNames)
        {
            bool output = false;
            
            foreach (var VARIABLE in tagNames)
            {
                output = collider2d.CompareTag(VARIABLE);
                if (output == true)
                {
                    return true;
                }
            }
            return false;
        }
        public static T RandomEnumValue<T>()
        {
            var values = Enum.GetValues(typeof(T));
            int random = UnityEngine.Random.Range(0, values.Length);
            return (T)values.GetValue(random);
        }

        public static List<T> GetAllEnumList<T>(this T t)
        {
            var values = Enum.GetValues(typeof(T));
            return values.Cast<T>().ToList();
        }
        
        
        public static void DestroyAllChildren(this Transform transform)
        {
            foreach (Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }
        }

        public static void OnOffCanvasGroup(this CanvasGroup canvasGroup,bool onoff)
        {
            canvasGroup.alpha = onoff ? 1 : 0;
            canvasGroup.interactable = onoff;
            canvasGroup.blocksRaycasts = onoff;
        }

        public static void Shift<T>(this IList<T> list, int fromIndex, int toIndex)
        {
            if (toIndex == fromIndex)
                return;
            int index = fromIndex;
            T obj = list[fromIndex];
            while (index > toIndex)
            {
                --index;
                list[index + 1] = list[index];
                list[index] = obj;
            }
            while (index < toIndex)
            {
                ++index;
                list[index - 1] = list[index];
                list[index] = obj;
            }
        }

        
        
        public static void ShuffleList<T>(this IList<T> list)
        {
            // if (ListExtensions._rng == null)
                // ListExtensions._rng = new Random();
            int count = list.Count;
            while (count > 1)
            {
                --count;
                int index = UnityEngine.Random.Range(0,count + 1);
                T obj = list.ElementAt(index);
                
                list[index] = list.ElementAt(count);
                list[count] = obj;
            }
        }
        
        public static IEnumerable<T> ShuffleEnumerable<T>(this IEnumerable<T> list, int size)
        {
            var r = new Random();
            var shuffledList = 
                list.
                    Select(x => new { Number = r.Next(), Item = x }).
                    OrderBy(x => x.Number).
                    Select(x => x.Item).
                    Take(size); // Assume first @size items is fine
            // list = shuffledList;
            list = shuffledList;
            return list;
            
        }
        
        
        
        public static T GetRandomFromList<T>(this List<T> list)
        {
            if (list.Count == 0) return default!;
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        
        public static T GetRandomFromEnumerable<T>(this IEnumerable<T> _enumerable)
        {
            if (_enumerable.Count() == 0) return default!;
            return _enumerable.ElementAt(UnityEngine.Random.Range(0, _enumerable.Count()));
        }

        public static List<T> GetRandomFromList<T>(this List<T> list,int amount)
        {
            if (list.Count == 0) return default!;
            List<T> result = new();
            
            for (int i = 0; i < amount; i++)
            {
                var a = list.Except(result).ToList().GetRandomFromList();
                result.Add(a);
            }

            return result;
        }
        public static GameObject GetChildGameObjectByName(this GameObject go, string childGameObjectName)
        {
            Transform[] children = go.transform.GetComponentsInChildren<Transform>();
            foreach (var t in children)
            {
                if (t.gameObject.name == childGameObjectName)
                {
                    return t.gameObject;
                }
            }

            Debug.Log($"<color=red>ERRROROROROROR</color>");
            return go;
            

        }

        public static float GetFloatVariableByString(this MonoBehaviour mono,string nameOfVariable)
        {
            float output =(float)mono.GetType().GetField(nameOfVariable).GetValue(mono);
            return output;
        }
        
     
        
        
        /// <summary>
        /// 이거로 특정 인터페이스를 실행하는 클래스를 잡을 수 있음.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetAllObjectsImplementingInterface<T>()
        {
            Type[] allTypes = System.Reflection.Assembly.GetAssembly(typeof(T)).GetTypes();
            List<T> output = new();
            foreach (Type type in allTypes)
            {
                if (type.IsClass && type.GetInterfaces().Contains(typeof(T)))
                {
                    // type is a class that implements MyInterface
                    // Debug.Log($"<color=cyan>{type.Name}</color>");
                    output.Add((T)Activator.CreateInstance(type));
                }
            }
            
            return output;
        } 
     
        
        

        public static List<Type> GetAllInheritedClasses<T>()
        {
            Type[] allTypes = System.Reflection.Assembly.GetAssembly(typeof(T)).GetTypes();
            List<Type> output = new();
            foreach (Type type in allTypes)
            {
                if (type.IsClass && type.IsSubclassOf(typeof(T)))
                {
                    // type is a class that implements MyInterface
                    // Debug.Log($"<color=cyan>{type.Name}</color>");
                    output.Add(type);
                }
            }
            
            return output;
        }
    }
}