using AutoMapper;
using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.DatabaseConection;
using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DoctorTrain.Business_Layer.Service
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactMessageService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddMessageAsync(ContactFormDto dto)
        {
            var entity = _mapper.Map<ContactMessage>(dto);
            entity.SubmittedAt = DateTime.Now; // Still set manually if not part of DTO
            entity.IsRead = false;

            _context.ContactMessages.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ContactFormDto>> GetAllAsync()
        {
            var list = await _context.ContactMessages.OrderByDescending(m=>m.SubmittedAt).ToListAsync();
            return _mapper.Map<List<ContactFormDto>>(list);

        }

        public async Task<List<ContactFormDto>> GetUnreadAsync()
        {
            var getUnread = await _context.ContactMessages.Where(m => !m.IsRead).ToListAsync();
            return _mapper.Map<List<ContactFormDto>>(getUnread);
        }

        public async Task MarkAsReadAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
