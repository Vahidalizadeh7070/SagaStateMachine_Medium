using System.ComponentModel.DataAnnotations;

namespace TicketService.DTO
{
    public class AddTicketDTO
    {

        public string TicketId { get; set; } = Guid.NewGuid().ToString();   
        public string Title { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime RequireDate { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
