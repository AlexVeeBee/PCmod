using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCMod.OS.zLayering;
using PCMod.OS.Sys2000;

namespace PCMod.OS.zLayering
{
	public partial class ZLayering
	{
		public int nextLayer = 0;

		public ZLayering()
		{
		}

		public int GetLayer()
		{
			nextLayer++;
			return nextLayer;
		}
	}
}

namespace PCMod.OS.Sys2000
{
	public class sys2000_CreateButton : Panel
	{
		private Panel Container;
		public Button btn;
		public Panel inner;
		public Label btntxt;


		public string text
		{
			get { return btntxt.Text; }
			set { btntxt.Text = value; }
		}
		public sys2000_CreateButton( string varText )
		{
			Container = Add.Panel();
			Container.Style.Set( "flex-direction: column;" );
			btn = Container.Add.Button( null );
			inner = btn.Add.Panel( "btn-inner" );
			btntxt = inner.Add.Label( varText );
		}
	}
	public class sys2000_CreateDivider : Panel
	{
		public Panel Outer;
		private Panel Inner;
		public sys2000_CreateDivider()
		{
			Outer = Add.Panel( "outer" );
			Inner = Outer.Add.Panel( "inner" );
		}

		public string BorderOuterStyle
		{
			set { Outer.Style.Set( value ); }
		}
		public string BorderInnerStyle
		{
			set { Inner.Style.Set( value ); }
		}

	}
	public class sys2000_CreateTextEntry : Panel
	{
		public Panel Outer;
		private Panel Inner;
		public TextEntry txtE;
		public Color CaretColor
		{
			set{txtE.CaretColor = value;}
		}


		public sys2000_CreateTextEntry()
		{
			Outer = Add.Panel( "outer" );
			Inner = Outer.Add.Panel( "inner" );
			txtE = Inner.Add.TextEntry( "" );
		}

		public string BorderOuterStyle
		{
			set { Outer.Style.Set( value ); }
		}
		public string BorderInnerStyle
		{
			set { Inner.Style.Set( value ); }
		}

	}
	public class sys2000_Windows : Panel
	{
		// Credit: Ryhon0 for the Moveable window script (i am stupid)

		public Vector2 _pos;
		public Vector2 _size;

		public Vector2 pos
		{
			get { return _pos; }
			set
			{
				_pos = value;
				Style.Set( $"left: {value.x}px; top: {value.y}px;" );
			}
		}
		public Vector2 size
		{
			get { return _size; }
			set
			{
				_size = value;
				WinInner.Style.Set( $"width: {value.x}px; height: {value.y}px;" );
			}
		}
		public Vector2 minSize = new Vector2( 100, 100 );
		public Vector2 maxSize = new Vector2( 1920, 1080 );
		private bool _isResizeable = true;
		public bool isResizeable
		{
			get { return this._isResizeable;  }
			set { 
				if(value == true)
				{
					_isResizeable = value;
					Corner_resize_br.SetClass( "hidden", false);
					Corner_resize_br.Style.Dirty();
				}
				if(value == false)
				{
					_isResizeable = value;
					Corner_resize_br.SetClass( "hidden", true );
					Corner_resize_br.Style.Dirty();
				}
			}
		}
		public bool isHidden = false;
		public bool isMoving;
		public bool isResizing;
		public bool ShowTitleBar = true;
		private bool _ShowCloseButton = true;
		public bool ShowCloseButton
		{
			get { return _ShowCloseButton; }
			set
			{
				_ShowCloseButton = value;
				if (value == true)
				{
					titleCloseButton.Style.Set( "display: flex;" );
				}
				if ( value == false )
				{
					titleCloseButton.Style.Set( "display: none;" );
				}
			}
		}
		public bool ShowReziseButtons = true;
		public bool isWindowFullscreen = false;

		public string _Windowtitle;
		public string Windowtitle
		{
			get { return _Windowtitle; }
			set
			{
				_Windowtitle = value;
				WinTitle.Text = _Windowtitle;
			}
		}
		public Vector2 DragOffset;

		public Panel MainContent;

		private Panel Window;
		private Panel WinInner;
		private Panel titlebar;

		private Panel titleCloseButton;

		private Panel mainPanel;
		private Panel UserPanel;
		private Panel Corner_resize_br;

		public Label WinTitle;



