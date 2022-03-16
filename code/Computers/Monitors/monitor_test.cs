using Sandbox;
using Sandbox.DataModel;
using Sandbox.UI;
using Sandbox.UI.Construct;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using PCMod.pc.ui;

namespace PCMod.pc.monitors.test {
	public partial class MonitorTest : AnimEntity
	{
		public enum Connections 
		{
			VGA,
		}
		public virtual int MonitorId => 0;
		public virtual string MonitorName => "View Tronic Big boi";


		public Vector3 ScreenRenderPos;
		public Rotation ScreenRenderRot;

		public bool isConnected;

		public Panel sys;

		public MonitorTest() { 
		}

		ScreenUI ScreenUI;

		public override void Spawn()
		{
			SetModel( "models/monitor_dellvmdl.vmdl" );
			Tags.Add( "Monitor" );

			SpawnUI();
		}

		[ClientRpc]
		public void SpawnUI()
		{
			ScreenUI = new ScreenUI();
			ScreenUI.Transform = this.Transform;
		}

		public void interact()
		{
			Log.Info( "Monitor Interact" );
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			ScreenUI.Position = this.Position;
			Log.Info( "THIS SHIT IS RUNNING OR BROKEN LOL XD" );
		}
	}
}





