using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Separator : EditorWindow
{
	private static Separator window;
	
	private string input = "";
	private string prefabPath = "Assets/Prefabs/Separator.prefab";

	[MenuItem("GameObject/Create separator")]
	private static void ShowWindow()
	{
		window = GetWindow<Separator>();
		window.titleContent = new GUIContent("Separator Settings");
		Vector2 windowSize = new Vector2(400, 30);
		window.maxSize = windowSize;
		window.minSize = windowSize;
		window.Show();
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		
		input = EditorGUILayout.TextField("Separator name:", input);

		EditorGUILayout.EndHorizontal();

		if (Event.current.keyCode == KeyCode.Return)
		{
			GameObject obj = (GameObject) Instantiate (AssetDatabase.LoadAssetAtPath<Object>(prefabPath));
			obj.name = $"******************** {input} ********************";
			EditorSceneManager.SaveOpenScenes();
			window.Close();
		}
	}
}
