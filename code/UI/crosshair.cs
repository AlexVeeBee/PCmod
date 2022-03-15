using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

namespace PCMod.ui.spawnmenu;

[Library]
public class crosshair : Panel
{
	public Panel cross;

	public crosshair()
	{
		//Style.Set(
		//	"width: 100%; height: 100%; position: absolute;" +
		//	"flex-direction: row;" +
		//	"align-items: center;" +
		//	"justify-items: center;" +
		//	"justify-content: center;" +
		//	"align-content: center;"
		//);

		cross = Add.Panel( "" );
		cross.Style.Set(
			"position: absolute; transform: translateX( -50% ) translateY( -50% );" +
			"border: 2px solid black; " +
			"padding: 3px; border-radius: " +
			"16px; background-color: white;" );
	}

	public override void Tick()
	{
		base.Tick();

		this.PositionAtCrosshair();
	}
}
