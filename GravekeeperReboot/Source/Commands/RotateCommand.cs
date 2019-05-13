using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	class RotateCommand : Command {
		private Entity entity;
		private float offset;

		private RotateComponent rotateComponent;

		public RotateCommand(Entity entity, float offset) {
			if(!entity.HasComponent<RotateComponent>())
				throw new System.NullReferenceException("Target does not have a RotateComponent attached!");

			this.entity = entity;
			this.offset = offset;

			rotateComponent = entity.getComponent<RotateComponent>();
		}

		public override void Execute() {
			rotateComponent.targetRotation += offset;
			rotateComponent.UpdateDirection();
		}

		public override void Undo() {
			rotateComponent.targetRotation -= offset;
			rotateComponent.UpdateDirection();
		}
	}
}
