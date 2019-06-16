using System;

namespace GravekeeperReboot.Source.Entities {
	/// <summary>
	/// flag enum that defines what movement operations this entity can undergo.
	/// </summary>
	[Flags]
	public enum MovabilityFlags {
		Grabbable = 1 << 0,
		Pushable = 1 << 1,
		Pivotable = 1 << 2
	}
}