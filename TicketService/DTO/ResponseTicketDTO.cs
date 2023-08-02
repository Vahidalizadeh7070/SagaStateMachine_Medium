namespace TicketService.DTO
{
    public class ResponseTicketDTO
    {
        public string TicketId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public DateTime RequireDate { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string TicketNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
