using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SoundDataSO), true)]
public class SoundDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (SoundDataSO)target;

        if (GUILayout.Button("Refreash", GUILayout.Height(40)))
            script.InitData();
        GUILayout.Space(60);
    }
#endif
}

