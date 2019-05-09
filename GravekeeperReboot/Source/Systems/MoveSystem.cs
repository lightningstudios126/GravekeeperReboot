using GravekeeperReboot.Source.Components;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
    class MoveSystem : EntitySystem {
        public MoveSystem(Matcher matcher) : base(matcher) {}

        protected override void process(List<Entity> entities) {
            base.process(entities);
            foreach(Entity entity in entities) {
                MoveComponent moveComponent = entity.getComponent<MoveComponent>();
                if (entity.position != moveComponent.targetPosition)
                    entity.position = moveComponent.targetPosition;
            }
        }
    }
}
