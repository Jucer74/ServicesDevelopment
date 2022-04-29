using ReminderApp.Application.exception;
using ReminderApp.Application.Interfaces;
using ReminderApp.Domain.Entities;
using ReminderApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Application.Services
{
    public class ReminderService: IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task Add(Reminder entity)
        {
            await _reminderRepository.Add(entity);
        }

        public async Task<IEnumerable<Reminder>> Find(Expression<Func<Reminder, bool>> predicate)
        {
            return await _reminderRepository.Find(predicate);
        }

        public async Task<IEnumerable<Reminder>> GetAll()
        {
            return await _reminderRepository.GetAll();
        }

        public async Task<Reminder> GetById(int id)
        {
            var var = await _reminderRepository.GetById(id);
            if (var != null)
            {
                return var;
            }
            else {
                throw new Exceptions("Id invalido");
            }
        }

        public async Task Remove(int id)
        {
            var var = await _reminderRepository.GetById(id);
            if (var != null)
            {
                await _reminderRepository.Remove(var);
            }
            else
            {
                throw new Exceptions("Id invalido");
            }
            
        }

        public async Task Update(int id, Reminder entity)
        {
            
            if (entity != null)
            {
                await _reminderRepository.Update(entity);
            }
            else
            {
                throw new Exceptions("invalido");
            }
        }
        public async Task DeleteAllByCategoryId(int id)
        {

                await _reminderRepository.DeleteAllByCategoryId(id);

        }
        public async Task<IEnumerable<Reminder>> GetAllBycategoryId(int id)
        {
            return await _reminderRepository.GetAllBycategoryId(id);
             
        }
    }
}
