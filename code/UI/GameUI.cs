using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

using PCMod.OS.Sys2000;
using PCMod.WorldUI;
using PCMod.ui.spawnmenu;
using PCMod.ui.PartsInfoUI;

namespace PCMod.ui
{
	public partial class GameUI : HudEntity<RootPanel>
	{
		//WorldPanel worldPanel = new WorldGameUI();
		public GameUI ()
		{
			//RootPanel.AddChild<Sys2000>();
			RootPanel.AddChild<crosshair>();
			RootPanel.AddChild<PartsInfo>();
			RootPanel.AddChild<SpawnMenu>();
			RootPanel.AddChild<VoiceList>();
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		}

		//[Event.Hotload]
		//public void HotloadUIgame()
		//{
		//		worldPanel.Delete();
		//	worldPanel = new WorldGameUI();
		//}

	}
}
namespace PCMod.WorldUI
{
	public partial class WorldGameUI : WorldPanel
	{
		public WorldGameUI()
		{
			Transform = Transform.Zero;
			PanelBounds = new Rect( -0, 0, 512, 512);
			Scale = 1f;
			AddChild<Sys2000>();
			//RootPanel.AddChild<Sys2000>();
			//RootPanel.AddChild<ChatBox>();
		}
	}
}
