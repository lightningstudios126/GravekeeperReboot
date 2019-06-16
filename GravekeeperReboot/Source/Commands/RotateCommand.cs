using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Utilities;
using Nez;
using System;

namespace GravekeeperReboot.Source.Commands {
	class RotateCommand : Command {
		private TileEntity entity;
		private readonly TileDirection initialDirection;
		private readonly TileDirection finalDirection;

		public RotateCommand(TileEntity entity, TileDirection offset) {
			this.entity = entity;

			this.initialDirection = entity.tileDirection;
			this.finalDirection = Directions.DirAdd(entity.tileDirection, offset);
		}

		public override void Execute() {
			entity.tileDirection = finalDirection;
			entity.addComponent<AnimationComponent>().animation = Animation;
		}

		public override void Undo() {
			entity.tileDirection = initialDirection;
			entity.rotationDegrees = Directions.DirectionDegrees(initialDirection);
		}

		private void Animation(float progress) {
			var initial = Directions.DirectionDegrees(initialDirection);
			var final = Directions.DirectionDegrees(finalDirection);

			var offset = progress * (final - initial);

			// When final wraps around 360, the offset is sometimes 270 instead of -90
			// (spins three times in the opposite direction instead of once in the correct direction)
			if (Math.Abs(final - initial) > 90)
				offset = progress * (90 * -Math.Sign(offset)); 

			entity.setRotationDegrees(initial + offset);
		}
	}
}
