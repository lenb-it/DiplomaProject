using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using DevClient.Services;

namespace DevClient.Models
{
    public partial class OrderModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isSelected;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private float _price;

        [ObservableProperty]
        private bool _isPaid;

        [ObservableProperty]
        private int _numberPlace;

        public int Id { get; set; }

        public void OpenChangeWindow()
        {
            
        }

        public async Task<bool> DeleteAsync()
        {
            return await ApiService.DeleteOrder(Id);
        }

        public async Task<bool> UpdateAsync()
        {
            return await ApiService.UpdateOrder(new Order
            {
                IsPaid = _isPaid,
                Email = _email,
                Name = _name,
                Id = Id,
                NumberPlace = _numberPlace,
                Price = _price,
            });
        }
    }
}
