using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BuildConfig{
	[CreateAssetMenu(fileName = " SteamBuildUpload", menuName = "BuildConfig/Steam Build Upload", order = 3)]
	public class SteamBuildUpload: ScriptableObject{

		public string steamcmdExePath;
		public string steamBuildScript = "app_build_<steam_appid>.vdf";
		public string username;
		public string password;

		private const string steamCmdArgFormat = "+login {0} {1} +run_app_build \"{2}\"";

#if UNITY_EDITOR
		public void UploadToSteam(){
			if(string.IsNullOrEmpty(steamcmdExePath)){
				steamcmdExePath = EditorUtility.OpenFilePanel("Select Steam Exe File","","");
			}
			if(!File.Exists(steamcmdExePath) || !File.Exists(steamBuildScript)){
				UnityEngine.Debug.Log("Steam files no exists");
				return;
			}
			ExecuteCMD();
		}

		private void ExecuteCMD(){
			Process foo = new Process();
			foo.StartInfo.FileName = steamcmdExePath;
			foo.StartInfo.Arguments = string.Format(steamCmdArgFormat, username, password, steamBuildScript);
			foo.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
			foo.Start();
		}
#endif
	}
}
