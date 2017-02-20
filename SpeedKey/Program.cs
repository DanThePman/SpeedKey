using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace SpeedKey
{
    class Program
    {
        private static Menu m;
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += eventArgs =>
            {
                m = MainMenu.AddMenu("SpeedKey", "speedkey");
                m.Add("key", new KeyBind("Key", false, KeyBind.BindTypes.HoldActive));
                m.Add("spellslot", new ComboBox("Slot", new[] { "Q", "W", "E", "R"}));

                Game.OnUpdate += GameOnOnUpdate;
            };
        }

        private static void GameOnOnUpdate(EventArgs args)
        {
            if (m["key"].Cast<KeyBind>().CurrentValue)
            {
                ObjectManager.Player.Spellbook.CastSpell(GetSlot(), false);
                ObjectManager.Player.Spellbook.CastSpell(SpellSlot.Summoner1, false);
                ObjectManager.Player.Spellbook.CastSpell(SpellSlot.Summoner2, false);
                Item.UseItem((int)ItemId.Talisman_of_Ascension);
                Item.UseItem((int)ItemId.Youmuus_Ghostblade);
                Item.UseItem((int)ItemId.Righteous_Glory);
                Item.UseItem((int) ItemId.Mercurial_Scimitar);
            }
        }

        static SpellSlot GetSlot()
        {
            switch (m["spellslot"].Cast<ComboBox>().SelectedIndex)
            {
                case 0: return SpellSlot.Q;
                case 1: return SpellSlot.W;
                case 2: return SpellSlot.E;
                case 3: return SpellSlot.R;
            }

            return SpellSlot.Unknown;
        }
    }
}
