using Nez;
using Microsoft.Xna.Framework;
using GravekeeperReboot.Source.Components;

namespace GravekeeperReboot.Source.Commands {
	public class MoveCommand : ICommand{
        private Entity target;
        private Point offset;
       
        public MoveCommand(Entity target, Point offset) {
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
