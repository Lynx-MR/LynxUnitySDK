using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Lynx.UI
{
    [CustomEditor(typeof(Lynx.UI.LynxThemeManager))]
    public class LynxThemeManagerEditor : Editor
    {
        
        private const string STR_THEME_MANAGER = "LynxThemeManager.prefab";


        bool showPosition = false;


        public static void InstantiateThemeManager()
        {
            GameObject handMenu = LynxBuildSettings.InstantiateGameObjectByPath(LynxBuildSettings.LYNX_CORE_PATH, STR_THEME_MANAGER, null);
            handMenu.transform.SetAsLastSibling();
            Undo.RegisterCreatedObjectUndo(handMenu, "Theme Manager");
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        public void EnableOnAllObject(bool enable = true)
        {
            foreach (LynxImage elt in FindObjectsByType<LynxImage>(FindObjectsInactive.Include ,FindObjectsSortMode.None))
                elt.SetUseTheme(enable);

            foreach (LynxSimpleButton elt in FindObjectsByType<LynxSimpleButton>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                elt.SetUseTheme(enable);

            foreach (LynxSlider elt in FindObjectsByType<LynxSlider>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                elt.SetUseTheme(enable);

            foreach (LynxSwitchButton elt in FindObjectsByType<LynxSwitchButton>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                elt.SetUseTheme(enable);

            foreach (LynxTimerButton elt in FindObjectsByType<LynxTimerButton>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                elt.SetUseTheme(enable);

            foreach (LynxToggleButton elt in FindObjectsByType<LynxToggleButton>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                elt.SetUseTheme(enable);

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }


        public override void OnInspectorGUI()
        {
            LynxThemeManager script = (LynxThemeManager)target;
            GUIStyle bold = new GUIStyle();
            bold = EditorStyles.boldLabel;

            serializedObject.Update();

            base.OnInspectorGUI();

            GUILayout.Space(5);

            showPosition = EditorGUILayout.Foldout(showPosition, "Helper");
            if (showPosition)
            {
                if (GUILayout.Button("Enable on all Lynx UI"))
                {
                    EnableOnAllObject();
                    script.RefreshThemes();
                }


                if (GUILayout.Button("Disable on all Lynx UI"))
                    EnableOnAllObject(false);
            }


            serializedObject.ApplyModifiedProperties();
        }
    }
}

