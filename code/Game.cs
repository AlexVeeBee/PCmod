
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using PCMod.ui;
using PCMod.player;
using PCMod.WorldUI;
using PCMod.OS.Sys2000;
using PCMod.pc.monitors.test;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace Sandbox
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	public partial class MyGame : Sandbox.Game
	{
		public GameUI GameUI;
		public SpawnUI SpawnUI;

		public MyGame()
		{
			if(IsServer)
			{
				SpawnUI = new SpawnUI();
			}
			if(IsClient)
			{
				GameUI = new GameUI();
			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			// Create a pawn for this client to play with

			var player = new PCplayer( client );
			player.Respawn();

			client.Pawn = player;

			//var worldPanel = new WorldGameUI();
			//worldPanel.Transform = Local.Pawn.Transform;

			// Get all of the spawnpoints
			//var spawnpoints = Entity.All.OfType<SpawnPoint>();
			//
			//// chose a random one
			//var randomSpawnPoint = spawnpoints.OrderBy( x => Guid.NewGuid() ).FirstOrDefault();
			//
			//// if it exists, place the pawn there
			//if ( randomSpawnPoint != null )
			//{
			//	var tx = randomSpawnPoint.Transform;
			//	tx.Position = tx.Position + Vector3.Up * 50.0f; // raise it up
			//	pawn.Transform = tx;
			//}
		}

		[ServerCmd( "spawn" )]
		public static void Spawn( string modelname )
		{
			var owner = ConsoleSystem.Caller?.Pawn;

			if ( ConsoleSystem.Caller == null )
				return;

			var tr = Trace.Ray( owner.EyePosition, owner.EyePosition + owner.EyeRotation.Forward * 500 )
				.UseHitboxes()
				.Ignore( owner )
				.Run();

			var model = Model.Load( modelname );
			if ( model == null || model.IsError )
				return;

			var ent = new Prop
			{
				Position = tr.EndPosition + Vector3.Down * model.PhysicsBounds.Mins.z,
				Rotation = Rotation.From( new Angles( 0, owner.EyeRotation.Angles().yaw, 0 ) ) * Rotation.FromAxis( Vector3.Up, 180 ),
				Model = model
			};

			// Let's make sure physics are ready to go instead of waiting
			ent.SetupPhysicsFromModel( PhysicsMotionType.Dynamic );
		}

		[ServerCmd( "spawn_Montest" ), ClientRpc]
		public static void SpawnMonitorTest()
		{
			var player = ConsoleSystem.Caller?.Pawn;
			
			var tr = Trace.Ray( 
				player.EyePosition, 
				player.EyePosition + player.EyeRotation.Forward * 500 )
				.UseHitboxes()
				.Ignore( player )
				.Run();


			var monitor = new MonitorTest();

			if ( monitor == null )
				return;

			monitor.Position = tr.EndPosition;

			monitor.SetupPhysicsFromModel( PhysicsMotionType.Dynamic );

			monitor.Spawn();

			//var ent = new Prop
			//{
			//	Position = tr.EndPosition + Vector3.Down * monitor.PhysicsBounds.Mins.z,
			//	Rotation = Rotation.From( new Angles( 0, player.EyeRotation.Angles().yaw, 0 ) ) * Rotation.FromAxis( Vector3.Up, 180 ),
			//	Model = monitor
			//};

			// Let's make sure physics are ready to go instead of waiting


			//MonitorTest
		}
		[ServerCmd( "spawn_keybtest" ), ClientRpc]
		public static void SpawnKeyboardTest()
		{
			var player = ConsoleSystem.Caller?.Pawn;

			var tr = Trace.Ray(
				player.EyePosition,
				player.EyePosition + player.EyeRotation.Forward * 500 )
				.UseHitboxes()
				.Ignore( player )
				.Run();


			var monitor = new KeyboardTest();

			if ( monitor == null )
				return;

			monitor.Position = tr.EndPosition;

			monitor.SetupPhysicsFromModel( PhysicsMotionType.Dynamic );

			monitor.Spawn();

			//var ent = new Prop
			//{
			//	Position = tr.EndPosition + Vector3.Down * monitor.PhysicsBounds.Mins.z,
			//	Rotation = Rotation.From( new Angles( 0, player.EyeRotation.Angles().yaw, 0 ) ) * Rotation.FromAxis( Vector3.Up, 180 ),
			//	Model = monitor
			//};

			// Let's make sure physics are ready to go instead of waiting


			//MonitorTest
		}


		[ServerCmd( "spawn_towertest" ), ClientRpc]
		public static void SpawnTowerTest()
		{
			var player = ConsoleSystem.Caller?.Pawn;

			var tr = Trace.Ray(
				player.EyePosition,
				player.EyePosition + player.EyeRotation.Forward * 500 )
				.UseHitboxes()
				.Ignore( player )
				.Run();


			var monitor = new TowerTest();

			if ( monitor == null )
				return;

			monitor.Position = tr.EndPosition;

			monitor.SetupPhysicsFromModel( PhysicsMotionType.Dynamic );

			monitor.Spawn();
		}
		[Event.Hotload]
		public void HotloadUIgame()
		{
			if ( IsServer )
			{
				SpawnUI.Delete();
				SpawnUI = new SpawnUI();
			}
			if ( IsClient )
			{
				if ( true )
				{
					GameUI.Delete();
					GameUI = new GameUI();
				}
			}
		}
	}
}
