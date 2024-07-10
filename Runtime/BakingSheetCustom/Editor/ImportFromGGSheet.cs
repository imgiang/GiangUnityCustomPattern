using System.IO;
using GiangCustom.Runtime.BakingSheetCustom.Containers;
using GiangCustom.Runtime.BakingSheetCustom.Editor;
using UnityEditor;
using UnityEngine;

namespace GiangCustom.Runtime.BakingSheetCustom
{
    public class ImportFromGGSheet : MonoBehaviour
    {
        private const string ID = "1sTVTnviL1Q50Z20UGcM-yROH2KeifM6aK5p4ce4jI-U";
    
        [MenuItem("Tools/ImportDta/Import Resource")]
        public static async void ImportDtaRule()
        {
            var jsonPath = Path.Combine("Assets/_DataGGSheet", "Resource");

            var sheetContainer = new SheetContainer();

            await GoogleSheetTools.ConvertFromGoogle(ID, jsonPath, sheetContainer);
        }
    }
}
