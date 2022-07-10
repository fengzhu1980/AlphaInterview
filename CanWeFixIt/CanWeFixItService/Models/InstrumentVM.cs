using CanWeFixItRepository.Models;

namespace CanWeFixItService.Models
{
    public class InstrumentVM
    {
        public InstrumentVM()
        {
        }
        public InstrumentVM(InstrumentDM instrumentDM)
        {
            Id = instrumentDM.Id;
            Sedol = instrumentDM.Sedol;
            Name = instrumentDM.Name;
            Active = instrumentDM.Active;
        }
        public int Id { get; set; }
        public string Sedol { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
