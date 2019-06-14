using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	class MoveSystem : EntitySystem {
		private InputSystem input;

		GameBoard gameBoard;
		float timePassed = 0.0f;
		float turnLength = 0.2f;
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
					foreach (Entity entity in entities) {
						entity.removeComponent<AnimationComponent>();
					}
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
