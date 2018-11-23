using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefBox.AdminUI.ViewModels.Base;
using ChefBox.Cooking.IData.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChefBox.AdminUI.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        public ISharedRepository SharedRepository { get; }

        protected BaseController(ISharedRepository sharedRepository)
        {
            SharedRepository = sharedRepository;
        }

        public override ViewResult View(object model)
        {
            FillBaseViewModel(model);
            return base.View(model);
        }
        public override ViewResult View(string viewName, object model)
        {
            FillBaseViewModel(model);
            return base.View(viewName, model);
        }

        private void FillBaseViewModel(object viewModel)
        {
            if (viewModel is SharedViewModel)
            {
                var results = SharedRepository.GetSharedContent();
                var vm = viewModel as SharedViewModel;
                vm.CategoriesCount = results.CategoriesCount;
                vm.IngredientsCount = results.IngredientsCount;
                vm.RecipesCount = results.RecipesCount;
                vm.PhotosCount = results.PhotosCount;
            }
        }
    }
}
