using BlazorLaboratory.Enum;

namespace BlazorLaboratory.Dto
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public DateTime AlertDateTime { get; set; }
        public int ObjectId { get; set; }
        public AlertType ObjectType { get; set; }
    }
}