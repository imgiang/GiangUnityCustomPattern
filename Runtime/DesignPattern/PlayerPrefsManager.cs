using System;
using System.Collections.Generic;
using System.Linq;
using Assembly_CSharp.UI;
using JetBrains.Annotations;
using UnityEngine;

namespace GiangCustom.DesignPattern
{
    public class PlayerPrefsManager
    {
        public static bool isFromWinMenu = false;
        public static GameMode CurrentMode;

        private static string format = "yyyy-MM-dd HH:mm:ss";
        private const string CURRENT_LEVEL = "current_level-";
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

        public static GameMode ModeGameLose = GameMode.None;
        public static int CurrentLevelLose = 0;
        public static int CurrentLevelLoseCount = 0;
        public static int GetCurrentLevel(string key)
        {
            var level = PlayerPrefs.GetInt(string.Concat(CURRENT_LEVEL, key), 1);
            if (key == GameMode.Hotel.ToString())
            {
                if (level > 60)
                {
                    SetCurrentLevel(key, 1);
                }
            }

            if (key == GameMode.Dinner.ToString())
            {
                if (level > 20)
                {
                    SetCurrentLevel(key, 1);
                }
            }

            if (key == GameMode.Store.ToString())
            {
                if (level > 20)
                {
                    SetCurrentLevel(key, 1);
                }
            }

            level = PlayerPrefs.GetInt(string.Concat(CURRENT_LEVEL, key), 1);
            return level;
        }

        public static void SetCurrentLevel(string key, int value)
        {
            PlayerPrefs.SetInt(string.Concat(CURRENT_LEVEL, key), value);
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

        public static void SetAdsCheckForBrush(int index, int value)
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
            return GetArray<int>(string.Concat(DecorRoom, key));
        }

        public static int[] SetDecorRoom(string key, int[] array)
        {
            Debug.Log(string.Concat(DecorRoom, key));
            SetArray(string.Concat(DecorRoom, key), array);
            return GetDecorRoom(key);
        }

        public static void SetUserDataForToiletBuild(int index, int value, string key)
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
        public const string SkinAdCount = "skin-ad-count";
        
        public static int GetSkinAdCount
        {
            get => PlayerPrefs.GetInt(SkinAdCount, 0) ;
            set => PlayerPrefs.SetInt(SkinAdCount, value);
        }
        public const string BrushAdCount = "brush-ad-count";
        public static int GetBrushAdCount
        {
            get => PlayerPrefs.GetInt(BrushAdCount, 0) ;
            set => PlayerPrefs.SetInt(BrushAdCount, value);
        }
        
        public static string[] GetSkinOwner(string key)
        {
            var skins = GetArray<string>(string.Concat(SkinOwner, key)).ToList();
            if (!skins.Contains("normal"))
            {
                skins.Add("normal");
                // if (key == Male)
                // {
                //     skins.Add("football");
                //     skins.Add("santa");
                // }
                // else
                // {
                //     skins.Add("wednesday");
                // }
            }

            SetArray(string.Concat(SkinOwner, key), skins.ToArray());
            return skins.ToArray();
        }

        public static void AddSkinOwner(string value, string key)
        {
            var tmpLst = GetSkinOwner(key).ToList();
            if (tmpLst.Contains(value)) return;
            tmpLst.Add(value);
            SetArray(string.Concat(SkinOwner, key), tmpLst.ToArray());
        }

        public static bool HasSkinOwner(string value, string key)
        {
            var tmpLst = GetSkinOwner(key).ToList();
            return tmpLst.Contains(value);
        }

        private const string SkinMaleUsingKey = "skin-male-using";
        private const string SkinFemaleUsingKey = "skin-female-using";

        public static string SkinMaleUsing
        {
            get => PlayerPrefs.GetString(SkinMaleUsingKey, "normal");
            set => PlayerPrefs.SetString(SkinMaleUsingKey, value);
        }

        public static string SkinFemaleUsing
        {
            get => PlayerPrefs.GetString(SkinFemaleUsingKey, "normal");
            set => PlayerPrefs.SetString(SkinFemaleUsingKey, value);
        }

        //=============================================

        #endregion

//===================================


//===================================

        #region BuildArea

        private const string BuildAreaKey = "build-area-";

        public static float[] GetBuildArea(string key)
        {
            var buildArea = GetArray<float>(string.Concat(BuildAreaKey, key));
            if (buildArea.Length == 0)
            {
                buildArea = new float[2];
                buildArea[0] = -1f;
            }

            SetArray(string.Concat(BuildAreaKey, key), buildArea);
            return buildArea;
        }

        public static void SetBuildArea(float[] value, string key)
        {
            var tmpLst = GetBuildArea(key);
            tmpLst[0] = value[0];
            tmpLst[1] = value[1];
            if (Mathf.Approximately(value[1], 1))
            {
                tmpLst[0] = value[0] + 1;
                tmpLst[1] = 0;
            }

            SetArray(string.Concat(BuildAreaKey, key), tmpLst);
        }
        
        public static bool CheckBuildArea()
        {
            var tmpLst1 = GetBuildArea(BuildAreaType.dinner.ToString());
            var tmpLst2 = GetBuildArea(BuildAreaType.store.ToString());
            var tmpLst3 = GetBuildArea(BuildAreaType.hotel.ToString());

            if (tmpLst1[0] < 5 || tmpLst2[0] < 5 || tmpLst3[0] < 5 ||
                tmpLst1[1] < 1 || tmpLst2[1] < 1 || tmpLst3[1] < 1)
            {
                // turn on red dot
                return true;
            }

            return false;
        }

        #endregion

//===================================


//===== daily reward ================

        private const string DailyRewardKey = "daily-reward";

        public static int[] GetDailyReward()
        {
            var dailyReward = GetArray<int>(DailyRewardKey);
            if (dailyReward.Length == 0)
            {
                dailyReward = new int[4];
            }

            SetArray(DailyRewardKey, dailyReward);
            return dailyReward;
        }

        public static void ResetDailyReward()
        {
            var dailyReward = new int[4];
            SetArray(DailyRewardKey, dailyReward);
        }

        public static int GetDailyReward(int index)
        {
            var tmpLst = GetDailyReward();
            return tmpLst[index];
        }

        public static void SetDailyReward(int value)
        {
            var tmpLst = GetDailyReward();
            tmpLst[value] = 1;
            SetArray(DailyRewardKey, tmpLst);
        }



//===================================

//================== store current ads position =================

        public static AdScreenType StoreLastAdsPosition;

        private static AdScreenType currentAdsPosition;

        public static AdScreenType CurrentAdsPosition
        {
            set
            {
                StoreLastAdsPosition = currentAdsPosition;
                currentAdsPosition = value;
            }
        }
//===================================

    }
}