using Nez;
using Components;
using Microsoft.Xna.Framework;

namespace Input {
    public class Command {
        public Command() { }
        public virtual void Execute() { }
		public virtual void Undo() { }
    }

    public class MoveCommand : Command{
        private Entity target;
        private Vector2 position;
		private Vector2 previousPosition;
       
        public MoveCommand(Entity target, Vector2 position) {
			if (target.getComponent<MoveComponent>() == null)
				throw new System.ArgumentException("Target does not have a MoveComponent attached!");

            this.target = target;
            this.position = position;
        }

        public override void Execute() {
			previousPosition = target.position;
			target.getComponent<MoveComponent>().targetPosition = position;
        }

		public override void Undo() {
			base.Undo();
			target.getComponent<MoveComponent>().targetPosition = previousPosition;
		}
	}
}
