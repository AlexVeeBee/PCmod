using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

namespace PCMod.ui.spawnmenu;

[Library]
public class SpawnMenu : Panel
{
	public Panel menu;
	public Panel props;
	public Panel tools;
	public Panel items_pr;
	public Panel items_tools;

	public Panel pr_tabs;
	public Panel tools_tabs;

	public Panel pr_items;
	public Panel tools_iems;

	public SpawnMenu()
	{
		StyleSheet.Load( "UI/spawnmenu/spawnmenu.scss" );

		menu = Add.Panel("spawner");
			menu.SetClass( "open", false );

		props = menu.Add.Panel( "props" );
			pr_tabs = props.Add.Panel( "tabs" );
				pr_tabs.Add.Button("presets", "tab");
				pr_tabs.Add.Button( "Computer Parts", "tab");
				pr_tabs.Add.Button( "Computer Accessories", "tab");
		Button conMen = pr_tabs.Add.Button( "Debug Spawn", "tab");
		pr_items = props.Add.Panel( "items" );
			pr_items.Add.Panel( "item" );
			pr_items.Add.Panel( "item" );

		tools = menu.Add.Panel( "tools" );
			tools_tabs = tools.Add.Panel( "tabs" );
				tools_tabs.Add.Button("tools", "tab");
			tools_iems = tools.Add.Panel( "items" );

		conMen.AddEventListener( "onClick", () =>
		{
			NewContextMenu m = new NewContextMenu();

			m.AddButtonItem( "Spawn Monitor Test", () =>
			{
				ConsoleSystem.Run( "spawn_Montest" );
			} );
			m.AddButtonItem( "Spawn Keyboard Test", () =>
			{
				ConsoleSystem.Run( "spawn_keybtest" );
			} );
			m.AddButtonItem( "Spawn Tower Test", () =>
			{
				ConsoleSystem.Run( "spawn_towertest" );
			} );

				ContextSubmenu m2 = new ContextSubmenu();
				m2.AddSubMenuItem( "Keybind settings", () => { } );
				m2.AddSubMenuItem( "Audio Settings", () => { } );
				m2.AddSubMenuItem( "More Settings", () => { } );
				m2.AddSubMenuItem( "WTF settings", () => { } );
			m.AddSubMenu( "Another Context Menu", m2 );

			AddChild( m );
		} );
	}

	public override void Tick()
	{
		base.Tick();

			menu.SetClass( "open", Input.Down( InputButton.Menu ) );
	}
}
