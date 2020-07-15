using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.BLL.Constants;
using ProductApp.BLL.Interfaces;
using ProductApp.BLL.Models;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService operationService;

        public OperationController(IOperationService operationService)
        {
            this.operationService = operationService;
        }
        [HttpGet]
        [Route("{id}")]
        //GET: api/Operation/id
        public IEnumerable<OperationDTO> GetOperations(int id, [FromQuery]SortOperationState sortOrder)
        {
            return operationService.GetAllOperations(id, sortOrder);
        }
        [HttpPost]
        //POST: api/Operation
        public async Task<IActionResult> AddNewOperation(OperationDTO model)
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "Id").Value;
                model.UserId = userId;

                await operationService.AddOperation(model);
                return Ok();
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest(ErrorMessages.InvalidProductAmount);
            }
            catch (ArgumentException)
            {
                return BadRequest(ErrorMessages.AmountNotEnough);
            }
            catch
            {
                return BadRequest(ErrorMessages.InvalidOperation);
            }
        }
    }
}