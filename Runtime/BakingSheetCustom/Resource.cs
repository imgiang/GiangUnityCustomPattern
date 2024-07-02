using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace GiangCustom.Runtime.BakingSheetCustom
{
    [Serializable]
    public struct Resource
    {
        public int Coin { get; set; }

        public static Resource operator +(Resource a, Resource b)
        {
            return new Resource
            {
                Coin = a.Coin + b.Coin,
            };
        }
    }
}