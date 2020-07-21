namespace Common.Model
{
    /// <summary>
    ///  Entity class for Address
    /// </summary>
    public abstract class Address
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }

    }
}
