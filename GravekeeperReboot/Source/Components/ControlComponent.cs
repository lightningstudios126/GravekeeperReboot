using GravekeeperReboot.Source.Extensions;
using Nez;

namespace GravekeeperReboot.Source.Components {
	public class ControlComponent : Component {
		public bool isPushable;
		public bool isPivotable;

		public TileComponent tileComponent;

		public ControlComponent(bool isPushable, bool isPivotable) {
			this.isPushable = isPushable;
			this.isPivotable = isPivotable;
		}

		public override void onAddedToEntity() {
			base.onAddedToEntity();

			if (isPushable && !entity.HasComponent<TileComponent>())
				throw new System.NullReferenceException("Entity is pushable yet has no TileComponent");
			if (isPivotable && !entity.HasComponent<TileComponent>())
				throw new System.NullReferenceException("Entity is pivotable yet has no TileComponent");
		}
	}
}
