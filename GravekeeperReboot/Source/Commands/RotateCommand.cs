using GravekeeperReboot.Source.Components;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	class RotateCommand : ICommand {
		private Entity target;
		private float offset;

		public RotateCommand(Entity target, float offset) {
			this.target = target;
			this.offset = offset;
		}

		void ICommand.Execute() {
			target.getComponent<RotateComponent>().targetRotation += offset;
		}

		void ICommand.Undo() {
			target.getComponent<RotateComponent>().targetRotation -= offset;
		}
	}
}
