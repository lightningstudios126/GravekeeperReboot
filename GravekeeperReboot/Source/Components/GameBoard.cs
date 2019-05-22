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
		List<Point> graveStones;

		public Vector2 Center => mapComponent.bounds.center;

		public override void onEnabled() {
			base.onEnabled();

			if (tileMapEntity == null)
				tileMapEntity = scene.createEntity(EntityName);
			if (!this.tileMapEntity.HasComponent<TiledMapComponent>())
				tileMapEntity.addComponent(new TiledMapComponent(null));

			mapComponent = tileMapEntity.getComponent<TiledMapComponent>();

			tileEntities = new List<Entity>();
			graveStones = new List<Point>();
		}

		public void LoadLevel(int world, int level) {
			LoadLevel($@"Tilemaps\map{world}-{level}");
		}

		public void LoadLevel(string location) {
			mapComponent.tiledMap = scene.content.Load<TiledMap>(location);

			foreach (TiledObject obj in mapComponent.tiledMap.getObjectGroup(TiledMapConstants.Spawns).objects) {

				Entity e = Prefabs.prefabs[obj.type].Instantiate(scene, Vector2.Zero);
				e.getComponent<TileComponent>().Initialize(this, WorldToTilePosition(obj.position));
				tileEntities.Add(e);
			}

			TiledObject exitObject = mapComponent.tiledMap.getObjectGroup(TiledMapConstants.Markers).objectsWithType(TiledMapConstants.Exit)[0];
			exit = exitObject.position.roundToPoint();

			foreach (TiledObject gravestone in mapComponent.tiledMap.getObjectGroup(TiledMapConstants.Markers).objectsWithType(TiledMapConstants.Gravestone)) {
				graveStones.Add(gravestone.position.roundToPoint());
			}
		}

		public Vector2 TileToWorldPosition(Point tilePos) {
			return mapComponent.tiledMap.tileToWorldPosition(tilePos) + new Vector2(8, -8);	
		}

		public Point WorldToTilePosition(Vector2 worldPos) {
			return mapComponent.tiledMap.worldToTilePosition(worldPos);
		}

		public Entity FindAtLocation(Point tilePos) {
			return tileEntities.Find(e => e.getComponent<MoveComponent>().position == tilePos);
		}
	}
}