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
    public class TicketService : ITicketService
    {
        readonly IUnitOfWork unit;
        readonly IMapper mapper;
        readonly AbstractValidator<TicketDto> validator;

        public TicketService(IUnitOfWork uow, IMapper map, AbstractValidator<TicketDto> rules)
        {
            unit = uow;
            mapper = map;
            validator = rules;
        }
        public void Create(TicketDto Ticket)
        {
            var validationResult = validator.Validate(Ticket);
            if (validationResult.IsValid)
                unit.Tickets.Create(mapper.Map<TicketDto, Ticket>(Ticket));
            else
                throw new ValidationException(validationResult.Errors);
            
        }

        public void Delete(int id)
        {
            unit.Tickets.Delete(id);
        }

        public void Delete(TicketDto Ticket)
        {
            unit.Tickets.Delete(mapper.Map<TicketDto, Ticket>(Ticket));
        }

        public TicketDto Get(int id)
        {
            return mapper.Map<Ticket, TicketDto>(unit.Tickets.Get(id));
        }

        public List<TicketDto> Get()
        {
            return mapper.Map<List<Ticket>, List<TicketDto>>(unit.Tickets.Get());
        }

        public void Update(TicketDto Ticket, int id)
        {
            var validationResult = validator.Validate(Ticket);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            try
            {
                unit.Tickets.Update(mapper.Map<TicketDto, Ticket>(Ticket), id);
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
