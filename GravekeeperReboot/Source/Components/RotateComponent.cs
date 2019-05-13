using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Components {
	public class RotateComponent : Component {
		public float targetRotation;
		public Vector2 Direction { get; private set; } = new Vector2(0, -1);

		public void UpdateDirection() {
			float angle = entity.rotation;
			Direction = -Vector2.Normalize(new Vector2(Mathf.roundToInt(Mathf.cos(angle)), Mathf.roundToInt(Mathf.sin(angle))));
		}
	}
}
