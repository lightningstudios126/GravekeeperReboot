using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Tiled;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GravekeeperReboot.Source {
	public class GameBoard : SceneComponent {
		public const string EntityName = "tileMapEntity";

		Entity tileMapEntity;
		TiledMapComponent mapComponent;
		TiledTile exit;

		List<TileEntity> tileEntities;
		List<TiledTile> graveStones;

		List<TiledTile> floorTiles;

		public Vector2 Center => mapComponent.bounds.center;

		public override void onEnabled() {
			base.onEnabled();

			if (tileMapEntity == null)
				tileMapEntity = scene.createEntity(EntityName);
			if (!tileMapEntity.HasComponent<TiledMapComponent>())
				tileMapEntity.addComponent(new TiledMapComponent(null));

			mapComponent = tileMapEntity.getComponent<TiledMapComponent>();

			tileEntities = new List<TileEntity>();
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
				Point tilePosition = new Point(tile.x, tile.y);
				TileEntity e = Prefabs.prefabs[type].Instantiate(scene, tilePosition);
				e.gameBoard = this;
				tileEntities.Add(e);
			}

			floorTiles = mapComponent.tiledMap.getLayer<TiledTileLayer>(TiledMapConstants.LAYER_FLOOR).tiles.ToList();
			floorTiles.RemoveAll(t => t == null);

			exit = floorTiles.Find(t => t.tilesetTile.properties[TiledMapConstants.PROPERTY_TYPE] == TiledMapConstants.TYPE_EXIT);
			graveStones = floorTiles.FindAll(t => t.tilesetTile.properties[TiledMapConstants.PROPERTY_TYPE] == TiledMapConstants.TYPE_GRAVESTONE_EMPTY);
			Console.WriteLine(graveStones.First().x + ", " + graveStones.First().y);
		}

		public TileEntity FindAtLocation(Point tilePos) {
			return tileEntities.Find(e => e.tilePosition == tilePos);
		}

		public bool EmptyAtLocation(Point tilePos) {
			return FindAtLocation(tilePos) == null;
		}

		public bool GroundAtLocation(Point tilePosition) {
			return floorTiles.Any(t => new Point(t.x, t.y) == tilePosition);
		}

		public bool ExitAtLocation(Point tilePosition) {
			return exit.getWorldPosition(mapComponent.tiledMap).roundToPoint() == tilePosition;
		}

		public bool GravestoneAtLocation(Point tilePosition) {
			return graveStones.Any(g => new Point(g.x, g.y) == tilePosition);
		}
	}
}