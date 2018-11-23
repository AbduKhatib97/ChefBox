using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChefBox.AdminUI.ViewModels.Home.Cards
{
    public class StatisticEntityCardViewModel
    {
        public int LatestItemId { get; set; }
        public string LatestItemName { get; set; }
        public int EntitiesCount { get; set; }
    }
}
