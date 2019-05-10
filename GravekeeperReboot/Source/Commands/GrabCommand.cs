using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	public class GrabCommand : ICommand {
		private Entity player;

		public GrabCommand(Entity player) {
			this.player = player;
		}

		void ICommand.Execute() {
			// Check Rotation
			// Check if Entity forward
			// Mark Entity's as grabbed
		}

		void ICommand.Undo() {

		}
	}
}
