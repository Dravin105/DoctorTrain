namespace DoctorTrain.Model.Dto
{
    public class PatientReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }     // Patient ka pehla naam
        public string Address { get; set; }      // Patient ka Address
        public string Gender { get; set; }      // Patient ka Gender
        public DateOnly DOB { get; set; }      // Patient ka DOB
        public string BloodGruop { get; set; }      // Patient ka BloodGruop
        public string Mobile { get; set; }        // Patient ka mobile number
        public string Email { get; set; }
    }
}
