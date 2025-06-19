using DoctorTrain.Model.Dto;

namespace DoctorTrain.Business_Layer.Interface
{
    public interface IContactMessageService
    {
        Task AddMessageAsync(ContactFormDto dto);
        Task<List<ContactFormDto>> GetAllAsync();
        Task<List<ContactFormDto>> GetUnreadAsync();
        Task MarkAsReadAsync(int id);
    }
}
