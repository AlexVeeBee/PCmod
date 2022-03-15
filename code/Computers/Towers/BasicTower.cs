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
	public partial class TowerTest : AnimEntity
	{
		public virtual int TowerId => 0;
		public virtual string TowerName => "Basic ass tower";

		public bool IsOn;

		public TowerTest()
		{
		}

		public override void Spawn()
		{
			SetModel( "models/basitower.vmdl" );
			Tags.Add( "Tower" );
		}
	}
}





