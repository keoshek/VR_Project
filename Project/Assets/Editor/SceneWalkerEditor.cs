using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

// Place this file inside any folder named "Editor" in your project.
// Open via: Tools > Scene Walker
public class SceneWalkerEditor : EditorWindow
{
    // ── Scene list ────────────────────────────────────────────────────────────
    [System.Serializable]
    private class SceneEntry
    {
        public SceneAsset sceneAsset;
        public bool foldout = true;
        public List<ObjectOverride> overrides = new List<ObjectOverride>();
    }

    // ── Per-object override ───────────────────────────────────────────────────
    [System.Serializable]
    private class ObjectOverride
    {
        public string objectName = "";          // name to search for in scene
        public bool overridePosition = false;
        public Vector3 position = Vector3.zero;
        public bool overrideRotation = false;
        public Vector3 rotation = Vector3.zero; // Euler angles
        public bool overrideScale = false;
        public Vector3 scale = Vector3.one;
    }

    // ── State ─────────────────────────────────────────────────────────────────
    private List<SceneEntry> _scenes = new List<SceneEntry>();
    private int _currentIndex = -1;
    private bool _autoApply = true;     // apply overrides right after loading
    private Vector2 _scroll;

    private const string WindowTitle = "Scene Walker";

    [MenuItem("Tools/Scene Walker")]
    public static void Open() =>
        GetWindow<SceneWalkerEditor>(WindowTitle);

    // ─────────────────────────────────────────────────────────────────────────
    private void OnGUI()
    {
        EditorGUILayout.Space(4);
        DrawToolbar();
        EditorGUILayout.Space(4);

        _scroll = EditorGUILayout.BeginScrollView(_scroll);
        DrawSceneList();
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space(4);
        DrawNavigationBar();
        EditorGUILayout.Space(4);
    }

    // ── Toolbar ───────────────────────────────────────────────────────────────
    private void DrawToolbar()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

        if (GUILayout.Button("+ Add Scene", EditorStyles.toolbarButton, GUILayout.Width(90)))
            _scenes.Add(new SceneEntry());

        GUILayout.FlexibleSpace();

        _autoApply = GUILayout.Toggle(_autoApply, " Auto-Apply on Load",
                                      EditorStyles.toolbarButton);

