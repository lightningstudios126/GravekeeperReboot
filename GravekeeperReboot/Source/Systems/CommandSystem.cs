using GravekeeperReboot.Source.Commands;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	public class CommandSystem : ProcessingSystem {
		private Stack<Command> commandsExecuted = new Stack<Command>();
		private Queue<Command> commandBuffer = new Queue<Command>();

		public void QueueCommand(Command command) {
			commandBuffer.Enqueue(command);
		}

		public void QueueCommand(params Command[] commands) {
			foreach (Command command in commands) {
				commandBuffer.Enqueue(command);
			}
		}

		public override void process() {
			while (commandBuffer.Count > 0) {
				Command command = commandBuffer.Dequeue();
				if (!(command is UndoCommand)) {
					command.Execute();
					commandsExecuted.Push(command);
				} else if (commandsExecuted.Count > 0) {
					// Undo all non-player initiated commands
					while (commandsExecuted.Count > 1 && !commandsExecuted.Peek().playerInitiated)
						commandsExecuted.Pop().Undo();

					commandsExecuted.Pop().Undo();
				}
			}
		}
	}
}
