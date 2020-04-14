using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

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

            this.AddBehaviors((CampaignGameStarter)gameStarter);
        }

        private void AddBehaviors(CampaignGameStarter gameStarter)
        {
            //gameStarter.AddBehavior((CampaignBehaviorBase)new ChangeCultureBehavior());
            gameStarter.AddBehavior(new SettlementVariablesBehaviorMod());
        }
    }
}