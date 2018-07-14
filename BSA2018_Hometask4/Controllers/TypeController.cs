﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSA2018_Hometask4.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSA2018_Hometask4.Shared.DTO;
using FluentValidation;
using BSA2018_Hometask4.Shared.Exceptions;

namespace BSA2018_Hometask4.Controllers
{
    [Route("v1/api/types")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        readonly ITypeService service;

        public TypesController(ITypeService typeService)
        {
            service = typeService;
        }
        // GET: v1/api/types
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(service.Get());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: v1/api/types/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(service.Get(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: v1/api/types
        [HttpPost]
        public IActionResult Post([FromBody]TypeDto value)
        {
            try
            {
                service.Create(value);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: v1/api/types/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TypeDto Type)
        {
            try
            {
                service.Update(Type, id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE: v1/api/types/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // DELETE: v1/api/types
        [HttpDelete]
        public IActionResult Delete([FromBody] TypeDto Type)
        {
            try
            {
                service.Delete(Type);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
