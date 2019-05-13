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
		private Entity player => scene.findEntity("Player");

		public InputSystem() : base() {
			WButton = () => commandSystem.QueueCommand(new MoveCommand(player, new Point(0, -1)));
			AButton = () => commandSystem.QueueCommand(new MoveCommand(player, new Point(-1, 0)));
			SButton = () => commandSystem.QueueCommand(new MoveCommand(player, new Point(0, 1)));
			DButton = () => commandSystem.QueueCommand(new MoveCommand(player, new Point(1, 0)));

			LeftArrow = () => commandSystem.QueueCommand(new RotateCommand(player, -90));
			RightArrow = () => commandSystem.QueueCommand(new RotateCommand(player, 90));
			LShift = () => commandSystem.QueueCommand(new GrabCommand(player));
			CButton = () => commandSystem.QueueCommand(new UndoCommand());
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

	}
}
