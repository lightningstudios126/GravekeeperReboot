using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravekeeperReboot.Scenes {
	class TestScene : Scene {
		public override void initialize() {
			base.initialize();
			Entity newEntity = createEntity("entityone");
			addRenderer(new DefaultRenderer());
		}

	}
}
