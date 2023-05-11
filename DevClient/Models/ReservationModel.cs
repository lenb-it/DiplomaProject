using System;
using System.Threading.Tasks;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using DevClient.Services;

namespace DevClient.Models
{
    public partial class ReservationModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isSelected;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private DateTime _date;

        [ObservableProperty]
        private bool _isConfirmed;

        public int Id { get; set; }

        [ObservableProperty]
        private int _countPeople;

        public async Task<bool> DeleteAsync()
        {
            return await Task.Run(() => false);
            //return await ApiService.DeleteReservation(Id);
        }

        public async Task<bool> UpdateAsync()
        {
            return await ApiService.UpdateReservation(new Reservation
            {
                CountPeople = _countPeople,
                Date = _date,
                Email = _email,
                Name = _name,
                Id = Id,
                IsConfirmed = _isConfirmed,
            });
        }
    }
}
