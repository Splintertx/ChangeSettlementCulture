using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;

namespace ChangeSettlementCulture
{
    class SettlementVariablesBehaviorMod : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener((object)this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChangedMod));
            CampaignEvents.ClanChangedKingdom.AddNonSerializedListener((object)this, new Action<Clan, Kingdom, Kingdom, bool, bool>(this.OnClanChangedKingdomMod));
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.OnGameLoaded));
        }

        public override void SyncData(IDataStore dataStore)
        {

        }

        public void OnSettlementOwnerChangedMod(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
        {
            if (!Campaign.Current.GameStarted)
                return;
            
            if (oldOwner.Clan.Kingdom.Culture.StringId != newOwner.Clan.Kingdom.Culture.StringId)
            {
                changeSettlementCulture(settlement, newOwner.Culture);
            }
        }

        public void OnClanChangedKingdomMod(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, bool byRebellion, bool showNotification)
        {
            if (!clan.Settlements.Any())
                return;

            if (oldKingdom.Culture.StringId != newKingdom.Culture.StringId)
            {
                foreach (var settlement in clan.Settlements.Where(x => x.IsVillage || x.IsCastle || x.IsTown))
                {
                    changeSettlementCulture(settlement, newKingdom.Culture);
                }
            }
        }

        public void OnGameLoaded(CampaignGameStarter starter)
        {
            if (!Campaign.Current.GameStarted || Campaign.Current.Settlements == null)
                return;

            foreach (Settlement settlement in Campaign.Current.Settlements.Where(x => x.IsVillage || x.IsCastle || x.IsTown))
            {
                //try
                //{
                    if (settlement.Culture.StringId != settlement.OwnerClan.Culture.StringId)
                    {
                        changeSettlementCulture(settlement, settlement.OwnerClan.Kingdom.Culture);
                    }
                //}
                //catch (Exception ex)
                //{
                //    System.Windows.Forms.MessageBox.Show("Error attempting to change a settlement's culture: " + String.Join(",", settlement.Name, settlement.OwnerClan.Kingdom.Culture, ex.Message));
                //    return;
                //}
            }
        }

        void changeSettlementCulture(Settlement settlement, CultureObject culture)
        {
            var editableSettlement = Campaign.Current.Settlements.Where(x => x.StringId == settlement.StringId).FirstOrDefault();
            editableSettlement.Culture = culture;

            if (!settlement.IsVillage && !settlement.IsFortification)
                return;

            settlement.CalculateSettlementValueForFactions();
        }
    }
}
