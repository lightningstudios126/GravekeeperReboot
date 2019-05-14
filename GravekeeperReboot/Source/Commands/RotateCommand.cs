using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	class RotateCommand : Command {
		private Entity entity;
		private Utilities.Direction.Directions offset;

		private RotateComponent rotateComponent;

		public RotateCommand(Entity entity, Utilities.Direction.Directions offset) {
			if(!entity.HasComponent<RotateComponent>())
				throw new System.ArgumentException("Target does not have a RotateComponent attached!");

			this.entity = entity;
			this.offset = offset;

			rotateComponent = entity.getComponent<RotateComponent>();
		}

		public override void Execute() {
			rotateComponent.direction = Utilities.Direction.DirAdd(rotateComponent.direction, offset);
		}

		public override void Undo() {
			rotateComponent.direction = Utilities.Direction.DirAdd(rotateComponent.direction, offset+2);
		}
	}
}
