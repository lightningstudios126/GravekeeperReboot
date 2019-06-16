using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Entities {
	public abstract class Prefab {
		public virtual string Type { get; private set; }

		public TileEntity Instantiate(Scene scene, Point position) {
			TileEntity entity = new TileEntity();
			Instantiate(entity, scene, position);
			entity.position = Tiled.TiledMapConstants.TileToWorldPosition(position) + Tiled.TiledMapConstants.ENTITY_OFFSET;
			scene.addEntity(entity);
			return entity;
		}
		protected abstract void Instantiate(TileEntity entity, Scene scene, Point position);
	}
}