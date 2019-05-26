using Microsoft.Xna.Framework.Input;

namespace GravekeeperReboot.Source.ActionMapping {
	public class WASDKeyBinding : KeyBinding {
		public WASDKeyBinding() {
			UpButton = Keys.W;
			LeftButton = Keys.A;
			DownButton = Keys.S;
			RightButton = Keys.D;

			GrabButton = Keys.LeftShift;
			UndoButton = Keys.C;
		}
	}
}
