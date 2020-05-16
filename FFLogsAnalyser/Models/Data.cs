
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
        Devotion,
        Embolden,
        Technical_Finish,
        Battle_Litany,
        Battle_Voice,
        Chain_Stratagem,        
        Medicated,
        Dragon_Sight,
        Brotherhood,
        Vulnerability_Up,
        Trick_Attack,
        Riddle_of_Fire,
        Lance_Charge,
        Ley_Lines,
        Presence_of_Mind,
        Recitation,
        Raging_Strikes,
        Wildfire,
        Devilment,
    }

    /// <summary>
    /// List of personal buff names (used in the timeline)
    /// </summary>
    public enum PersonalBuffs
    {
        Riddle_of_Fire,
        Lance_Charge,
        Ley_Lines,
        Presence_of_Mind,
        Recitation,
        Raging_Strikes,
        Wildfire,
        Devilment,
    }

    #endregion

    #region Buff Colours
    

    public enum BuffColours
    {
        LightYellow,
        Indigo,
        Salmon,
        LightBlue,        
        Teal,
        DarkBlue,               
        Silver,
        DarkRed,
        OrangeRed,
        NoColour,
        DarkOrange,
        Maroon,
        Red,
        Purple,
        Magenta,
        HotPink,
        MediumVioletRed,
        Gold,
        LightGreen,
    }

    #endregion

    #region Timeline Colours

    /// <summary>
    /// List of colours to identify the different parses in the timeline
    /// </summary>
    public enum TimeLineColours
    {
        Pink,
        Aqua,
        Green,
        LightYellow,
        Violet,
    }
    #endregion
}
