using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevClient.Models;
using DevClient.Services;

namespace DevClient.ViewModels
{
    public partial class DiscountVM : ObservableObject
    {
        [ObservableProperty]
        private DateTime _dateStart = DateTime.UtcNow;

        [ObservableProperty]
        private DateTime _dateEnd = DateTime.UtcNow;

        [ObservableProperty]
        private int _discount = 10;

        public Action CloseAction { get; set; }

        private int[] _dishAndDrinkIds;

        public void SetDishesAndDrinks(int[] dishesAndDrinks)
        {
            _dishAndDrinkIds = dishesAndDrinks;
        }

        [RelayCommand]
        public async void AddDiscount()
        {
            var dateEnd = DateEnd.AddDays(1);
            var model = new DiscountAddRange
            {
                DateEnd = DateStart,
                DateStart = dateEnd,
                Discount = Discount,
                DishAndDrinkIds = _dishAndDrinkIds,
            };

            if (!(await ApiService.DiscountAddRange(model)))
                MessageBox.Show("Произошла ошибка при сохранении");

            CloseAction?.Invoke();
        }
    }
}
