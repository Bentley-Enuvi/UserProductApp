using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserProduct.API.Response;
using UserProduct.Core.Abstractions;
using UserProduct.Core.DTOs;
using UserProduct.Domain.Entities;

namespace UserProduct.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userService;
        private readonly SignInManager<User> _signInManager;

        public UserManagementController(
            IUserManagementService userService,
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        // [Authorize(Roles = "ADMIN")]
        [HttpGet("get-all-users")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "User may not exist or has been deleted",
                    Error = "No Users Found"
                });
            }
            return Ok(new ResponseDto<object>
            {
                Data = users,
                Code = (int)HttpStatusCode.OK,
                Message = "Ok",
                Error = "",
                IsSuccessful = true
            });
        }

        //[Authorize(Roles = "ADMIN")]
        [HttpGet("get-user-by-id/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var users = await _userService.GetUserById(userId);

            if (users == null)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = (int)HttpStatusCode.NotFound,
                    Message = "Bad Request",
                    Error = "User Not Found"
                });
            }
            return Ok(new ResponseDto<object>
            {
                Data = users,
                Code = (int)HttpStatusCode.OK,
                Message = "Ok",
                Error = "",
                IsSuccessful = true
            });
        }


        [HttpGet("get-purchased-products-by-user")]
        [Authorize]
        public async Task<IActionResult> ProductsPurchasedByUser()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (user == null)
            {
                // Handle the case where the user is not logged in
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Bad Request",
                    Error = "User Not Found"
                });
            }

            var userId = user.Id;

            // Call the service method to get products purchased by the user
            var pucrchasedProducts = await _userService.GetProductsPurchasedByUser(userId);

            return Ok(new ResponseDto<object>
            {
                Data = pucrchasedProducts,
                Code = (int)HttpStatusCode.OK,
                Message = "Ok",
                Error = "",
                IsSuccessful = true
            });
        }

        // [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {

            var result = await _userService.DeleteUser(id);

            if (result != null)
            {
                var response = new ResponseDto<object>
                {
                    Code = 200,
                    Data = result,
                    Message = "User Deleted Successfully",
                    Error = string.Empty,
                    IsSuccessful = true
                };

                return Ok(response);
            }
            else
            {
                var response = new ResponseDto<object>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Data = null,
                    Message = "Failed to Delete User",
                    Error = "Failed"
                };

                return BadRequest(response);
            }
        }

        // [Authorize(Roles = "ADMIN")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequestDto appUserDTO)
        {
            var result = await _userService.UpdateUser(id, appUserDTO);

            if (result != null)
            {
                var response = new ResponseDto<UpdateUserRequestDto>
                {
                    Code = 200,
                    Data = result,
                    Message = "User Updated Successfully",
                    Error = string.Empty,
                    IsSuccessful = true
                };

                return Ok(response);
            }
            else
            {
                var response = new ResponseDto<UpdateUserRequestDto>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Data = null,
                    Message = "Failed to update User",
                    Error = "Failed"
                };

                return BadRequest(response);
            }
        }
    }
}
