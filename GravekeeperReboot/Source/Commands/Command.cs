namespace GravekeeperReboot.Source.Commands {
	public class Command {
		public bool playerInitiated = true;
		public virtual void Execute() { }
		public virtual void Undo() { }
    }
}
