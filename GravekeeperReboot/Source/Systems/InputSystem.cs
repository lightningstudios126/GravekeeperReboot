using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;

namespace GravekeeperReboot.Source.Systems {
    public class InputSystem : ProcessingSystem {
		private CommandSystem commandSystem;
		private Action WButton, AButton, SButton, DButton, CButton, LeftArrow, RightArrow, LShift;
		private Entity player => scene.findEntitiesWithTag((int)Tags.Player)[0];

		public InputSystem(Scene scene) : base() {
			commandSystem = scene.getEntityProcessor<CommandSystem>();

			WButton = () => commandSystem.QueueCommand(new MoveCommand(player, new Vector2(0, -TiledMapConstants.TileSize)));
			AButton = () => commandSystem.QueueCommand(new MoveCommand(player, new Vector2(-TiledMapConstants.TileSize, 0)));
			SButton = () => commandSystem.QueueCommand(new MoveCommand(player, new Vector2(0, TiledMapConstants.TileSize)));
			DButton = () => commandSystem.QueueCommand(new MoveCommand(player, new Vector2(TiledMapConstants.TileSize, 0)));

			LeftArrow = () => commandSystem.QueueCommand(new RotateCommand(player, -90));
			RightArrow = () => commandSystem.QueueCommand(new RotateCommand(player, 90));
			LShift = () => commandSystem.QueueCommand(new GrabCommand(player));
			CButton = () => commandSystem.QueueCommand(new UndoCommand());
		}

		public override void process() {
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

	}
}
