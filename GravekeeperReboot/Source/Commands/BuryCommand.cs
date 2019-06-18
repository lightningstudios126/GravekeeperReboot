using GravekeeperReboot.Source.Entities;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Source.Commands {
	public class BuryCommand : Command {
		private GameBoard gameboard;
		private TileEntity entity;
		private MovabilityFlags movability;

		private Texture2D fullGravestone;
		private Texture2D soul;

		public BuryCommand(TileEntity entity) {
			this.gameboard = entity.scene.getSceneComponent<GameBoard>();
			this.entity = entity;

			fullGravestone = entity.scene.content.Load<Texture2D>(Content.Sprites.Tiles.fullGravestone);
			soul = entity.scene.content.Load<Texture2D>(Content.Sprites.Tiles.soul);
		}

		public override void Execute() {
			this.movability = entity.movability;
			entity.movability = 0;
			entity.getComponent<Sprite>().setSubtexture(new Nez.Textures.Subtexture(fullGravestone));
		}

		public override void Undo() {
			entity.movability = this.movability;
			entity.getComponent<Sprite>().setSubtexture(new Nez.Textures.Subtexture(soul));
		}
	}
}
