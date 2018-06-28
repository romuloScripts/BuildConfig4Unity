using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BuildConfig{
	[CreateAssetMenu(fileName = "Builder", menuName = "BuildConfig/Builder", order = 0)]
	public class Builder : ScriptableObject {

#if UNITY_EDITOR
		[Header("Normal Settings")]
		public string buildFolderPath;
		public string buildName;
		public BuildTarget buildTarget = BuildTarget.StandaloneWindows64;
		public bool developerBuild;
		public bool createBuildNameFolder=true;

		[Header("Define Symbols")]
		public bool setDefineSymbols=true;
		public string defineSymbols;
		public BuildTargetGroup buildTargetGroup = BuildTargetGroup.Standalone;

		[Header("Scenes")]
		public SceneList sceneList;
		

		public void Build(){
			if(string.IsNullOrEmpty(buildFolderPath)) return;
			string previousSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
			if(setDefineSymbols)
				PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup,defineSymbols);
			try{
				string[] scenes = sceneList?sceneList.GetScenes():SceneList.GetBuildSettingsScenes();
				string buildNamePath;
				if(createBuildNameFolder)
					buildNamePath = buildFolderPath + "/" + buildTarget + "/" + buildName;
				else
					buildNamePath = buildFolderPath + "/" + buildTarget;
				string buildFullPath = buildNamePath + "/" + buildName+".exe";

				DeleteBuildFolder(buildNamePath);
				BuildOptions bo = developerBuild?BuildOptions.Development:BuildOptions.None;
				BuildPipeline.BuildPlayer (scenes, buildFullPath, buildTarget,bo);	
        	}finally{
				PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup,previousSymbols);
    		}
		}
		
		private void DeleteBuildFolder(string buildFolderPath) {
			if (System.IO.Directory.Exists (buildFolderPath)) {
				System.IO.Directory.Delete (buildFolderPath, true);
			}
		}
#endif
	}
}
