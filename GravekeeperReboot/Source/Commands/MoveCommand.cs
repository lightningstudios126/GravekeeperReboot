using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	public class MoveCommand : Command {
        private Entity entity;
        private Point offset;

		private MoveComponent moveComponent;
       
        public MoveCommand(Entity entity, Point offset) {
			if (!entity.HasComponent<MoveComponent>())
				throw new System.ArgumentException("Target does not have a MoveComponent attached!");

            this.entity = entity;
            this.offset = offset;
			moveComponent = entity.getComponent<MoveComponent>();
		}

		public override void Execute() {
			moveComponent.position += offset;
        }

		public override void Undo() {
			moveComponent.position -= offset;
		}
	}
}
