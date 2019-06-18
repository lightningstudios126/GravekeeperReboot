using Nez;
using System;

namespace GravekeeperReboot.Source.Components {
	class AnimationComponent : Component {
		public Action<float> animation;
		public Action animationFinish;

		public override void onRemovedFromEntity() {
			base.onRemovedFromEntity();
			animation(1f);
			animationFinish();
		}
	}
}
