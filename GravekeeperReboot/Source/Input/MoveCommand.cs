using Nez;
using Components;
using Microsoft.Xna.Framework;

namespace Input {
	public class MoveCommand : ICommand{
        private Entity target;
        private Vector2 position;
		private Vector2 previousPosition;
       
        public MoveCommand(Entity target, Vector2 position) {
			if (target.getComponent<MoveComponent>() == null)
				throw new System.ArgumentException("Target does not have a MoveComponent attached!");

            this.target = target;
            this.position = position;
        }

        void ICommand.Execute() {
			previousPosition = target.position;
			target.getComponent<MoveComponent>().targetPosition = position;
        }

		void ICommand.Undo() {
			target.getComponent<MoveComponent>().targetPosition = previousPosition;
		}
	}
}
