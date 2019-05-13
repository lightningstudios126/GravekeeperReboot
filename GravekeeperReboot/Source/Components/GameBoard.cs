using GravekeeperReboot.Source.Entities;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravekeeperReboot.Source {
	class GameBoard : SceneComponent {
		public const string EntityName = "tileMapEntity";

		Entity tileMapEntity;
		TiledMapComponent mapComponent;
		Point exit;

		public Vector2 Center => mapComponent.bounds.center;

		public override void onEnabled() {
			base.onEnabled();
			if (tileMapEntity == null)
				tileMapEntity = scene.createEntity(EntityName);
			if (this.tileMapEntity.getComponent<TiledMapComponent>() == null)
				tileMapEntity.addComponent(new TiledMapComponent(null));
			mapComponent = tileMapEntity.getComponent<TiledMapComponent>();
		}

		public void LoadLevel(int world, int level) {
			LoadLevel($@"Tilemaps\map{world}-{level}");
		}

		public void LoadLevel(string location) {
			mapComponent.tiledMap = scene.content.Load<TiledMap>(location);
			foreach (TiledObject obj in mapComponent.tiledMap.getObjectGroup(TiledMapConstants.Spawns).objects) {
				Prefabs.prefabs[obj.type].Instantiate(scene, mapComponent.tiledMap.tileToWorldPosition(new Point(obj.x, obj.y)));
			}
			TiledObject exitObject = mapComponent.tiledMap.getObjectGroup(TiledMapConstants.Markers).objectsWithType(TiledMapConstants.Exit)[0];
			exit = new Point(exitObject.x, exitObject.y);
		}

		public Vector2 TileToWorldPosition(Point tilePos) {
			return mapComponent.tiledMap.tileToWorldPosition(tilePos);
		}

		public Point WorldToTilePosition(Vector2 worldPos) {
			return mapComponent.tiledMap.worldToTilePosition(worldPos);
		}
	}

}
