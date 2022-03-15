using Sandbox;
using Sandbox.DataModel;
using Sandbox.UI;
using Sandbox.UI.Construct;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

		public override void Spawn()
		{
			SetModel( "models/monitor_dellvmdl.vmdl" );
			Tags.Add( "Monitor" );
		}


		public void interact()
		{
			Log.Info( "Monitor Interact" );
		}
	}
}





