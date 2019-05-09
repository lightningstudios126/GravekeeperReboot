using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input {
	class UndoCommand : ICommand {
		void ICommand.Undo() {
			throw new NotImplementedException("wait that's illegal: undo");
		}

		void ICommand.Execute() {
			throw new NotImplementedException("wait that's illegal: execute");
		}
	}
}
