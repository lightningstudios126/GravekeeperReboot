using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace GravekeeperReboot.Source.Entities {
	public class TileEntity : Entity {
		/// <summary>
		/// position of this entity in grid space. This might not match exactly with the equivalent world position due to animation
		/// </summary>
		public Point tilePosition;

		/// <summary>
		/// rotation of this entity snapped to the cardinal directions. This might not match exactly with the world rotation due to animation
		/// </summary>
		public TileDirection tileDirection;

		/// <summary>
		/// which movement operations this entity can undergo
		/// </summary>
		public MovabilityFlags movability;

		public GameBoard gameBoard;

		public TileEntity() : base() { }
		public TileEntity(string name) : base(name) { }

		public override Entity clone(Vector2 position = default(Vector2)) {
			TileEntity entity = base.clone() as TileEntity;
			entity.tilePosition = tilePosition;
			entity.tileDirection = tileDirection;
			entity.movability = movability;

			return entity;
		}

		/// <summary>
		/// determines if this entity can be pushed in a certain direction.
		/// Returns <see langword="false"/> if not assigned to a <see cref="GameBoard"/>.
		/// </summary>
		/// <param name="direction">the direction to move in</param>
		/// <returns>whether this entity can be pushed in that direction</returns>
		public bool CanPush(TileDirection direction) {
			if (!movability.HasFlag(MovabilityFlags.Pushable)) return false;
			if (gameBoard == null) return false;
			
			return gameBoard.EmptyAtLocation(tilePosition + direction.Offset());
		}

		/// <summary>
		/// determines if this entity can be pivoted around <paramref name="pivot"/>.
		/// Returns <see langword="false"/> if not assigned to a <see cref="GameBoard"/>.
		/// </summary>
		/// <param name="pivot">the point to pivot around. Must be adjacent.</param>
		/// <param name="direction">the direction to pivot in. Must be <see cref="TileDirection.LEFT"/> or <see cref="TileDirection.RIGHT"/>.</param>
		/// <returns>whether this entity can be pivoted in that direction</returns>
		public bool CanPivot(Point pivot, TileDirection direction) {
			if (Math.Abs(pivot.X - tilePosition.X) + Math.Abs(pivot.Y - tilePosition.Y) != 1)
				throw new ArgumentOutOfRangeException("Pivot is too distant from entity position");
			if ((direction != TileDirection.LEFT) && (direction != TileDirection.RIGHT))
				throw new ArgumentOutOfRangeException("Direction is not left or right");
			if (gameBoard == null) return false;
			if (!movability.HasFlag(MovabilityFlags.Pivotable)) return false;

			var offset = Directions.OffsetDirection(tilePosition-pivot).Add(direction).Offset();

			// Check that a 1x2 area is free in the direction of the pivot
			bool targetPosition = gameBoard.EmptyAtLocation(pivot + offset);
			bool block = gameBoard.EmptyAtLocation(tilePosition + offset);

			return targetPosition && block;
		}

		public void UpdateWorldPosition() {
			position = Tiled.TiledMapConstants.TileToWorldPosition(tilePosition);
		}
	}
}
