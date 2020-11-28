using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using NoxFiretail.EditorExtras;

[InitializeOnLoad]
public static class AutoSaveOnRunMenuItem
{
    public const string MenuBeforePlay = "Tools/Autosave/On Run";
    public const string MenuLostFocus = "Tools/Autosave/On Lost Focus";
    private static bool isAvtosaveOnRun;
    private static bool isAutosaveOnLostFocus = false;

    static AutoSaveOnRunMenuItem()
    {
        EditorApplication.delayCall += () =>
        {
            isAvtosaveOnRun = EditorPrefs.GetBool(MenuBeforePlay, false);
            UnityEditor.Menu.SetChecked(MenuBeforePlay, isAvtosaveOnRun);
            SetOnRun();
            SetOnReload();
        };
    }

    [MenuItem(MenuBeforePlay)]
    private static void AutosaveBeforeRun()
    {
        isAvtosaveOnRun = !isAvtosaveOnRun;
        UnityEditor.Menu.SetChecked(MenuBeforePlay, isAvtosaveOnRun);
        EditorPrefs.SetBool(MenuBeforePlay, isAvtosaveOnRun);
        SetOnRun();
    }

    [MenuItem(MenuLostFocus)]
    private static void AutosaveFocusLost()
    {
        isAutosaveOnLostFocus = !isAutosaveOnLostFocus;
        UnityEditor.Menu.SetChecked(MenuLostFocus, isAutosaveOnLostFocus);
        EditorPrefs.SetBool(MenuLostFocus, isAutosaveOnLostFocus);
        SetOnReload();
    }

    private static void SetOnRun()
    {
        if (isAvtosaveOnRun)
            EditorApplication.playModeStateChanged += AutoSaveOnRun;
        else EditorApplication.playModeStateChanged -= AutoSaveOnRun;
    }

    private static void SetOnReload()
    {
        if (isAutosaveOnLostFocus)
            EditorWindowFocusUtility.OnUnityEditorFocusLost += AutoSaveOnReload;
        else EditorWindowFocusUtility.OnUnityEditorFocusLost -= AutoSaveOnReload;
    }

    private static void AutoSaveOnReload()
    {
        AutoSave();
    }

    private static void AutoSaveOnRun(PlayModeStateChange state)
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
        {
            AutoSave();
        }
    }

    private static void AutoSave()
    {
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
    }
}