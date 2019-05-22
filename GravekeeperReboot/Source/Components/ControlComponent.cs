using GravekeeperReboot.Source.Extensions;
using Nez;

namespace GravekeeperReboot.Source.Components {
	public class ControlComponent : Component {
		public bool isPushable;
		public bool isPivotable;

		public MoveComponent moveComponent;

		public ControlComponent(bool isPushable, bool isPivotable) {
			this.isPushable = isPushable;
			this.isPivotable = isPivotable;
		}

		public override void onAddedToEntity() {
			base.onAddedToEntity();

			if (isPushable && !entity.HasComponent<MoveComponent>())
				throw new System.NullReferenceException("Entity is pushable yet has no MoveComponent");
			if (isPivotable && !entity.HasComponent<MoveComponent>())
				throw new System.NullReferenceException("Entity is pivotable yet has no MoveComponent");
		}
	}
}
