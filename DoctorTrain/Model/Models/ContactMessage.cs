﻿namespace DoctorTrain.Model.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}
