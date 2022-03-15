using Sandbox;
using Sandbox.UI;
using PCMod.WorldUI;
using PCMod.pc.monitors.test;

namespace PCMod.player {
	public class PCplayer : Player
	{
		public Clothing.Container Clothing = new();
		WorldInput WorldInput = new();

		public PCplayer()
		{

		}

		public PCplayer(Client cl) : this() {
			Clothing.LoadFromClient( cl );
		}
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			Controller = new WalkController();
			Animator = new StandardPlayerAnimator();

			Clothing.DressEntity( this );

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			CameraMode = new FirstPersonCamera();

			base.Respawn();

		}


		public override void BuildInput( InputBuilder input )
		{
			var cursor_trace = Trace.Ray( Input.Cursor, 10f )
				.Ignore( Owner ).Ignore( this ).Run();

			if(input.Down(InputButton.Attack1))
			{
				//DebugOverlay.Sphere( cursor_trace.EndPosition, 2f, Color.Red, true, 2f );
			}

			WorldInput.Ray = new Ray( cursor_trace.StartPosition, cursor_trace.EndPosition );
			WorldInput.MouseLeftPressed = input.Down( InputButton.Attack1 );
		}

		public override void Simulate( Client cl )
		{
			if ( Input.ActiveChild != null )
			{
				ActiveChild = Input.ActiveChild;
			}



			TickPlayerUse();
			SimulateActiveChild( cl, ActiveChild );


			//var cursor_trace = Trace.Ray( Input.Cursor, 9999f )
			//	.Ignore( Owner ).Ignore( this ).Run();
			var eye_trace = Trace.Ray( EyePosition, EyePosition + EyeRotation.Forward * 75f )
				.Ignore( Owner ).Ignore( this ).Run();
			//
			//DebugOverlay.Sphere( cursor_trace.EndPosition, 1f, Color.Red, true, 2f );
			//DebugOverlay.Sphere( eye_trace.EndPosition, 2f, Color.Orange, true, 2f );

			if ( Input.Pressed( InputButton.View ) )
			{
				if ( CameraMode is ThirdPersonCamera )
				{
					CameraMode = new FirstPersonCamera();
				}
				else
				{
					CameraMode = new ThirdPersonCamera();
				}
			}

			if (Input.Pressed(InputButton.Use ))
			{
				if( eye_trace.Entity is MonitorTest mon )
				{
					mon.interact();
				}
			}

			base.Simulate( cl );
		}
	}

}
