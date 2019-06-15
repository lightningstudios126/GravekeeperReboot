using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Tiled;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;
using System.Collections.Generic;
using System.Linq;

namespace GravekeeperReboot.Source {
	public class GameBoard : SceneComponent {
		public const string EntityName = "tileMapEntity";

		Entity tileMapEntity;
		TiledMapComponent mapComponent;
		TiledTile exit;

		List<Entity> tileEntities;
		List<TiledTile> graveStones;

		public Vector2 Center => mapComponent.bounds.center;

		public override void onEnabled() {
			base.onEnabled();

			if (tileMapEntity == null)
				tileMapEntity = scene.createEntity(EntityName);
			if (!tileMapEntity.HasComponent<TiledMapComponent>())
				tileMapEntity.addComponent(new TiledMapComponent(null));

			mapComponent = tileMapEntity.getComponent<TiledMapComponent>();

			tileEntities = new List<Entity>();
			graveStones = new List<TiledTile>();
		}

		public void LoadLevel(int world, int level) {
			LoadLevel($@"Tilemaps\map{world}-{level}");
		}

		public void LoadLevel(string location) {
			mapComponent.tiledMap = scene.content.Load<TiledMap>(location);

			TiledTileLayer spawnLayer = mapComponent.tiledMap.getLayer<TiledTileLayer>(TiledMapConstants.LAYER_SPAWNS);
			spawnLayer.visible = false;
			List<TiledTile> spawnTiles = spawnLayer.tiles.ToList();
			spawnTiles.RemoveAll(t => t == null);

			foreach (TiledTile tile in spawnTiles) {
				string type = tile.tilesetTile.properties[TiledMapConstants.PROPERTY_TYPE];
				Entity e = Prefabs.prefabs[type].Instantiate(scene, Vector2.Zero);
				e.getComponent<TileComponent>().Initialize(this, WorldToTilePosition(tile.getWorldPosition(mapComponent.tiledMap)));
				tileEntities.Add(e);
			}

			List<TiledTile> floorTiles = mapComponent.tiledMap.getLayer<TiledTileLayer>(TiledMapConstants.LAYER_FLOOR).tiles.ToList();
			floorTiles.RemoveAll(t => t == null);

			exit = floorTiles.Find(t => t.tilesetTile.properties[TiledMapConstants.PROPERTY_TYPE] == TiledMapConstants.TYPE_EXIT);
			graveStones = floorTiles.FindAll(t => t.tilesetTile.properties[TiledMapConstants.PROPERTY_TYPE] == TiledMapConstants.TYPE_GRAVESTONE_FULL);
		}

		public bool CanPush(TileComponent tile, TileDirection direction) {
			return EmptyAtLocation(tile.tilePosition + Directions.Offset(direction));
		}

		public bool CanRotate(TileComponent tile, TileComponent pivot, TileDirection direction) {
			var offset = Directions.Offset(Directions.DirAdd(pivot.tileDirection, direction));

			var targetPosition = EmptyAtLocation(pivot.tilePosition + offset);
			var block = EmptyAtLocation(tile.tilePosition + offset);

			return targetPosition && block;
		}

		public Vector2 TileToWorldPosition(Point tilePos) {
			return mapComponent.tiledMap.tileToWorldPosition(tilePos) + new Vector2(8, 8);
		}

		public Point WorldToTilePosition(Vector2 worldPos) {
			return mapComponent.tiledMap.worldToTilePosition(worldPos);
		}

		public Entity FindAtLocation(Point tilePos) {
			return tileEntities.Find(e => e.getComponent<TileComponent>().tilePosition == tilePos);
		}

		public bool EmptyAtLocation(Point tilePos) {
			return FindAtLocation(tilePos) == null;
		}

		public bool GroundAtLocation(Point tilePosition) {
			return mapComponent.getTileAtWorldPosition(tilePosition.ToVector2()) != null;
		}
	}
}