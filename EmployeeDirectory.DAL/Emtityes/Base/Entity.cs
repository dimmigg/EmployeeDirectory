using EmployeeDirectory.Interfaces;

namespace EmployeeDirectory.DAL.Emtityes.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
