using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevClient.Models;
using DevClient.Views;

namespace DevClient.ViewModels
{
    public partial class DishAndDrinkVM :ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<DishAndDrinkModel> _dishesAndDrinks;

        public DishAndDrinkVM()
        {
            DishesAndDrinks = new ObservableCollection<DishAndDrinkModel>();
        }

        public void SetData(List<DishAndDrinkModel> data)
        {
            DishesAndDrinks.Clear();
            data.ForEach(x => DishesAndDrinks.Add(x));
        }

        [RelayCommand]
        public async void UpdateSelectedAsync()
        {
            var selected = DishesAndDrinks.Where(x => x.IsSelected).ToList();

            foreach (var item in selected)
            {
                if (await item.UpdateAsync())
                    item.IsSelected = false;
            }

            if (selected.Any(x => x.IsSelected))
                MessageBox.Show("Выделенные элементы не удалось сохранить, попробуйте ещё раз!");
        }

        [RelayCommand]
        public async void DiscountSelectedAsync()
        {
            var view = new DiscountView();
            var model = new DiscountVM();
            var selected = DishesAndDrinks
                .Where(x => x.IsSelected)
                .Select(x => x.Id)
                .ToArray();

            model.SetDishesAndDrinks(selected);
            model.CloseAction = () => view.Close();
            view.DataContext = model;
            view.Show();
        }
    }
}
