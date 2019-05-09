using Nez;
using Microsoft.Xna.Framework;
using GravekeeperReboot.Source.Components;

namespace GravekeeperReboot.Source.Commands {
	public class MoveCommand : ICommand{
        private Entity target;
        private Vector2 offset;
       
        public MoveCommand(Entity target, Vector2 offset) {
			if (target.getComponent<MoveComponent>() == null)
				throw new System.ArgumentException("Target does not have a MoveComponent attached!");

            this.target = target;
            this.offset = offset;
        }

        void ICommand.Execute() {
			target.getComponent<MoveComponent>().targetPosition += offset;
        }

		void ICommand.Undo() {
			target.getComponent<MoveComponent>().targetPosition -= offset;
		}
	}
}
