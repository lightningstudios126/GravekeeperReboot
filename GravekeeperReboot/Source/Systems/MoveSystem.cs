using GravekeeperReboot.Source.Components;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	class MoveSystem : EntitySystem {
		private InputSystem input;
		private GameBoard gameBoard;

		private float timePassed = 0.0f;
		private const float turnLength = 0.2f;

		bool IsAnimating => timePassed > 0;

		public MoveSystem(Matcher matcher) : base(matcher) {}

        protected override void process(List<Entity> entities) {
            base.process(entities);
			if (gameBoard == null) gameBoard = scene.getSceneComponent<GameBoard>();
			if (input == null) input = scene.getEntityProcessor<InputSystem>();

			if (entities.Count > 0) {
				timePassed += Time.deltaTime;

				foreach (Entity entity in entities) {
					entity.getComponent<AnimationComponent>().animation(timePassed / turnLength);
				}

				if (timePassed >= turnLength) {
					timePassed = 0;
					entities.ForEach(e => e.removeComponent<AnimationComponent>());
				}
			}
		}

		public void InterruptAnimation() {
			if (IsAnimating) {
				timePassed = turnLength;
			}
		}
	}
}
