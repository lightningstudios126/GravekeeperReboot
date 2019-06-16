using GravekeeperReboot.Source.Commands;
using Nez;
using System;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	public class CommandSystem : ProcessingSystem {
		private Stack<Command> commandsExecuted = new Stack<Command>();
		private Queue<Command> commandBuffer = new Queue<Command>();

		public void StartNewTurn() {
			QueueCommand(new TickCommand());
		}

		public void UndoTurn() {
			QueueCommand(new UndoCommand());
		}

		public void QueueCommand(Command command) {
			Console.WriteLine("queued: " + command.GetType().Name);
			commandBuffer.Enqueue(command);
		}

		public override void process() {
			// If nothing happened in this turn, don't record it
			if (commandBuffer.Count == 1 && commandBuffer.Peek() is TickCommand) {
				var emptyTurn = commandBuffer.Dequeue();
				return;
			}

			while (commandBuffer.Count > 0) {
				Command command = commandBuffer.Dequeue();
				if (command is UndoCommand) {
					if (commandsExecuted.Count > 0) {
						// undo commands until it reaches the beginning of the turn
						while (commandsExecuted.Count > 0 && !(commandsExecuted.Peek() is TickCommand)) {
							commandsExecuted.Pop().Undo();
						}

						commandsExecuted.Pop();
					}
				} else {
					if (!(command is TickCommand))
						command.Execute();
					commandsExecuted.Push(command);
				}
			}
		}
	}
}
