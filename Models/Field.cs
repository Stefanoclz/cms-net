namespace cms_net.Models
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int ComponentId { get; set; }
        public Component Component { get; set; }

        public Field()
        {

        }

        public Field(string name)
        {
            Name = name;
        }

    }
}
