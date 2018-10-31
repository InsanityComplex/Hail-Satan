//Works on Unity 5.3+

using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave
{
    //Constructor called by Unity Editor
    static AutoSave()
    {
        EditorApplication.playModeStateChanged += SaveWhenExitEdit;
    }

    private static void SaveWhenExitEdit(PlayModeStateChange change)
    {
        //If we're exiting edit mode (about to play the scene)
        if (change == PlayModeStateChange.ExitingEditMode)
        {
            //Save the scene and the assets
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        }
    }
}
