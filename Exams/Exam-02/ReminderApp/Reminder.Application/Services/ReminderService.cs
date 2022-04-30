using ReminderAPP.Application.Interfaces;
using ReminderAPP.Domain.Entities;
using ReminderAPP.Domain.Interface;
using ReminderAPP.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReminderAPP.Application.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task AddAsync(Reminder entity)
        {
            await _reminderRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Reminder>> FindAsync(Expression<Func<Reminder, bool>> predicate)
        {
            return await _reminderRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _reminderRepository.GetAllAsync();
        }

        public async Task<Reminder> GetByIdAsync(int id)
        {
            var Reminder= await _reminderRepository.GetByIdAsync(id);

            // Validte If Exist
            return Reminder;
        }

        public async Task RemoveAsync(int id)
        {
            var Reminder = await _reminderRepository.GetByIdAsync(id);
            await _reminderRepository.RemoveAsync(Reminder);
        }

        public async Task UpdateAsync(int id, Reminder entity)
        {
            // Validate if Exist
            await _reminderRepository.UpdateAsync(entity);
        }
        public async Task DeleteByCategoryId(int Id)
        {
            await _reminderRepository.DeleteByCategoryId(Id);
        }

        public async Task<IEnumerable<Reminder>> getAllByCategoryId(int Id)
        {
            return await _reminderRepository.getAllByCategoryId(Id);
        }
    }
}
