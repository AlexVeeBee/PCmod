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

			Pos = MousePosition;

			AddEventListener( "onClick", () => Delete() );
		}

		public void AddButtonItem(string Text, Action OnClick)
		{
			Button btm = ContextMenu.Add.Button( Text , "MenuItem");

			btm.AddEventListener( "onClick", () => { OnClick();
				Delete();
			} );
		}

	}
}
namespace PCMod.ui.ContextMenuComps
{

}
