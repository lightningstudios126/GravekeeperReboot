using System;

namespace GravekeeperReboot.Source.Commands {
	class UndoCommand : ICommand {
		void ICommand.Undo() {
			throw new NotImplementedException("wait that's illegal: undo");
		}

		void ICommand.Execute() {
			throw new NotImplementedException("wait that's illegal: execute");
		}
	}
}
