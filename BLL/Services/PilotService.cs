﻿using AutoMapper;
using BSA2018_Hometask4.BLL.Interfaces;
using BSA2018_Hometask4.Shared.DTO;
using BSA2018_Hometask4.Shared.Exceptions;
using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSA2018_Hometask4.BLL.Services
{
    public class PilotService : IPilotService
    {
        readonly IUnitOfWork unit;
        readonly IMapper mapper;
        readonly AbstractValidator<PilotDto> validator;

        public PilotService(IUnitOfWork uow, IMapper map, AbstractValidator<PilotDto> rules)
        {
            unit = uow;
            mapper = map;
            validator = rules;
        }
        public void Create(PilotDto Pilot)
        {
            var validationResult = validator.Validate(Pilot);
            if (validationResult.IsValid)
                unit.Pilots.Create(mapper.Map<PilotDto, Pilot>(Pilot));
            else
                throw new ValidationException(validationResult.Errors);
            
        }

        public void Delete(int id)
        {
            unit.Pilots.Delete(id);
        }

        public void Delete(PilotDto Pilot)
        {
            unit.Pilots.Delete(mapper.Map<PilotDto, Pilot>(Pilot));
        }

        public PilotDto Get(int id)
        {
            return mapper.Map<Pilot, PilotDto>(unit.Pilots.Get(id));
        }

        public List<PilotDto> Get()
        {
            return mapper.Map<List<Pilot>, List<PilotDto>>(unit.Pilots.Get());
        }

        public void Update(PilotDto Pilot, int id)
        {
            var validationResult = validator.Validate(Pilot);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                unit.Pilots.Update(mapper.Map<PilotDto, Pilot>(Pilot), id);
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public void Update(int experience, int id)
        {
            var validationResult = validator.Validate(new PilotDto() { Experience = experience }, "Experience");
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                (unit.Pilots as PilotRepository).Update(experience, id);
            }
            catch (ArgumentNullException)
            {
                throw new NotFoundException();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
