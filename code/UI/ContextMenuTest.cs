using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

namespace PCMod.ui.spawnmenu;

[Library]
public class ContextMenuTest : Panel
{
	public ContextMenuTest()
	{
		Style.Set( " position: absolute;width: 100%;height: 100%;" );
	}

	public override void Tick()
	{
		base.Tick();

		if(Input.Pressed(InputButton.Slot1))
		{
			NewContextMenu context = new NewContextMenu();

			context.AddButtonItem( "i am text", () =>
			{
				Log.Info( "hello" );
			} );

			AddChild( context );

		}
	}
}
