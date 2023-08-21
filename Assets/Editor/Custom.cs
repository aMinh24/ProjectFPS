

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Custom : EditorWindow
{
    [MenuItem("OpenScene/MainMenu", false, 1)]
    public static void Menu()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
    }
    [MenuItem("OpenScene/Campaign", false, 2)]
    public static void Campaign()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scenes/Campaign.unity");
    }

}
