namespace Business.Models
{
    public interface IViewModelIsValid
    {
        public bool IsValid();

        public IEnumerable<string> Errors();
    }
}
