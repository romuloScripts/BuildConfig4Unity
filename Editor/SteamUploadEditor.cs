using UnityEngine;
using UnityEditor;

namespace BuildConfig{
	[CustomEditor(typeof(SteamBuildUpload))]
	public class SteamUploadEditor : Editor {
	
		SteamBuildUpload b;

		private void OnEnable () {
			b= (SteamBuildUpload)target;
		}

		public override void OnInspectorGUI () {
			DrawDefaultInspector();
			GUILayout.Space(20);
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Chose Steam Upload Exe")){
				b.steamcmdExePath = EditorUtility.OpenFilePanel("Select Steam Exe File","","");
			}
			if(GUILayout.Button("Upload")){
				EditorApplication.delayCall += ()=> b.UploadToSteam();
			}
			GUILayout.EndHorizontal();
		}
	}
}

