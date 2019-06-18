using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Entities;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	class SoulSystem : EntitySystem {
		private GameBoard gameBoard;
		private CommandSystem commandSystem;

		public SoulSystem(Matcher matcher) : base(matcher) { }

		protected override void process(List<Entity> entities) {
			base.process(entities);
			if (gameBoard == null) gameBoard = scene.getSceneComponent<GameBoard>();
			if (commandSystem == null) commandSystem = scene.getEntityProcessor<CommandSystem>();

			foreach (Entity entity in entities) {

				try {
					var soul = ((TileEntity)entity);
					if (soul.movability == 0)
						continue;

					if (!soul.isBeingGrabbed && soul.tag == (int)Tags.Soul) {
						if (gameBoard.GravestoneAtLocation(soul.tilePosition)) {
							commandSystem.QueueCommand(new BuryCommand(soul));
						}
					}
				}
				catch (System.InvalidCastException e) { }
			}
		}
	}
}
