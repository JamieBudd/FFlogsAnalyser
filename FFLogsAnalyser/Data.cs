using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFLogsAnalyser
{
    #region Region enum

    //list of different Regions in ffxiv
    public enum Region
    {
        EU,
        NA,
        JP
    }

    #endregion


    #region EU Servers

    //List of servers in the EU region
    public enum ServerEU
    {
        Cerberus,
        Lich,
        Louisoix,
        Moogle,
        Odin,
        Omega,
        Phoenix,
        Ragnarok,
        Shiva,
        Zodiark
    }

    #endregion

    #region NA Servers

    //List of Servers in the NA Region
    public enum ServerNA
    {
        Adamantoise,
        Balmung,
        Cactuar,
        Coeurl,
        Faerie,
        Gilgamesh,
        Goblin,
        Jenova,
        Mateus,
        Midgardsormr,
        Sargatanas,
        Siren,
        Zalera,
        Behemoth,
        Brynhildr,
        Diabolos,
        Excalibur,
        Exodus,
        Famfrit,
        Hyperion,
        Lamia,
        Leviathan,
        Malboro,
        Ultros
    }

    #endregion

    #region JP Servers

    //List of Servers in the JP region
    public enum ServerJP
    {
        Aegis,
        Atomos,
        Carbuncle,
        Garuda,
        Gungnir,
        Kujata,
        Ramuh,
        Tonberry,
        Typhon,
        Unicorn,
        Alexander,
        Bahamut,
        Durandal,
        Fenrir,
        Ifrit,
        Ridill,
        Tiamat,
        Ultima,
        Valefor,
        Yojimbo,
        Zeromus,
        Anima,
        Asura,
        Belias,
        Chocobo,
        Hades,
        Ixion,
        Mandragora,
        Masamune,
        Pandaemonium,
        Shinryu,
        Titan
    }

    #endregion

    #region Buff names
    //List of buff names (for use in the Buff timeline)
    public enum Buffs
    {
        Battle_Litany,
        Trick_Attack,
        Ley_Lines,
        Battle_Voice,
        Chain_Strategem,
        Blood_For_Blood
    }

    #endregion
}
