using Sandbox;
using Sandbox.DataModel;
using Sandbox.UI;
using Sandbox.UI.Construct;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PCMod.pc.monitors.test
{
	public partial class KeyboardTest : AnimEntity
	{
		public virtual int KeyboardId => 0;
		public virtual string KeyboardName => "Basic ass keyboard";
		//public virtual const Conecctions => {
		//
		//	};

		public Vector3 ScreenRenderPos;
		public Rotation ScreenRenderRot;

		public bool isConnected;

		public Panel sys;

		public KeyboardTest()
		{
		}

		public override void Spawn()
		{
			SetModel( "models/basickeyboard.vmdl" );
			Tags.Add( "Keyboard" );
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
		}
	}
}





