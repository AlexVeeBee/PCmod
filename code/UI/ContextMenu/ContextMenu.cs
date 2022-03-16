using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Linq;


namespace PCMod.ui 
{
	public partial class NewContextMenu : Panel
	{
		public Panel ContextMenu;
		public Panel OutsideBox;


		private float _Minwidth;

		public float Minwidth
		{
			set { ContextMenu.Style.MinWidth = value; _Minwidth = value; }
		}
		private Vector2 Pos
		{
			set { ContextMenu.Style.Set($"left: {value.x}; top: {value.y}"); }
		}
		public NewContextMenu()
		{
			StyleSheet.Load( "UI/ContextMenu/ContextMenu.scss" );
			ContextMenu = Add.Panel("menu");
			OutsideBox = Add.Panel( "OutsideBox" );
			Pos = MousePosition;

			OutsideBox.AddEventListener( "onClick", () => {
				Delete();
			} );
		}

		public void AddButtonItem(string Text, Action OnClick)
		{
			
			Button btm = ContextMenu.Add.Button( Text , "MenuItem");

			btm.AddEventListener( "onClick", () => { OnClick();
				Delete();
			} );
		}

		public void AddSubMenu( string Text, ContextSubmenu Items  )
		{
			//Button btm = ContextMenu.Add.Button( Text, "SubMenuItem" );
			Vector2 Pos = MousePosition;
			Log.Info( "DEBUG: Style width:" );
			Log.Info( ContextMenu.Style.Width );
			//Items.Style.Set($"left: {}px; top: {Pos.y}px");
			Button btm = ContextMenu.Add.Button( Text , "SubMenuItem" );
			
			btm.AddEventListener( "onClick", () =>
			{
				AddChild( Items );
			} );

			/*
			btm.AddEventListener( "mouseenter", () => {
				OnClick();
			} );*/
		}
	}
	public partial class ContextSubmenu : Panel
	{
		public Panel Menu;
		public ContextSubmenu()
		{
			Log.Info( "I am now rendered" );

			Menu = Add.Panel( "menu" );
			Style.Set( $" z-index: 2; position: absolute;" );
			Menu.Style.Set( $"background-color: red; padding: 12px;" );

			Style.Set( $"left: {MousePosition.x}px; top: {MousePosition.y}px" );
			
			AddChild( Menu );
		}
		public void AddSubMenuItem( string Text, Action OnClick )
		{
			Log.Info( $"Added Submenu; TEXT: {Text}" );

			Button btm = Menu.Add.Button( Text, "MenuItem" );

			btm.AddEventListener( "onClick", () => {
				OnClick();
				Delete();
			} );
		}
	}
}
namespace PCMod.ui.ContextMenuComps
{

}
