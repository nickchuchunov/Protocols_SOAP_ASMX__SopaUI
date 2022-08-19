﻿using ClinicService.Data;

namespace ClinicService.Services.Impl
{
    public class PetRepository : IPetRepository
    {

        #region Serives

        private readonly ClinicServiceDbContext _dbContext;
        private readonly ILogger<PetRepository> _logger;

        #endregion

        #region Constructors

        public PetRepository(ClinicServiceDbContext dbContext,
            ILogger<PetRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        #endregion

        public int Add(Pet item)
        {
            _dbContext.Pets.Add(item);
            _dbContext.SaveChanges();
            return item.ClientId;
        }

        public void Delete(Pet item)
        {
            if (item == null)
                throw new NullReferenceException();
            Delete(item.PetId);


        }

        public void Delete(int id)
        {
            var pet = GetById(id);
            if (pet == null)
                throw new KeyNotFoundException();
            _dbContext.Remove(pet);
            _dbContext.SaveChanges();

        }

        public IList<Pet> GetAll()
        {
            return _dbContext.Pets.ToList();
        }

        public Pet? GetById(int id)
        {
            return _dbContext.Pets.FirstOrDefault(pet => pet.PetId == id);
        }

        public void Update(Pet item)
        {
            if (item == null)
                throw new NullReferenceException();

            var pet = GetById(item.PetId);
            if (pet == null)
            throw new KeyNotFoundException();

            pet.PetId = item.PetId;
            pet.ClientId = item.ClientId;
            pet.Name = item.Name;
            pet.Birthday = item.Birthday;
            pet.Client = item.Client;
            pet.Consultations = item.Consultations;
          
            _dbContext.Update(pet);
            _dbContext.SaveChanges();

        }
    }
}
