using Nez;
using System.Collections.Generic;

namespace Input {
	public class CommandSystem : ProcessingSystem {
		private Stack<ICommand> commandsExecuted = new Stack<ICommand>();
		private Queue<ICommand> commandBuffer = new Queue<ICommand>();

		public void AddCommand(ICommand command) {
			commandBuffer.Enqueue(command);
		}

		public override void process() {
			while (commandBuffer.Count > 0) {
				ICommand command = commandBuffer.Dequeue();
				if (!(command is UndoCommand)) {
					command.Execute();
					commandsExecuted.Push(command);
				} else if (commandsExecuted.Count > 0) {
					commandsExecuted.Pop().Undo();
				}
			}
		}
	}
}
