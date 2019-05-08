using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;

namespace Input {
    public static class InputHandler {
        private static Action AButton = () => new MoveCommand(Core.scene.findEntitiesWithTag((int)Tags.Player)[0], new Vector2(100, 100)).Execute();

        public static void Update() {
            if (Nez.Input.isKeyPressed(Keys.A)) AButton();
        }
    }
}