        EditorGUILayout.EndHorizontal();
    }

    // ── Scene list ────────────────────────────────────────────────────────────
    private void DrawSceneList()
    {
        for (int i = 0; i < _scenes.Count; i++)
        {
            SceneEntry entry = _scenes[i];
            bool isCurrent = (i == _currentIndex);

            // Scene row header
            GUI.backgroundColor = isCurrent
                ? new Color(0.4f, 0.8f, 0.4f)
                : Color.white;

            EditorGUILayout.BeginVertical("box");
            GUI.backgroundColor = Color.white;

            // ── Header ────────────────────────────────────────────────────────
            EditorGUILayout.BeginHorizontal();

            entry.foldout = EditorGUILayout.Foldout(entry.foldout,
                $"[{i}]  {(entry.sceneAsset != null ? entry.sceneAsset.name : "<no scene>")}",
                true, EditorStyles.foldoutHeader);

            GUILayout.FlexibleSpace();

            if (isCurrent)
            {
                GUIStyle badge = new GUIStyle(EditorStyles.miniLabel);
                badge.normal.textColor = new Color(0.2f, 0.6f, 0.2f);
                GUILayout.Label("● ACTIVE", badge);
            }

            // Load button
            if (GUILayout.Button("Load", GUILayout.Width(50)))
                LoadScene(i);

            // Apply button (even without loading again)
            GUI.enabled = isCurrent;
            if (GUILayout.Button("Apply", GUILayout.Width(50)))
                ApplyOverrides(entry);
            GUI.enabled = true;

            // Remove scene
            GUI.backgroundColor = new Color(1f, 0.4f, 0.4f);
            if (GUILayout.Button("✕", GUILayout.Width(22)))
            {
                _scenes.RemoveAt(i);
                if (_currentIndex >= _scenes.Count) _currentIndex = _scenes.Count - 1;
                GUIUtility.ExitGUI();
                return;
            }
            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndHorizontal();

            // ── Body ──────────────────────────────────────────────────────────
            if (entry.foldout)
            {
                EditorGUI.indentLevel++;

                entry.sceneAsset = (SceneAsset)EditorGUILayout.ObjectField(
                    "Scene", entry.sceneAsset, typeof(SceneAsset), false);

                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("GameObject Overrides", EditorStyles.boldLabel);

                DrawOverrideList(entry);

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
        }
    }

    // ── Override list for one scene ───────────────────────────────────────────
    private void DrawOverrideList(SceneEntry entry)
    {
        for (int j = 0; j < entry.overrides.Count; j++)
        {
            ObjectOverride ov = entry.overrides[j];

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField($"Object {j + 1}", EditorStyles.miniBoldLabel);
            GUILayout.FlexibleSpace();

            GUI.backgroundColor = new Color(1f, 0.5f, 0.5f);
            if (GUILayout.Button("✕", GUILayout.Width(20)))
            {
                entry.overrides.RemoveAt(j);
                GUI.backgroundColor = Color.white;
                GUIUtility.ExitGUI();
                return;
            }
            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndHorizontal();

            // Name to search
            ov.objectName = EditorGUILayout.TextField("Search Name", ov.objectName);

            // Position
            EditorGUILayout.BeginHorizontal();
            ov.overridePosition = EditorGUILayout.Toggle(ov.overridePosition, GUILayout.Width(14));
            EditorGUI.BeginDisabledGroup(!ov.overridePosition);
            ov.position = EditorGUILayout.Vector3Field("Position", ov.position);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            // Rotation
            EditorGUILayout.BeginHorizontal();
            ov.overrideRotation = EditorGUILayout.Toggle(ov.overrideRotation, GUILayout.Width(14));
            EditorGUI.BeginDisabledGroup(!ov.overrideRotation);
            ov.rotation = EditorGUILayout.Vector3Field("Rotation", ov.rotation);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            // Scale
            EditorGUILayout.BeginHorizontal();
            ov.overrideScale = EditorGUILayout.Toggle(ov.overrideScale, GUILayout.Width(14));
            EditorGUI.BeginDisabledGroup(!ov.overrideScale);
            ov.scale = EditorGUILayout.Vector3Field("Scale", ov.scale);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(1);
        }

        EditorGUI.indentLevel--;
        if (GUILayout.Button("+ Add Object Override", GUILayout.Height(22)))
            entry.overrides.Add(new ObjectOverride());
        EditorGUI.indentLevel++;
    }

    // ── Navigation bar ────────────────────────────────────────────────────────
    private void DrawNavigationBar()
    {
        if (_scenes.Count == 0) return;

        EditorGUILayout.BeginHorizontal();

        GUI.enabled = _currentIndex > 0;
        if (GUILayout.Button("◀  Previous", GUILayout.Height(30)))
            LoadScene(_currentIndex - 1);

        GUI.enabled = true;
        GUILayout.Label(
            _currentIndex >= 0
                ? $"Scene  {_currentIndex + 1} / {_scenes.Count}"
                : "— not loaded —",
            new GUIStyle(EditorStyles.centeredGreyMiniLabel) { fontSize = 12 },
            GUILayout.ExpandWidth(true), GUILayout.Height(30));

        GUI.enabled = _currentIndex < _scenes.Count - 1;
        if (GUILayout.Button("Next  ▶", GUILayout.Height(30)))
            LoadScene(_currentIndex + 1);

        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();
    }

    // ── Load scene + optionally apply overrides ───────────────────────────────
    private void LoadScene(int index)
    {
        if (index < 0 || index >= _scenes.Count) return;

        SceneEntry entry = _scenes[index];
        if (entry.sceneAsset == null)
        {
            Debug.LogWarning("[SceneWalker] Scene asset is not assigned.");
            return;
        }

        // Prompt save if dirty
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;

        string path = AssetDatabase.GetAssetPath(entry.sceneAsset);
        EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        _currentIndex = index;

        if (_autoApply)
            ApplyOverrides(entry);

        Repaint();
    }

    // ── Apply transform overrides to found GameObjects ────────────────────────
    private void ApplyOverrides(SceneEntry entry)
    {
        if (entry.overrides == null || entry.overrides.Count == 0) return;

        int applied = 0;

        foreach (ObjectOverride ov in entry.overrides)
        {
            if (string.IsNullOrWhiteSpace(ov.objectName)) continue;

            GameObject[] all = FindLegacy(ov.objectName);

            if (all.Length == 0)
            {
                Debug.LogWarning($"[SceneWalker] No GameObject found with name \"{ov.objectName}\".");
                continue;
            }

            foreach (GameObject go in all)
            {
                Undo.RecordObject(go.transform, "SceneWalker Override");

                if (ov.overridePosition)
                    go.transform.position = ov.position;

                if (ov.overrideRotation)
                    go.transform.eulerAngles = ov.rotation;

                if (ov.overrideScale)
                    go.transform.localScale = ov.scale;

                EditorUtility.SetDirty(go);
                applied++;
            }
        }

        Debug.Log($"[SceneWalker] Applied overrides to {applied} object(s) in \"{entry.sceneAsset.name}\".");
    }

    // Fallback name search for Unity versions older than 2023.1
    private static GameObject[] FindLegacy(string searchName)
    {
        var results = new List<GameObject>();
#pragma warning disable CS0618
        foreach (GameObject go in FindObjectsOfType<GameObject>(true))
#pragma warning restore CS0618
            if (go.name == searchName)
                results.Add(go);
        return results.ToArray();
    }
}