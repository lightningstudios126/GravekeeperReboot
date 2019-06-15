using Nez;

namespace GravekeeperReboot.Source.Components {
	class AnimationComponent : Component {
		public delegate void Animation(float progress);
		public Animation animation;

		public override void onRemovedFromEntity() {
			base.onRemovedFromEntity();
			animation(1f);
		}
	}
}
