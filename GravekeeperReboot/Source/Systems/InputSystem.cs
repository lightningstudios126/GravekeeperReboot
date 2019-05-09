using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;

namespace GravekeeperReboot.Source.Systems {
    public class InputSystem : ProcessingSystem {
		private CommandSystem commandSystem;
		private Action WButton, AButton, SButton, DButton, CButton;

		public InputSystem(Scene scene) : base() {
			commandSystem = scene.getEntityProcessor<CommandSystem>();
			WButton = () => commandSystem.AddCommand(new MoveCommand(scene.findEntitiesWithTag((int)Tags.Player)[0], new Vector2(0, -10)));
			AButton = () => commandSystem.AddCommand(new MoveCommand(scene.findEntitiesWithTag((int)Tags.Player)[0], new Vector2(-10, 0)));
			SButton = () => commandSystem.AddCommand(new MoveCommand(scene.findEntitiesWithTag((int)Tags.Player)[0], new Vector2(0, 10)));
			DButton = () => commandSystem.AddCommand(new MoveCommand(scene.findEntitiesWithTag((int)Tags.Player)[0], new Vector2(10, 0)));
			CButton = () => commandSystem.AddCommand(new UndoCommand());
		}

		public override void process() {
			if (Input.isKeyPressed(Keys.W)) WButton();
			if (Input.isKeyPressed(Keys.A)) AButton();
			if (Input.isKeyPressed(Keys.S)) SButton();
			if (Input.isKeyPressed(Keys.D)) DButton();
			if (Input.isKeyPressed(Keys.C)) CButton();
		}

	}
}
