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
		Entity tileMapEntity;
		TiledMapComponent mapComponent;

		public GameBoard (Entity tileMapEntity) {
			this.tileMapEntity = tileMapEntity;
			if (this.tileMapEntity.getComponent<TiledMapComponent>() == null) {
				tileMapEntity.addComponent(new TiledMapComponent(null));
			}
			mapComponent = tileMapEntity.getComponent<TiledMapComponent>();
		}

		public void LoadLevel(int world, int level) {
			LoadLevel($@"Tilemaps\map{world}-{level}");
		}

		public void LoadLevel(string location) {
			mapComponent.tiledMap = scene.content.Load<TiledMap>(location);
			foreach (TiledObject obj in mapComponent.tiledMap.getObjectGroup(TiledMapConstants.Spawns).objects) {
				Console.WriteLine(Prefabs.prefabs["Player"]);
				Prefabs.prefabs[obj.type].Instantiate(scene, mapComponent.tiledMap.tileToWorldPosition(new Point(obj.x, obj.y)));
			}
		}
	}
}
