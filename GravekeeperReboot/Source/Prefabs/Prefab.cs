using Nez;

namespace GravekeeperReboot.Source.Entities {
	public abstract class Prefab {
		public virtual string Type { get; private set; }
		public abstract Entity Instantiate(Scene scene);
	}
}