using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.DTO.User;
using ApiProject.Models;
using ApiProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser>signInManager)
        {
            _userManager=userManager;
            _tokenService=tokenService;
            _signInManager=signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto){

            try{
                if(!ModelState.IsValid){  // lel validation li aamlneha fel DTO
                    return BadRequest(ModelState);
                }
                var appUser= new AppUser 
                {
                    UserName=registerDto.Username,
                    Email=registerDto.Email
                };
                var createdUser= await _userManager.CreateAsync(appUser,registerDto.Password);

                if(createdUser.Succeeded){
                    var roleResult= await _userManager.AddToRoleAsync(appUser,"User");
                    if(roleResult.Succeeded){
                        return Ok(
                            new NewUserDto {
                                Username=appUser.UserName,
                                Email=appUser.Email,
                                token=_tokenService.CreateToken(appUser)
                            }
                        );
                    }else {
                        return StatusCode(500, roleResult.Errors);
                    }
                }else {

                    return StatusCode(500,createdUser.Errors);
                }





            }catch(Exception e){
                return StatusCode(500,e);

            }

            
        }
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDto loginDto){

            try{
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
                var user= await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==loginDto.Username.ToLower());

                if(user==null){
                    return Unauthorized("Invalid Username");
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

                if(!result.Succeeded){
                    return Unauthorized("Username is not found /Password not found");
                }

                return Ok(
                    new NewUserDto {
                        Username=user.UserName,
                        Email=user.Email,
                        token=_tokenService.CreateToken(user)

                    }
                );





            }catch(Exception e){
                return StatusCode(500,e);



            }




        }
    }
}