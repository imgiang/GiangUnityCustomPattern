using System.Collections;
using System.IO;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Unity;
using Newtonsoft.Json;
using UnityEngine;

namespace GiangCustom.Runtime.BakingSheetCustom.Containers
{
    public class SheetContainer : SheetContainerBase
    {
        public SheetContainer() : base(UnityLogger.Default) {}

        // property name matches with corresponding sheet name
        // for .xlsx or google sheet, **property name matches with the name of sheet tab in workbook**
        // for .csv or .json, **property name matches with the name of file**
        public MoneyRewardSheet HotelReward { get; private set; }
        public MoneyRewardSheet StoreReward { get; private set; }
        public MoneyRewardSheet DinnerReward { get; private set; }
    }
    
    public class MoneyRewardSheet : Sheet<MoneyRewardSheet.ResourceBonusRow>
    {
        public class ResourceBonusRow : SheetRow
        {
            public Resource Resource { get; set; }

            public IEnumerator LoadAsync(string path)
            {
                var resourceRequest = Resources.LoadAsync<TextAsset>($"Data/ResourceBonus/{path}");
                yield return resourceRequest;
                if (resourceRequest.asset is TextAsset textAsset)
                {
                    JsonConvert.PopulateObject(textAsset.text, this);
                }
                else
                {
                    Debug.LogError($"Load fail: {path}");
                }
            }
        }

        public override void PostLoad(SheetConvertingContext context)
        {
            base.PostLoad(context);

            string dirPath = Path.Combine(Application.dataPath, "Resources", "Data", "ResourceBonus","Hotel");
            string dirPath2 = Path.Combine(Application.dataPath, "Resources", "Data", "ResourceBonus","Store");
            string dirPath3 = Path.Combine(Application.dataPath, "Resources", "Data", "ResourceBonus","Dinner");
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath, true);
            }
            if (Directory.Exists(dirPath2))
            {
                Directory.Delete(dirPath2, true);
            }
            if (Directory.Exists(dirPath3))
            {
                Directory.Delete(dirPath3, true);
            }

            Directory.CreateDirectory(dirPath);
            Directory.CreateDirectory(dirPath2);
            Directory.CreateDirectory(dirPath3);

            if (context.Container is SheetContainer sheetContainer)
            {
                foreach (var resourceBonusRow in sheetContainer.HotelReward.Items)
                {
                    var jsonPath = Path.Combine(dirPath, $"{resourceBonusRow.Id}.json");
                    using (StreamWriter file = File.CreateText(jsonPath))
                    {
                        JsonSerializer serializer = new JsonSerializer
                        {
                            DefaultValueHandling = DefaultValueHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore
                        };
                        serializer.Serialize(file, resourceBonusRow);
                    }
                }
                
                foreach (var resourceBonusRow in sheetContainer.StoreReward.Items)
                {
                    var jsonPath = Path.Combine(dirPath2, $"{resourceBonusRow.Id}.json");
                    using (StreamWriter file = File.CreateText(jsonPath))
                    {
                        JsonSerializer serializer = new JsonSerializer
                        {
                            DefaultValueHandling = DefaultValueHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore
                        };
                        serializer.Serialize(file, resourceBonusRow);
                    }
                }
                
                foreach (var resourceBonusRow in sheetContainer.DinnerReward.Items)
                {
                    var jsonPath = Path.Combine(dirPath3, $"{resourceBonusRow.Id}.json");
                    using (StreamWriter file = File.CreateText(jsonPath))
                    {
                        JsonSerializer serializer = new JsonSerializer
                        {
                            DefaultValueHandling = DefaultValueHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore
                        };
                        serializer.Serialize(file, resourceBonusRow);
                    }
                }
            }
        }
    }
}