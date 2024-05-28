using System;
using UnityEngine;

namespace GiangCustom.DesignPattern
{
    public class PlayerPrefsManager
    {
        private static string format = "yyyy-MM-dd HH:mm:ss";
        private const string CURRENT_LEVEL = "current_level";
        private const string OPEN_LEVEL = "open_level";
        private const string DATETIME_START = "datetime_start";

        public static DateTime DatetimeStart
        {
            get
            {
                string dateTm = PlayerPrefs.GetString(DATETIME_START, DateTime.Now.ToString(format));
            
                DateTime firstDate = DateTime.ParseExact(dateTm, 
                    format,
                    null);
            
                return firstDate;
            }
            set
            {
                string dateTm = value.ToString(format);
            
                PlayerPrefs.SetString(DATETIME_START, dateTm);
            }
        }

        public static int CurrentLevel
        {
            get => PlayerPrefs.GetInt(CURRENT_LEVEL, 1);
            set => PlayerPrefs.SetInt(CURRENT_LEVEL, value);
        }

        public static int OpenLevel
        {
            get => PlayerPrefs.GetInt(OPEN_LEVEL, 0);
            set => PlayerPrefs.SetInt(OPEN_LEVEL, value);
        }

        #region MyRegion

        public const string PREFS_COIN = "coin";
        public const string PREFS_BGSOUND = "BGsound";
        public const string PREFS_SOUND = "sound";
        public const string PREFS_VIBRATE = "VibrateSound";
    
    
    
        private const string PREF_CURRENT_DAY = "current_day";
        private const string PREFS_AMOUT_DAILY_CLAIMED = "amout_daily_claimed";
        private const string PREF_DAY_REWARD = "day_reward";
        private const string PREF_FIRST_SET_DATA_DAILY = "first_set_data_daily";
        private const string PREF_CHECK_CLAIM_DAILY_REWARD= "check_claim_daily_reward";

        private const string PREF_FIRST_SET_DATA_BG = "first_set_data_background";
        private const string PREF_BACKGROUND = "background";
    
        private const string PREF_HAIR = "listHairs";
        private const string PREF_PROMDRESS = "listPromDresss";
        private const string PREF_CLOTHING = "listClothings";
        private const string PREF_PANT_SKIRT = "listPantsSkirts";
        private const string PREF_SOCK = "listSockss";
        private const string PREF_SHOES = "listShoess";
        private const string PREF_HAIR_ACCESSORIESS = "listHairAccessoriess";
        private const string PREF_EARRING = "listEarrings";
        private const string PREF_NECKBAND_ACCESSORIES = "listNeckbandAccessoriess";
        private const string PREF_FACESTICKER = "listFaceStickers";
        private const string PREF_PERSON_OR_HAND_ACCESSORIES = "listPersonOrHandAccessoriess";


        public static string CurrentDay
        {
            get => PlayerPrefs.GetString(PREF_CURRENT_DAY);
            set => PlayerPrefs.SetString(PREF_CURRENT_DAY, value);
        }
        public static int AmoutDailyClaimed
        {
            get => PlayerPrefs.GetInt(PREFS_AMOUT_DAILY_CLAIMED, -1);
            set => PlayerPrefs.SetInt(PREFS_AMOUT_DAILY_CLAIMED, value);
        }
        public static int SetData
        {
            get => PlayerPrefs.GetInt(PREF_FIRST_SET_DATA_DAILY,0);
            set => PlayerPrefs.SetInt(PREF_FIRST_SET_DATA_DAILY, value);
        }
        public static bool[] DayReward
        {
            // get => SDKPlayerPrefs.GetBoolArray(PREF_DAY_REWARD);
            // set => SDKPlayerPrefs.SetBoolArray(PREF_DAY_REWARD, value);
            get => GetBoolArray(PREF_DAY_REWARD);
            set => SetBoolArray(PREF_DAY_REWARD, value);
        }
        public static int SetBG
        {
            get => PlayerPrefs.GetInt(PREF_FIRST_SET_DATA_BG,0);
            set => PlayerPrefs.SetInt(PREF_FIRST_SET_DATA_BG, value);
        }
        public static bool[] BackGround
        {
            get => GetBoolArray(PREF_BACKGROUND);
            set => SetBoolArray(PREF_BACKGROUND, value);
        }
    
