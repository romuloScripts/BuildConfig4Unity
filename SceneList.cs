using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BuildConfig{
	[CreateAssetMenu(fileName = "SceneList", menuName = "BuildConfig/Scene List", order = 2)]
	public class SceneList : ScriptableObject {
		
		public Object[] scenes;

#if UNITY_EDITOR
		public string[] GetScenes () {
			List<string> scenesStrings = new List<string>();
			foreach (Object s in scenes) {
				scenesStrings.Add(AssetDatabase.GetAssetPath(s));
			}
			return scenesStrings.Count >0?scenesStrings.ToArray():GetBuildSettingsScenes();
		}

		public static string[] GetBuildSettingsScenes(){
			List<string> scenesStrings = new List<string>();
			int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;     
			for( int i = 0; i < sceneCount; i++ ){
				scenesStrings.Add(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
			}
			return scenesStrings.ToArray();
		}
#endif
	}
}