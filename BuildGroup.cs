using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace BuildConfig{
	[CreateAssetMenu(fileName = "BuildGroup", menuName = "BuildConfig/Build Group", order = 1)]
	public class BuildGroup : ScriptableObject {
		
		public Builder[] builders;

		[Header("Steam SDK Settings")]
		public SteamBuildUpload steamSettings;

#if UNITY_EDITOR
		public void BuildAll(){
			Build(builders,steamSettings);
		}

		public static void Build(Builder[] builders,SteamBuildUpload steamSettings=null){
			foreach (var item in builders) {
				if(string.IsNullOrEmpty(item.buildFolderPath)){
					item.buildFolderPath = EditorUtility.SaveFolderPanel("Select Build Folder","","");
				}
			}
			BuildTarget previousBuildTarget = EditorUserBuildSettings.activeBuildTarget;
			BuildTargetGroup previousBuildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
			foreach (var item in builders) {
				item.Build();
			}
			List<string> buildFolders = new List<string>();
			foreach (var item in builders) {
				if(!buildFolders.Contains(item.buildFolderPath)){
					buildFolders.Add(item.buildFolderPath);
					OpenFolder(item.buildFolderPath,item.buildTarget);
				}
			}
			steamSettings?.UploadToSteam();
			EditorUserBuildSettings.SwitchActiveBuildTarget(previousBuildTargetGroup,previousBuildTarget);
		}

		public static void OpenFolder(string buildFolderPath, BuildTarget target){
			EditorUtility.RevealInFinder(buildFolderPath+"/" + target + "/");
		}
#endif
	}
}