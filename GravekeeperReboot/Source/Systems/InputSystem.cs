using GravekeeperReboot.Source.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;

namespace GravekeeperReboot.Source.Systems {
	public class InputSystem : ProcessingSystem {
		private CommandSystem commandSystem;
		private Action WButton, AButton, SButton, DButton, CButton, LeftArrow, RightArrow, LShift;
		private Entity player => scene.findEntity("Player");

		public InputSystem(Scene scene) : base() {

			WButton = () => MoveUp();
			AButton = () => MoveLeft();
			SButton = () => MoveDown();
			DButton = () => MoveRight();

			LeftArrow = () => RotateLeft();
			RightArrow = () => RotateRight();
			LShift = () => Grab();
			CButton = () => Undo();
		}

		public override void process() {
			if (commandSystem == null) commandSystem = scene.getEntityProcessor<CommandSystem>();

			if (Input.isKeyPressed(Keys.W)) WButton();
			if (Input.isKeyPressed(Keys.A)) AButton();
			if (Input.isKeyPressed(Keys.S)) SButton();
			if (Input.isKeyPressed(Keys.D)) DButton();

			if (Input.isKeyPressed(Keys.C)) CButton();
			if (Input.isKeyPressed(Keys.Left)) LeftArrow();
			if (Input.isKeyPressed(Keys.Right)) RightArrow();
			if (Input.isKeyPressed(Keys.LeftShift)) LShift();
			if (Input.isKeyReleased(Keys.LeftShift)) LShift();
		}


		// Main Game Actions
		public void MoveUp() => commandSystem.QueueCommand(new MoveCommand(player, new Vector2(0, -TiledMapConstants.TileSize)));
		public void MoveLeft() => commandSystem.QueueCommand(new MoveCommand(player, new Vector2(-TiledMapConstants.TileSize, 0)));
		public void MoveDown() => commandSystem.QueueCommand(new MoveCommand(player, new Vector2(0, TiledMapConstants.TileSize)));
		public void MoveRight() => commandSystem.QueueCommand(new MoveCommand(player, new Vector2(TiledMapConstants.TileSize, 0)));

		public void RotateLeft() => commandSystem.QueueCommand(new RotateCommand(player, -90));
		public void RotateRight() => commandSystem.QueueCommand(new RotateCommand(player, 90));

		public void Grab() => commandSystem.QueueCommand(new GrabCommand(player));
		public void Undo() => commandSystem.QueueCommand(new UndoCommand());
	}
}
