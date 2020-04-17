using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace ChangeSettlementCulture
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleFolderName = "ChangeSettlementCulture";

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            if (!(game.GameType is Campaign))
                return;

            ((CampaignGameStarter)gameStarter).AddBehavior(new SettlementVariablesBehaviorMod());
        }
    }
}