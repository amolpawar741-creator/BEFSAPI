namespace BEFS.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string MenuName { get; set; } = "";
        public string Route { get; set; } = "";
        public string font { get; set; } = "";
        public bool ischildren { get; set; } = false;
        public int? ParentId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
