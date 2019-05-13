using Nez;

namespace GravekeeperReboot.Source.Entities {
	public static class EntityExtensions {
		public static bool HasComponent<T>(this Entity entity) where T: Component {
			return entity.getComponent<T>() != null;
		}
	}
}
