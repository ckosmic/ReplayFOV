using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine.SceneManagement;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using BeatSaberMarkupLanguage.Settings;

namespace FOVCamReplay
{

	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin
	{
		internal static Plugin Instance { get; private set; }
		internal static IPALogger Log { get; private set; }

		[Init]
		/// <summary>
		/// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
		/// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
		/// Only use [Init] with one Constructor.
		/// </summary>
		public void Init(IPALogger logger, Config conf) {
			Instance = this;
			Log = logger;

			PluginConfig.Instance = conf.Generated<PluginConfig>();
			BSMLSettings.instance.AddSettingsMenu("Replay FOV", $"FOVCamReplay.Settings.bsml", SettingsController.instance);
			Log.Info("Config loaded.");

			Log.Info("FOVCamReplay initialized.");
		}

		#region BSIPA Config
		//Uncomment to use BSIPA's config
		/*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
		#endregion

		[OnStart]
		public void OnApplicationStart() {
			Log.Debug("OnApplicationStart");
			new GameObject("FOVCamReplayController").AddComponent<FOVCamReplayController>();

			BS_Utils.Utilities.BSEvents.gameSceneLoaded += FOVCamReplayController.Instance.OnGameSceneLoaded;
			BS_Utils.Plugin.LevelDidFinishEvent += FOVCamReplayController.Instance.OnLevelDidFinish;
		}

		[OnExit]
		public void OnApplicationQuit() {
			Log.Debug("OnApplicationQuit");
			BS_Utils.Utilities.BSEvents.gameSceneLoaded -= FOVCamReplayController.Instance.OnGameSceneLoaded;
			BS_Utils.Plugin.LevelDidFinishEvent -= FOVCamReplayController.Instance.OnLevelDidFinish;
		}
	}
}
