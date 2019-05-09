﻿using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;

namespace GravekeeperReboot.Source.Systems {
    public class InputSystem : ProcessingSystem {
		private CommandSystem commandSystem;
		private Action AButton, BButton, CButton;

		public InputSystem(Scene scene) : base() {
			commandSystem = scene.getEntityProcessor<CommandSystem>();
			AButton = () => commandSystem.AddCommand(new MoveCommand(scene.findEntitiesWithTag((int)Tags.Player)[0], new Vector2(10, 0)));
			BButton = () => commandSystem.AddCommand(new MoveCommand(scene.findEntitiesWithTag((int)Tags.Player)[0], new Vector2(-10, 0)));
			CButton = () => commandSystem.AddCommand(new UndoCommand());
		}

		public override void process() {
			if (Nez.Input.isKeyPressed(Keys.A)) AButton();
			if (Nez.Input.isKeyPressed(Keys.B)) BButton();
			if (Nez.Input.isKeyPressed(Keys.C)) CButton();
		}

	}
}
