using Nez;
using System.Collections.Generic;

namespace Input {
	public class CommandSystem : ProcessingSystem {
		private static List<Command> commands = new List<Command>();

		public static void AddCommand(Command command) {
			commands.Add(command);
		}

		public override void process() {
			for (int i = 0; i < commands.Count; i++)
				commands[i].Execute();
		}
	}
}
