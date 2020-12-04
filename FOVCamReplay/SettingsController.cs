using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;

namespace FOVCamReplay
{
	public class SettingsController : PersistentSingleton<SettingsController>
	{
		[UIValue("fov")]
		public int FOV {
			get { return PluginConfig.Instance.FOV; }
			set { PluginConfig.Instance.FOV = value; }
		}

		[UIValue("pos-correction")]
		public bool PositionalCorrection
		{
			get { return PluginConfig.Instance.PositionalCorrection; }
			set { PluginConfig.Instance.PositionalCorrection = value; }
		}

		[UIAction("#apply")]
		public void OnApply() => Plugin.Log.Info($"FOV value set to: {FOV}");
	}
}
