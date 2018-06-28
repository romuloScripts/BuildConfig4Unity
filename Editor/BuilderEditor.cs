using UnityEngine;
using UnityEditor;

namespace BuildConfig{
	[CustomEditor(typeof(Builder)),CanEditMultipleObjects]
	public class BuilderEditor : Editor {
		private Builder[] builders;
	
		private void OnEnable () {
			builders = new Builder[targets.Length];
			for (int i = 0; i <targets.Length; i++) {
				builders[i] = (Builder)targets[i];
			}
		}

		public override void OnInspectorGUI () {
			DrawDefaultInspector();
			GUILayout.Space(20);
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Chose Build Folder")){
				string buildFolderPath = EditorUtility.SaveFolderPanel("Select Build Folder","","");
				foreach (var item in builders) {
					item.buildFolderPath = buildFolderPath;
				}
			}
			if(GUILayout.Button("Build")){
				EditorApplication.delayCall += ()=> BuildGroup.Build(builders);
			}
			GUILayout.EndHorizontal();
		}
	}
}

