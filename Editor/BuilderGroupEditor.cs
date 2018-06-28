using UnityEngine;
using UnityEditor;

namespace BuildConfig{
	[CustomEditor(typeof(BuildGroup))]
	public class BuilderGroupEditor : Editor {
	
		BuildGroup b;

		private void OnEnable () {
			b= (BuildGroup)target;
		}

		public override void OnInspectorGUI () {
			DrawDefaultInspector();
			GUILayout.Space(20);
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Build All")){
				EditorApplication.delayCall += ()=> b.BuildAll();
			}
			GUILayout.EndHorizontal();
		}
	}
}

