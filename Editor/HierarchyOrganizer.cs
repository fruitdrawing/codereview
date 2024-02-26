using UnityEditor;
using UnityEngine;

public static class HierarchyOrganizer
{
    // Expand recursively and collapse any inactive GameObjects in
    // selection and children.
    [MenuItem("GameObject/Expand Active Children", false, 46)]
    static void Menu_ExpandActiveChildren()
    {
        EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
        var hierarchy_window = EditorWindow.focusedWindow;

        // Use transforms so it only works for hierarchy view.
        foreach (var t in Selection.transforms)
        {
            ExpandActiveChildren(t.gameObject, hierarchy_window);
        }
    }

    static void ExpandActiveChildren(GameObject parent, EditorWindow hierarchy_window)
    {
        SetExpandedRecursive(parent, true, hierarchy_window);
        for (int i = 0; i < parent.transform.childCount; ++i)
        {
            var t = parent.transform.GetChild(i);
            var obj = t.gameObject;
            bool is_active = t.gameObject.activeSelf;
            SetExpandedRecursive(obj, is_active, hierarchy_window);
            //~ Debug.Log(string.Format("Setting hierarchy for {0} expanding={1}", obj.name, is_active), obj);
            if (is_active)
            {
                ExpandActiveChildren(obj, hierarchy_window);
            }
        }
    }

    // https://answers.unity.com/questions/656869/foldunfold-gameobject-from-code.html
    static void SetExpandedRecursive(GameObject go, bool expand, EditorWindow hierarchy_window)
    {
        var type = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        // There's no exposed nonrecursive function in 2018, so call the
        // recursive one instead.
        var methodInfo = type.GetMethod("SetExpandedRecursive");
        Debug.Assert(methodInfo != null, "Failed to find SetExpandedRecursive. Maybe it changed in this version of Unity.");
        methodInfo.Invoke(hierarchy_window, new object[] { go.GetInstanceID (), expand });
    }
}