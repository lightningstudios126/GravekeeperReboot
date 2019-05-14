using Microsoft.Xna.Framework;
using Nez;
using static GravekeeperReboot.Source.Utilities.Direction;

namespace GravekeeperReboot.Source.Components {
	public class RotateComponent : Component {
		/// <summary>
		/// Direction that the entity with this component should be facing.
		/// </summary>
		public Directions direction = Directions.UP;

		//public void UpdateDirection() {
		//	float angle = entity.rotation;
		//	Direction = -Vector2.Normalize(new Vector2(Mathf.roundToInt(Mathf.cos(angle)), Mathf.roundToInt(Mathf.sin(angle))));
		//}
	}
}
