using GravekeeperReboot.Source.Commands;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	public class CommandSystem : ProcessingSystem {
		private Stack<Command> commandsExecuted = new Stack<Command>();
		private Queue<Command> commandBuffer = new Queue<Command>();

		public void StartNewTurn() {
			QueueCommand(new TurnCommand());
		}

		public void UndoTurn() {
			QueueCommand(new UndoCommand());
		}

		public void QueueCommand(Command command) {
			System.Console.WriteLine("queued:" + command.GetType().Name);
			commandBuffer.Enqueue(command);
		}

		//public void QueueCommand(params Command[] commands) {
		//	foreach (Command command in commands) {
		//		commandBuffer.Enqueue(command);
		//	}
		//}

		public override void process() {
			while (commandBuffer.Count > 0) {
				Command command = commandBuffer.Dequeue();
				if (command is UndoCommand) {
					if (commandsExecuted.Count > 0) {
						// undo commands until it reaches the beginning of the turn
						while (commandsExecuted.Count > 0 && !(commandsExecuted.Peek() is TurnCommand)) {
							Command toundo = commandsExecuted.Pop();
							System.Console.WriteLine("undid: " + toundo);
							toundo.Undo();
						}

						commandsExecuted.Pop();
					}
				} else {
					if (!(command is TurnCommand))
						command.Execute();
					commandsExecuted.Push(command);
				}
			}
		}
	}
}
