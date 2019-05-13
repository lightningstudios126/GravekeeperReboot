using System;

namespace GravekeeperReboot.Source.Commands {
	public class UndoCommand : Command {
		public override void Undo() {
			throw new NotImplementedException("wait that's illegal: undo");
		}

		public override void Execute() {
			throw new NotImplementedException("wait that's illegal: execute");
		}
	}
}
