using Nez;
using Microsoft.Xna.Framework;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;

namespace GravekeeperReboot.Source.Commands {
	public class MoveCommand : Command {
        private Entity entity;
        private Vector2 offset;

		private MoveComponent moveComponent;
       
        public MoveCommand(Entity entity, Vector2 offset) {
			if (!entity.HasComponent<MoveComponent>())
				throw new System.NullReferenceException("Target does not have a MoveComponent attached!");

            this.entity = entity;
            this.offset = offset;
			moveComponent = entity.getComponent<MoveComponent>();
		}

		public override void Execute() {
			moveComponent.targetPosition += offset;
        }

		public override void Undo() {
			moveComponent.targetPosition -= offset;
		}
	}
}
