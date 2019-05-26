using Microsoft.Xna.Framework.Input;
using Nez;

namespace GravekeeperReboot.Source.ActionMapping {
	public class CustomKeyBinding : KeyBinding {
		public CustomKeyBinding() {
			UpButton = Keys.Up;
			LeftButton = Keys.Left;
			DownButton = Keys.Down;
			RightButton = Keys.Right;

			GrabButton = Keys.Z;
			UndoButton = Keys.X;
		}

		public void Rebind() {

		}
	}
}