		public sys2000_Windows( string WindowName, Panel Content )
		{
			MainContent = Content;

			Window = Add.Panel( "Window" );

			Window.AddEventListener( "onClick",() => Window.Style.ZIndex = new ZLayering().GetLayer() );

			WinInner = Window.Add.Panel( "inner-br" );
			WinInner.Style.Set( $"width: {_size.x}px; height: {_size.y}px;" );

			// Titlebar

			titlebar = WinInner.Add.Panel( "titlebar" );
			Panel Left_title = titlebar.Add.Panel( "tb_Title" );
			Panel Middle_Drag = titlebar.Add.Panel( "tb_Dragable" );
			Panel Right_buttons = titlebar.Add.Panel( "tb_buttons" );

			WinTitle = Left_title.Add.Label( WindowName, "TitleBarWindowName" );

			titleCloseButton = Right_buttons.Add.Button( null, "CloseButton" );
			//16
			//Closebutton.Style.Set( "" );
			Panel Closebutton_inner = titleCloseButton.Add.Panel( "btn-inner" );
			Image Closebutton_image = Closebutton_inner.Add.Image( "UI/icons/close_small.png", "btn-icon" );

			//Corner Resizer

			Corner_resize_br = Window.Add.Panel( "Corner_resize_br" );


			// Buttons

			titleCloseButton.AddEventListener( "onClick", () => Close() );


			Middle_Drag.AddEventListener( "OnMouseDown", () =>
			{
				isMoving = true;
				DragOffset = titlebar.MousePosition + Vector2.Up + 1 + Vector2.Left + 1;
				_pos = DragOffset;
			} );
			Middle_Drag.AddEventListener( "OnMouseUp", () => {
				isMoving = false;
				if( _pos.x <= 0)
				{
					_pos.x = 0;
					Style.Top = 0;
				}
				if ( _pos.y <= 0 )
				{
					_pos.y = 0;
					Style.Left = 0;
				}
			} );

			Corner_resize_br.AddEventListener( "OnMouseDown", () =>
			 {
				 isResizing = true;
				 DragOffset = Corner_resize_br.MousePosition + Vector2.Up + 1 + Vector2.Left + 1;
			 } );

			Corner_resize_br.AddEventListener( "OnMouseUp", () => isResizing = false );

			// Draw Content

			AddChild( Corner_resize_br );

			mainPanel = WinInner.Add.Panel( "Content" );
			mainPanel.AddChild( MainContent );
		}

		public sys2000_Windows() { }
		public override void OnParentChanged()
		{
			base.OnParentChanged();

			if ( isWindowFullscreen == true )
			{
				if ( Screen.Size.x != _size.x || Screen.Size.y != _size.y )
				{
					isWindowFullscreen = false;
				}
			}
		}

		public void Move( Vector2 vec )
			=> Move( vec.x, vec.y );

		public void Move( float x, float y )
		{
			if ( Style.Left.HasValue )
				x += Style.Left.Value.Value;

			if ( Style.Top.HasValue )
				y += Style.Top.Value.Value;

			Style.Left = x;
			Style.Top = y;
			Style.Dirty();
		}
		public void Resize( float width, float height )
		{
			Vector2 minimun = minSize;

			if ( Style.MinWidth.HasValue )
				minimun.x = WinInner.Style.Width.Value.Value;

			if ( Style.MinHeight.HasValue )
				minimun.y = WinInner.Style.Height.Value.Value;

			WinInner.Style.Width = Math.Max( minimun.x, width );
			WinInner.Style.Height = Math.Max( minimun.y, height );

			Style.Dirty();
		}

		[Event.Frame]
		void Frame()
		{
			if ( isResizing )
			{
				var size = MousePosition + DragOffset * 2;
				Resize( size.x, size.y );
			}

			else if ( isMoving )
			{
				var pos = MousePosition - DragOffset;
				Move( pos );
			}
		}

		public void Close()
		{
			Delete();
		}
		public void removeFocus()
		{
			SetClass( "focused", false );
		}
		public void setFocus()
		{
			SetClass( "focused", true );
		}
	}
}

public class Sys2000 : Panel
{
	public bool isBooting { get; set; }
	public Panel main { get; set; }
	public Panel winContainer { get; set; }

	public Panel textContainer_1;
	public Panel textContainer_2;
	public sys2000_Windows window;

