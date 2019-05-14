using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;
using System.Collections.Generic;

namespace GravekeeperReboot.Source {
	class GameBoard : SceneComponent {
		public const string EntityName = "tileMapEntity";

		Entity tileMapEntity;
		TiledMapComponent mapComponent;
		Point exit;

		List<Entity> tileEntities;

		public Vector2 Center => mapComponent.bounds.center;

		public override void onEnabled() {
			base.onEnabled();
			if (tileMapEntity == null)
				tileMapEntity = scene.createEntity(EntityName);
			if (this.tileMapEntity.getComponent<TiledMapComponent>() == null)
				tileMapEntity.addComponent(new TiledMapComponent(null));
			mapComponent = tileMapEntity.getComponent<TiledMapComponent>();
			tileEntities = new List<Entity>();
		}

		public void LoadLevel(int world, int level) {
			LoadLevel($@"Tilemaps\map{world}-{level}");
		}

		public void LoadLevel(string location) {
			mapComponent.tiledMap = scene.content.Load<TiledMap>(location);
			foreach (TiledObject obj in mapComponent.tiledMap.getObjectGroup(TiledMapConstants.Spawns).objects) {
				System.Console.WriteLine("{0}, {1}", obj.x, obj.y);
				Entity e = Prefabs.prefabs[obj.type].Instantiate(scene, Vector2.Zero);
				tileEntities.Add(e);
				e.getComponent<MoveComponent>().position = WorldToTilePosition(obj.position);
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

		public Entity FindAtLocation(Point tilePos) {
			bool pred(Entity e) {
				if (e.HasComponent<MoveComponent>()) {
					MoveComponent move = e.getComponent<MoveComponent>();
					return move.position == tilePos;
				}
				return false;
			}
			return tileEntities.Find(pred);
		}
	}

}
