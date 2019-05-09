using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;

namespace Input {
    public class InputSystem : ProcessingSystem {	
		private Action AButton = () => CommandSystem.AddCommand(new MoveCommand(Core.scene.findEntitiesWithTag((int)Tags.Player)[0], new Vector2(100, 100)));
		private Action BButton = () => CommandSystem.AddCommand(new MoveCommand(Core.scene.findEntitiesWithTag((int)Tags.Player)[0], new Vector2(50, 50)));

		public override void process() {
			if (Nez.Input.isKeyPressed(Keys.A)) AButton();
			else if (Nez.Input.isKeyPressed(Keys.B)) BButton();
		}
	}
}
