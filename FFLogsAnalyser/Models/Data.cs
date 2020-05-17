
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
        Devotion = 1001213,
        Embolden = 1001297,
        Technical_Finish = 1001822,
        Battle_Litany = 1000786,
        Battle_Voice = 1000141,
        Chain_Stratagem = 1001221,        
        Medicated = 1000049,
        Left_Eye = 1001454,
        Brotherhood = 1001185,
        Vulnerability_Up = 1000638,
        Trick_Attack,
        Riddle_of_Fire = 1001181,
        Lance_Charge = 1001864,
        Ley_Lines = 1000737,
        Presence_of_Mind = 1000157,
        Recitation = 1001896,
        Raging_Strikes = 1000125,
        Wildfire = 1000861,
        Devilment = 1001825,
    }

    /// <summary>
    /// List of personal buff names (used in the timeline)
    /// </summary>
    public enum PersonalBuffs
    {
        Riddle_of_Fire = 1001181,
        Lance_Charge = 1001864,
        Ley_Lines = 1000737,
        Presence_of_Mind = 1000157,
        Recitation = 1001896,
        Raging_Strikes = 1000125,
        Wildfire = 1000861,
        Devilment = 1001825,
    }

    #endregion

    #region Buff Colours
    

    public enum BuffColours
    {
        LightYellow = 1001213,
        Indigo = 1001297,
        Salmon = 1001822,
        LightBlue = 1000786,
        Teal = 1000141,
        DarkBlue = 1001221,
        Silver = 1000049,
        DarkRed = 1001454,
        OrangeRed = 1001185,
        NoColour = 1000638,
        DarkOrange,
        Maroon = 1001181,
        Red = 1001864,
        Purple = 1000737,
        Magenta = 1000157,
        HotPink = 1001896,
        MediumVioletRed = 1000125,
        Gold = 1000861,
        LightGreen = 1001825,
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
