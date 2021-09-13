using System.Collections.Generic;

namespace GraphqlDemo.Core.Domain
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseKey { get; set; }
        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}