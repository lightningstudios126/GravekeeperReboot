using Nez;
using Components;
using Microsoft.Xna.Framework;

namespace Input {
    public class Command {
        public Command() { }
        public virtual void Execute() { }
    }

    public class MoveCommand : Command{
        private Entity target;
        private Vector2 position;
       
        public MoveCommand(Entity target, Vector2 position) {
            this.target = target;
            this.position = position;
        }

        public override void Execute() {
            target.getComponent<MoveComponent>().targetPosition = position;
        }
    }
}
