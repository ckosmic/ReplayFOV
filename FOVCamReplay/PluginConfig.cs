using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace FOVCamReplay
{
	internal class PluginConfig
	{
		public static PluginConfig Instance { get; set; }
		public int FOV { get; set; } = 50;
		public bool PositionalCorrection { get; set; } = true;
	}
}
