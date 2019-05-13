using GravekeeperReboot.Source.Components;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	class RotateCommand : ICommand {
		private Entity target;
		private float offset;

		private RotateComponent rotateComponent;

		public RotateCommand(Entity target, float offset) {
			if(target.getComponent<RotateComponent>() == null)
				throw new System.ArgumentException("Target does not have a RotateComponent attached!");

			this.target = target;
			this.offset = offset;

			rotateComponent = target.getComponent<RotateComponent>();
		}

		void ICommand.Execute() {
			rotateComponent.targetRotation += offset;
			rotateComponent.UpdateDirection();
		}

		void ICommand.Undo() {
			rotateComponent.targetRotation -= offset;
			rotateComponent.UpdateDirection();
		}
	}
}
