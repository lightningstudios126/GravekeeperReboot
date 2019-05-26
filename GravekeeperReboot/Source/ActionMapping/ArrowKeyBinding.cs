using Microsoft.Xna.Framework.Input;

namespace GravekeeperReboot.Source.ActionMapping {
	public class ArrowKeyBinding : KeyBinding {
		public ArrowKeyBinding() {
			UpButton = Keys.Up;
			LeftButton = Keys.Left;
			DownButton = Keys.Down;
			RightButton = Keys.Right;

			GrabButton = Keys.Z;
			UndoButton = Keys.X;
		}
	}
}
