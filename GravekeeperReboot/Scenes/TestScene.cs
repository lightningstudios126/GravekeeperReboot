using Components;
using Entities;
using Nez;
using Systems;

namespace GravekeeperReboot.Scenes {
    class TestScene : Scene {
        Player player;

        public override void initialize() {
            base.initialize();
            player = Player.CreatePlayer(this);
            addEntityProcessor(new MoveSystem(new Matcher().all(typeof(MoveComponent))));

            addRenderer(new DefaultRenderer());
        }
    }
}
