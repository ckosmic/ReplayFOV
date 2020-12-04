using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FOVCamReplay
{
	/// <summary>
	/// Monobehaviours (scripts) are added to GameObjects.
	/// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
	/// </summary>
	public class FOVCamReplayController : MonoBehaviour
	{
		public static FOVCamReplayController Instance { get; private set; }
		private GameObject camObj;
		private int state = 0;

		// These methods are automatically called by Unity, you should remove any you aren't using.
		#region Monobehaviour Messages
		/// <summary>
		/// Only ever called once, mainly used to initialize variables.
		/// </summary>
		private void Awake() {
			// For this particular MonoBehaviour, we only want one instance to exist at any time, so store a reference to it in a static property
			//   and destroy any that are created while one already exists.
			if (Instance != null) {
				Plugin.Log?.Warn($"Instance of {GetType().Name} already exists, destroying.");
				GameObject.DestroyImmediate(this);
				return;
			}
			GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
			Instance = this;
			Plugin.Log?.Debug($"{name}: Awake()");
		}

		/// <summary>
		/// Called every frame if the script is enabled.
		/// </summary>
		private void Update() {
			if (state == 1) {
				if (camObj != null) {
					Camera cam = camObj.GetComponent<Camera>();
					cam.fieldOfView = PluginConfig.Instance.FOV;
					state = 2;
				} else {
					camObj = GameObject.Find("RecorderCamera");
				}
			}
			
		}

		private void LateUpdate() {
			if (state == 2 && PluginConfig.Instance.PositionalCorrection) {
				camObj.transform.position = new Vector3(camObj.transform.position.x, camObj.transform.position.y, -(140 - PluginConfig.Instance.FOV)/90f);
			}
		}

		/// <summary>
		/// Called when the script is being destroyed.
		/// </summary>
		private void OnDestroy() {
			Plugin.Log?.Debug($"{name}: OnDestroy()");
			if (Instance == this)
				Instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.

		}
		#endregion

		public void OnGameSceneLoaded() {
			state = 1;
			Plugin.Log.Info("OnGameSceneLoaded");
			camObj = null;
		}

		public void OnLevelDidFinish(StandardLevelScenesTransitionSetupDataSO scene, LevelCompletionResults result) {
			state = 0;
			Plugin.Log.Info("OnLevelDidFinish");
			camObj = null;
		}
	}
}