	public Sys2000()
	{
		Style.Set( "pointer-events: all;" );

		SetTemplate( "UI/sys2000/index.html" );
		StyleSheet.Load( "UI/sys2000/index.scss" );

		//textContainer_1 = Add.Panel("textContainer");
		
		textContainer_1 = Add.Panel("content");
		textContainer_1.Add.Label( "Hello world from code" );
		textContainer_1.Add.Label( "I am testing" );
		textContainer_1.AddChild(new sys2000_CreateDivider() { } );
		textContainer_1.Add.Label( "I am testing" );
		textContainer_1.Add.Label( "I am testing" );
		textContainer_1.Add.Label( "I am testing" );
		textContainer_1.AddChild( new sys2000_CreateDivider() { BorderOuterStyle = "Margin: 12px 0px;"  } );
		Panel ButtonContainer_test = textContainer_1.Add.Panel( "" );

		// Panel Button 1
		Panel Pbtn = new sys2000_CreateButton( "test" );
		Pbtn.AddEventListener( "onClick", () =>
		{
			Log.Info( "bruh" );
		} );
		ButtonContainer_test.AddChild( Pbtn );
		// Panel Button 2
		sys2000_CreateButton Pbtn2 = new sys2000_CreateButton("test 2: on change")
		{

		};
		var didonce = false;
		Pbtn2.AddEventListener( "onClick", () =>
		{
			Log.Info( "bruh 2" );
			Pbtn2.text = "< Back";
			Pbtn2.btntxt.Style.Set( "padding: 8px 0px; width: 120px; text-align: center;" );
			if ( !didonce )
			{
				didonce = true;
			sys2000_CreateButton Pbtn_inOnclick = new sys2000_CreateButton( "Next >" )
			{

			};
			Pbtn_inOnclick.btntxt.Style.Set( "padding: 8px 0px; width: 120px; text-align: center;" );
			ButtonContainer_test.AddChild( Pbtn_inOnclick );

			}

		} );
		textContainer_1.AddChild(new sys2000_CreateDivider() { } );
		textContainer_1.AddChild( ButtonContainer_test );
		ButtonContainer_test.AddChild( Pbtn2 );

		Panel btns_winChange = textContainer_1.Add.Panel( "" );

		//sys2000_CreateButton btn_callClass = new sys2000_CreateButton( "call Class" );
		//	btn_callClass.AddEventListener( "onClick", () =>
		//	{
		//		//AddChild( _ = new WinTest() );
		//		if(window.isResizeable == true )
		//		{
		//			window.isResizeable = false;
		//			btn_callClass.text = "Make resizeable";
		//		}
		//		else
		//		{
		//			window.isResizeable = true;
		//			btn_callClass.text = "Make not resizeable";
		//		}
		//	} );
		sys2000_CreateButton btn_hideshowClosebtn = new sys2000_CreateButton( "hide close button" );
			btn_hideshowClosebtn.AddEventListener( "onClick", () =>
			{
				//AddChild( _ = new WinTest() );
				if(window.ShowCloseButton == true )
				{
					window.ShowCloseButton = false;
					btn_hideshowClosebtn.text = "show close button";
				}
				else
				{
					window.ShowCloseButton = true;
					btn_hideshowClosebtn.text = "hide close button";
				}
			} );
		sys2000_CreateButton btn_hideshowResize = new sys2000_CreateButton( "show resize corner" );
			btn_hideshowResize.AddEventListener( "onClick", () =>
			{
				//AddChild( _ = new WinTest() );
				if(window.isResizeable == true )
				{
					window.isResizeable = false;
					btn_hideshowClosebtn.text = "show resize corner";
				}
				else
				{
					window.isResizeable = true;
					btn_hideshowClosebtn.text = "hide resize corner";
				}
			} );

		//btns_winChange.AddChild( btn_callClass );
		btns_winChange.AddChild( btn_hideshowClosebtn );
		btns_winChange.AddChild( btn_hideshowResize );
		textContainer_1.AddChild( btns_winChange );
		textContainer_1.AddChild( new sys2000_CreateDivider() { } );
		textContainer_1.AddChild( new sys2000_CreateTextEntry() { } );

		window = new sys2000_Windows( "Hello world", textContainer_1 )
		{
			pos = new Vector2( 0, 0 ),
			size = new Vector2( 300, 300 ),
			isResizeable = true,

		};


		AddChild( window );
	/*
		textContainer_2 = Add.Panel( "content" );
		textContainer_2.Add.Image( "UI/icons/Ex5QlBEWYAgnHIm.jpg" );
		textContainer_2.Add.Label( "ALERT ALERT ALERT ALERT ALERT ALERT ALERT ALERT" );

		window = new sys2000_Windows( "Shut Down Windows", textContainer_2 )
		{
			size = new Vector2( 417, 500 ),
			pos = new Vector2(300,500)
		};
		AddChild( window );
	*/
	}
	public void CreateNewWindow()
	{
		Log.Info( "Button Press" );

		Panel MainSetup = Add.Panel( "waiting" );
		
		MainSetup.StyleSheet.Load( "UI/sys2000/StyleSheetsTest/setupWindowTest.scss" );
		
		Panel Setup = MainSetup.Add.Panel( "setupBanner" );


		Panel LeftImage = Setup.Add.Panel( "LeftIMG" );
		LeftImage.Add.Image( "UI/icons/logo_dark.png" );

		Panel ps = MainSetup.Add.Panel( "ps" );
		ps.Add.Label( "Please wait..." );


		Panel bre = new sys2000_Windows( "PC MOD System 2000 Setup", MainSetup )
		{
			size = new Vector2( 351, 137 ),
			pos = new Vector2( 250, 500 ),
			isResizeable=false,
			ShowCloseButton = false,
		};
		main.AddChild( bre );
	}

	public void DrawSetupTop()
	{
	}

	public void RemoveAnyFocusedWindows()
	{

	}

}
public partial class WinTest : sys2000_Windows
{
	public WinTest()
	{
		Windowtitle = "lol";
		MainContent = Add.Label( "llol" );
	}
}