        public static int OffAdBackGround
        {
            set
            {
                bool[] bgs = BackGround;
                bgs[value] = false;
                BackGround = bgs;
            }
        }

        #region dataadforlevel

        public static bool[] listHairs
        {
            get => GetBoolArray(PREF_HAIR);
            set => SetBoolArray(PREF_HAIR, value);
        }
        public static bool[] listPromDresss
        {
            get => GetBoolArray(PREF_PROMDRESS);
            set => SetBoolArray(PREF_PROMDRESS, value);
        }
        public static bool[] listClothings
        {
            get => GetBoolArray(PREF_CLOTHING);
            set => SetBoolArray(PREF_CLOTHING, value);
        }
        public static bool[] listPantsSkirts
        {
            get => GetBoolArray(PREF_PANT_SKIRT);
            set => SetBoolArray(PREF_PANT_SKIRT, value);
        }
        public static bool[] listSockss
        {
            get => GetBoolArray(PREF_SOCK);
            set => SetBoolArray(PREF_SOCK, value);
        }
        public static bool[] listShoess
        {
            get => GetBoolArray(PREF_SHOES);
            set => SetBoolArray(PREF_SHOES, value);
        }
        public static bool[] listHairAccessoriess
        {
            get => GetBoolArray(PREF_HAIR_ACCESSORIESS);
            set => SetBoolArray(PREF_HAIR_ACCESSORIESS, value);
        }
        public static bool[] listEarrings
        {
            get => GetBoolArray(PREF_EARRING);
            set => SetBoolArray(PREF_EARRING, value);
        }
        public static bool[] listNeckbandAccessoriess
        {
            get => GetBoolArray(PREF_NECKBAND_ACCESSORIES);
            set => SetBoolArray(PREF_NECKBAND_ACCESSORIES, value);
        }
        public static bool[] listFaceStickers
        {
            get => GetBoolArray(PREF_FACESTICKER);
            set => SetBoolArray(PREF_FACESTICKER, value);
        }
        public static bool[] listPersonOrHandAccessoriess
        {
            get => GetBoolArray(PREF_PERSON_OR_HAND_ACCESSORIES);
            set => SetBoolArray(PREF_PERSON_OR_HAND_ACCESSORIES, value);
        }

        #endregion

        public static bool CheckClaim
        {
            get => PlayerPrefs.GetInt(PREF_CHECK_CLAIM_DAILY_REWARD, 1) == 1;
            set => PlayerPrefs.SetInt(PREF_CHECK_CLAIM_DAILY_REWARD, value ? 1 : 0);
        }

        public static int Coin
        {
            get => PlayerPrefs.GetInt(PREFS_COIN);
            set => PlayerPrefs.SetInt(PREFS_COIN, value);
        }
   
        // public static int NoAds
        // {
        //     get => PlayerPrefs.GetInt(StringConstants.REMOVE_ADS, 0);
        //     set => PlayerPrefs.SetInt(StringConstants.REMOVE_ADS, value);
        // }

        public static bool Sound
        {
            get => PlayerPrefs.GetInt(PREFS_SOUND, 1) == 1;
            set => PlayerPrefs.SetInt(PREFS_SOUND, value ? 1 : 0);
        }
    
        public static bool BGSound
        {
            get => PlayerPrefs.GetInt(PREFS_BGSOUND, 1) == 1;
            set => PlayerPrefs.SetInt(PREFS_BGSOUND, value ? 1 : 0);
        }
    
        public static bool VibrateSound
        {
            get => PlayerPrefs.GetInt(PREFS_VIBRATE, 1) == 1;
            set => PlayerPrefs.SetInt(PREFS_VIBRATE, value ? 1 : 0);
        }

        #endregion
    
        public static bool[] GetBoolArray(string Prefs)
        {
            string[] tmp = PlayerPrefs.GetString(Prefs).Split("|"[0]);
            if(tmp.Length != 0)
            {
                bool[] myBool = new bool[tmp.Length - 1];
                for (int i = 0; i < tmp.Length - 1; i++)
                {
                    myBool[i] = bool.Parse(tmp[i]);
                }
                return myBool;
            }
            return new bool[0];
        }
        public static void SetBoolArray(string Prefs, bool[] _Value)
        {
            string Value = "";
            for (int y = 0; y < _Value.Length; y++) {
                Value += _Value[y].ToString() + "|"; 
            }
            PlayerPrefs.SetString(Prefs, Value);
        }
    }
}