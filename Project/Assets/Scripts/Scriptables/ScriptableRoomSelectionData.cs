#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableRoomSelectionData", menuName = "Scriptable Objects/ScriptableRoomSelectionData")]
public class ScriptableRoomSelectionData : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public string SceneName;

    public bool model;
    public bool sound;
    public bool components;
}

#if UNITY_EDITOR
[CustomEditor(typeof(ScriptableRoomSelectionData))]
public class ScriptableRoomSelectionDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ScriptableRoomSelectionData source = target as ScriptableRoomSelectionData;

        if (!EditorApplication.isPlaying && GUILayout.Button("Open Scene"))
        {
            EditorSceneManager.OpenScene($"Assets/Scenes/{source.SceneName}.unity", OpenSceneMode.Single);
        }
    }
}
#endif
