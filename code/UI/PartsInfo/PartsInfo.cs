using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Linq;
using PCMod.pc.monitors.test;
using PCMod.player;

namespace PCMod.ui.PartsInfoUI
{
	[Library]
	public class PartsInfo : Panel
	{
		public Panel PartsContainer;
		public Label ItemName;
		public Label Info;
		public Panel PortsInfoContainer;
		public PartsInfo()
		{
			StyleSheet.Load( "UI/PartsInfo/PartsInfo.scss" );

			PartsContainer = Add.Panel( "InfoContainer" );

			Panel ItemNameContainer = PartsContainer.Add.Panel();
			ItemName = ItemNameContainer.Add.Label( "Hover on something", "ItemName" );

			PortsInfoContainer = PartsContainer.Add.Panel( "PartsInfo" );
		}

		public override void Tick()
		{
			base.Tick();

			if(Local.Pawn is PCplayer player )
			{
				var tr = Trace.Ray( player.EyePosition, player.EyePosition + player.EyeRotation.Forward * 100 )
						.Ignore( player ).Run();
				if (tr.Entity.IsValid() )
				{
					if (tr.Entity is MonitorTest monitor )
					{
						ItemName.Text = $"{ monitor.MonitorName }";

						this.PositionAtWorld( tr.Entity.Position );
						PartsContainer.SetClass( "hidden", false );
					} else if ( tr.Entity is KeyboardTest keyboard )
					{
						ItemName.Text = $"{ keyboard.KeyboardName }";


						this.PositionAtWorld( tr.Entity.Position );
						PartsContainer.SetClass( "hidden", false );
					}
					else if ( tr.Entity is TowerTest Tower )
					{
						ItemName.Text = $"{ Tower.TowerName }";

						this.PositionAtWorld( tr.Entity.Position );
						PartsContainer.SetClass( "hidden", false );
					}
					else
					{
						PartsContainer.SetClass( "hidden", true );
					}
				}
				else
				{
					ItemName.Text = "";
					PartsContainer.SetClass( "hidden", true );
				}
			}

		}
	}
}
