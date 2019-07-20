
namespace FFLogsAnalyser
{
    #region Region enum

    /// <summary>
    /// list of different Regions in ffxiv
    /// </summary>
    public enum Region
    {
        EU,
        NA,
        JP
    }

    #endregion

    #region EU Servers

    /// <summary>
    /// List of servers in the EU region
    /// </summary>
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

    /// <summary>
    /// List of Servers in the NA Region
    /// </summary>
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

    /// <summary>
    /// List of Servers in the JP region
    /// </summary>
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

    /// <summary>
    /// List of buff names (for use in the Buff timeline) where '_' = ' '
    /// </summary>
    public enum Buffs
    {
        Battle_Litany,
        Trick_Attack,
        Ley_Lines,
        Battle_Voice,
        Chain_Strategem,
        Blood_For_Blood,
        Vulnerability_Up
    }

    #endregion

    #region Buff Colours

    public enum BuffColours
    {
        LightBlue,
        Orange,
        Purple,
        Teal,
        DarkBlue,
        Red,
        DarkOrange,
    }

    #endregion

    #region Timeline Colours

    /// <summary>
    /// List of colours to identify the different parses in the timeline
    /// </summary>
    public enum TimeLineColours
    {
        Pink,
        Blue,
        Green,
        Yellow,
        Purple
    }
    #endregion
}
