namespace Common.Model
{
    public class DocumentEmployee
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }

        public int EmployeeId { get; set; }

        public  Employee Employee { get; set; }

        public  Document Document { get; set; }


    }
}
