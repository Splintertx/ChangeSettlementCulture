//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using TaleWorlds.CampaignSystem;
//using TaleWorlds.CampaignSystem.Actions;
//using TaleWorlds.CampaignSystem.GameMenus;
//using TaleWorlds.CampaignSystem.Overlay;
//using TaleWorlds.Library;
//using TaleWorlds.Core;
//using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.Towns;
//using TaleWorlds.Localization;

//namespace ChangeSettlementCulture
//{
//    class ChangeCultureBehavior : CampaignBehaviorBase
//    {
//        List<CultureObject> cultures = new List<CultureObject>();

//        public override void RegisterEvents()
//        {
//            CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
//            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.OnGameLoaded));
//            CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener((object)this, new Action<MapEvent>(this.OnBattleEnded));
//        }

//        public override void SyncData(IDataStore dataStore)
//        {

//        }

//        public void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
//        {
//            this.AddGameMenus(campaignGameStarter);
//        }

//        public void OnGameLoaded(CampaignGameStarter campaignGameStarter)
//        {
//            this.AddGameMenus(campaignGameStarter);
//        }

//        protected void AddGameMenus(CampaignGameStarter campaignGameStarter)
//        {
//            foreach (Kingdom kingdom in Game.Current.ObjectManager.GetObjectTypeList<Kingdom>().ToList<Kingdom>())
//            {
//                if (kingdom.IsKingdomFaction || kingdom.Name.ToString() == "Looters")
//                    cultures.Add(kingdom.Culture);
//            }

//            campaignGameStarter.AddGameMenuOption("village", "change_culture", "Change Culture", new GameMenuOption.OnConditionDelegate(on_condition_delegate), new GameMenuOption.OnConsequenceDelegate(on_consequence_delegate), false, -1, false);
//            campaignGameStarter.AddGameMenuOption("town", "change_culture", "Change Culture", new GameMenuOption.OnConditionDelegate(on_condition_delegate), new GameMenuOption.OnConsequenceDelegate(on_consequence_delegate), false, -1, false);
//        }

//        private bool on_condition_delegate(MenuCallbackArgs args)
//        {
//            return true;
//        }

//        private void on_consequence_delegate(MenuCallbackArgs args)
//        {
//            try
//            {
//                var oldCulture = Settlement.CurrentSettlement.Culture;
//                var newCulture = cultures[(cultures.IndexOf(Settlement.CurrentSettlement.Culture) + 1) % cultures.Count];

//                System.Windows.Forms.MessageBox.Show("Attempting to change settlement culture from " + oldCulture.Name + " to " + newCulture.Name);
//                var settlement = Settlement.CurrentSettlement;

//                var editableSettlement = Campaign.Current.Settlements.Where(x => x.StringId == Settlement.CurrentSettlement.StringId).FirstOrDefault();
//                editableSettlement.Culture = newCulture;


//                foreach (Hero notable in editableSettlement.Notables)
//                {
//                    for (int index = 0; index < 6; ++index)
//                    {
//                        notable.VolunteerTypes = new CharacterObject[6];
//                        if (HeroHelper.HeroCanRecruitFromHero(Hero.MainHero, notable, index))
//                            ++num;
//                    }
//                }

//                settlement.Culture = cultures[(cultures.IndexOf(settlement.Culture) + 1) % cultures.Count];
//                System.Windows.Forms.MessageBox.Show("Success");
//            }
//            catch (Exception ex)
//            {
//                System.Windows.Forms.MessageBox.Show("Failed\n\n" + ex.Message);
//            }
//        }
//    }
//}
