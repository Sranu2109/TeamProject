
using System.Security.Claims;
using API.Dtos;
using API.Errors;
using API.Extensions;
using Core.Entities.SellerIdentity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SellerController : BaseApiController
    {
        private readonly UserManager<AppSeller> _userManager;
        private readonly SignInManager<AppSeller> _signInManager;
        private readonly ISellerTokenService _sellerTokenService;

        public SellerController(UserManager<AppSeller> userManager, SignInManager<AppSeller> signInManager, ISellerTokenService sellerTokenService)
        {
            _sellerTokenService = sellerTokenService;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        
        [HttpGet]
        public async Task<ActionResult<SellerDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var seller = await _userManager.FindByEmailAsync(email);

            return new SellerDto
            {
                Email = seller.Email,
                SellerName = seller.SellerName,
                Token = _sellerTokenService.CreateToken(seller)
            };


            // var seller = await _userManager.FindByEmailClaimsPrinciple(HttpContent.User);
            // return new SellerDto
            // {
            //     Email = seller.Email,
            //     SellerName = seller.SellerName,
            //   //  Token = _tokenService.CreateToken(user)

            // };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("sellerlogin")]
        public async Task<ActionResult<SellerDto>> SellerLogin(SellerLoginDto sellerLoginDto)
        {
            var seller = await _userManager.FindByEmailAsync(sellerLoginDto.Email);

            if(seller == null) return Unauthorized(new ApiResponse(401, "Please register yourself"));

            var result = await _signInManager.CheckPasswordSignInAsync(seller, sellerLoginDto.Password, false);

            if(!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Password"));

            return new SellerDto
            {
                Email = seller.Email,
                SellerName = seller.SellerName,
                Token = _sellerTokenService.CreateToken(seller)
            };
        }


        [HttpPost("sellerregister")]
        public async Task<ActionResult<SellerDto>> SellerRegister(SellerRegisterDto sellerRegisterDto) 
        {
            if (CheckEmailExistsAsync(sellerRegisterDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in Use" } });
            }

            var seller = new AppSeller
            {
                SellerName = sellerRegisterDto.SellerName,
                Email = sellerRegisterDto.Email,
                UserName = sellerRegisterDto.Email
            };

            var result = await _userManager.CreateAsync(seller, sellerRegisterDto.Password);

            if(!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new SellerDto
            {
                SellerName = seller.SellerName,
                Email = seller.Email,
                Token  = _sellerTokenService.CreateToken(seller)
            };
        }
    }
}