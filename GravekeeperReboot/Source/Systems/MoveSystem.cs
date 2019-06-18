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
		bool interrupt = false;


		public MoveSystem(Matcher matcher) : base(matcher) { }

		protected override void process(List<Entity> entities) {
			base.process(entities);
			if (gameBoard == null) gameBoard = scene.getSceneComponent<GameBoard>();
			if (input == null) input = scene.getEntityProcessor<InputSystem>();

			if (interrupt) {
				entities.ForEach(RemoveAnimations);
				timePassed = 0;
				interrupt = false;
			} else if (entities.Count > 0) {
				timePassed += Time.deltaTime;
				float tween = Utilities.Tween.EaseInOutCubic(timePassed / turnLength);
				foreach (Entity entity in entities) {
					entity.getComponent<AnimationComponent>().animation(tween);
				}

				if (timePassed >= turnLength) {
					entities.ForEach(RemoveAnimations);
					timePassed = 0;
				}
			}

			void RemoveAnimations(Entity entity) {
				entity.getComponents<AnimationComponent>().ForEach(c => entity.removeComponent(c));
			}
		}

		public void InterruptAnimation() {
			if (IsAnimating) {
				interrupt = true;
			}
		}
	}
}
