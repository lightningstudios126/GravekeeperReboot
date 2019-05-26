using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Utilities;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	class RotateCommand : Command {
		private Entity entity;
		private TileDirection offset;

		private TileComponent entityTile;

		public RotateCommand(Entity entity, TileDirection offset) {
			if(!entity.HasComponent<TileComponent>())
				throw new System.ArgumentException("Target does not have a TileComponent attached!");

			this.entity = entity;
			this.offset = offset;

			entityTile = entity.getComponent<TileComponent>();
		}

		public override void Execute() {
			entityTile.tileDirection = Directions.DirAdd(entityTile.tileDirection, offset);
		}

		public override void Undo() {
			entityTile.tileDirection = Directions.DirAdd(entityTile.tileDirection, offset+2);
		}
	}
}
