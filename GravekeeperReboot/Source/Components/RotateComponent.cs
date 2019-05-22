using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Components {
	public class RotateComponent : Component {
		/// <summary>
		/// Direction that the entity with this component should be facing.
		/// </summary>
		public TileDirection direction = TileDirection.UP;

		//public void UpdateDirection() {
		//	float angle = entity.rotation;
		//	Direction = -Vector2.Normalize(new Vector2(Mathf.roundToInt(Mathf.cos(angle)), Mathf.roundToInt(Mathf.sin(angle))));
		//}
	}
}
