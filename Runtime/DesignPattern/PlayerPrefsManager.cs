using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace GiangCustom.DesignPattern
{
    public class PlayerPrefsManager
    {
        private static string format = "yyyy-MM-dd HH:mm:ss";
        private const string CURRENT_LEVEL = "current_level";
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

        #region MyRegion

        public const string PREFS_COIN = "coin";
        public const string PREFS_BGSOUND = "BGsound";
        public const string PREFS_SOUND = "sound";
        public const string PREFS_VIBRATE = "VibrateSound";
        

        public static int Coin
        {
            get => PlayerPrefs.GetInt(PREFS_COIN, 100);
            set => PlayerPrefs.SetInt(PREFS_COIN, value);
        }

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

        #region brush

        private const string brush = "brush-ads-watch";

        public static int[] AdsCheckForBrush
        {
            get => GetArray<int>(brush);
            set => SetArray(brush, value);
        }
        
        public static void SetAdsCheckForBrush(int index ,int value)
        {
            var ads = AdsCheckForBrush;
            if (index < 0 || index >= ads.Length)
            {
                Debug.LogError("Index out of bounds when setting AdsCheckForBrush value.");
                return;
            }
            ads[index] = value;
            SetArray(brush, ads);
        }
        
        private const string currentBrush = "current-brush";
        
        public static int CurrentBrush
        {
            get => PlayerPrefs.GetInt(currentBrush, 0);
            set => PlayerPrefs.SetInt(currentBrush, value);
        }
        #endregion
        
        public static T[] GetArray<T>(string key)
        {
            var json = PlayerPrefs.GetString(key);
            return string.IsNullOrEmpty(json) ? Array.Empty<T>() : JsonHelper.getJsonArray<T>(json);
        }

        public static void SetArray<T>(string key, T[] value)
        {
            PlayerPrefs.SetString(key, JsonHelper.arrayToJson(value));
        }
//===================================
        #region UserConfig
        private const string DecorRoom = "decor-room-";
        public static int[] GetDecorRoom(string key)
        {
           return GetArray<int>(string.Concat(DecorRoom,key));
        }
        public static int[] SetDecorRoom(string key, int[] array)
        {
            Debug.Log(string.Concat(DecorRoom,key));
            SetArray(string.Concat(DecorRoom,key), array);
            return GetDecorRoom(key);
        }

        public static void SetUserDataForToiletBuild(int index ,int value, string key)
        {
            var array = GetDecorRoom(key);
            if (index < 0 || index >= array.Length)
            {
                Debug.LogError("Index out of bounds when setting AdsCheckForBrush value " + key);
                return;
            }
            array[index] = value;
            SetDecorRoom(key, array);
        }
        
        //================ skins =====================
        public const string SkinOwner = "skin-owner-";
        public const string Male = "male";
        public const string Female = "female";

        public static int[] GetSkinOwner(string key)
        {
            var skins = GetArray<int>(string.Concat(SkinOwner, key));
            if (skins.Length == 0)
            {
                skins = new[] { 0 };
            }
            SetArray(string.Concat(SkinOwner, key), skins);
            return skins;
        }
        
        public static void AddSkinOwner(int value, string key)
        {
            var tmpLst = GetSkinOwner(key).ToList();
            if (tmpLst.Contains(value)) return;
            tmpLst.Add(value);
            SetArray(string.Concat(SkinOwner, key), tmpLst.ToArray());
        }

        public static bool HasSkinOwner(int value, string key)
        {
            var tmpLst = GetSkinOwner(key).ToList();
            return tmpLst.Contains(value);
        }
        
        private const string SkinMaleUsingKey = "skin-male-using";
        private const string SkinFemaleUsingKey = "skin-female-using";

        public static int SkinMaleUsing
        {
            get => PlayerPrefs.GetInt(SkinMaleUsingKey, 0);
            set => PlayerPrefs.SetInt(SkinMaleUsingKey, value);
        }
        
        public static int SkinFemaleUsing
        {
            get => PlayerPrefs.GetInt(SkinFemaleUsingKey, 0);
            set => PlayerPrefs.SetInt(SkinFemaleUsingKey, value);
        }

        //=============================================
        #endregion
//===================================

    }
}