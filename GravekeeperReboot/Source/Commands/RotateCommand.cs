using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Utilities;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	class RotateCommand : Command {
		private Entity entity;
		private TileDirection offset;

		private RotateComponent rotateComponent;

		public RotateCommand(Entity entity, TileDirection offset) {
			if(!entity.HasComponent<RotateComponent>())
				throw new System.ArgumentException("Target does not have a RotateComponent attached!");

			this.entity = entity;
			this.offset = offset;

			rotateComponent = entity.getComponent<RotateComponent>();
		}

		public override void Execute() {
			rotateComponent.direction = Directions.DirAdd(rotateComponent.direction, offset);
		}

		public override void Undo() {
			rotateComponent.direction = Directions.DirAdd(rotateComponent.direction, offset+2);
		}
	}
}
