using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using PCMod.OS.Sys2000;

namespace PCMod.pc.ui
{
	public partial class ScreenUI : WorldPanel
	{
		public ScreenUI()
		{
			AddChild( new Sys2000() );
			Scale = 2f;
		}
	}
}
