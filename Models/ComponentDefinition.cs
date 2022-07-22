using System.ComponentModel.DataAnnotations;

namespace cms_net.Models
{
    public class ComponentDefinition
    {
        [Key]
        public string Key { get; set; }

        public ComponentDefinition()
        {

        }
    }
}
