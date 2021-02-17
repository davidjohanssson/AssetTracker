using System.Collections.Generic;

namespace server
{
    public class FormFactor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Asset> Assets { get; } = new List<Asset>();
    }
}