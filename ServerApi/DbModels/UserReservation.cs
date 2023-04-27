namespace ServerApi.DbModels
{
    public class UserReservation
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public bool IsValid { get; set; }

        public DateTime ChangeAt { get; set; }

        public DateTime CreateAt { get; set; }

        public User User { get; set; }
    }
}
