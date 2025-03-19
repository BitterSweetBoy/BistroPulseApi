using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Application.DTOs;
using Module.Application.Interfaces;
using Module.Infrastructure.Filters;
using Module.Shared.Response;

namespace BistroPulseApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);
            if (!result)
            {
                return BadRequest(new ApiResponse<string>(
                    Success: false,
                    Message: "Registro fallido",
                    Data: null,
                    Errors: new Dictionary<string, string[]>
                    {
                        { "Register", new[] { "El registro no pudo completarse" } }
                    }
                ));
            }

            return Ok(new ApiResponse<string>(
                Success: true,
                Message: "Usuario registrado con éxito",
                Data: null
            ));
        }


        /// <summary>
        /// Inicia sesión y crea una sesión de autenticación.
        /// </summary>
        /// <param name="model">Credenciales del usuario</param>
        /// <returns>200 si es exitoso, 401 si las credenciales son incorrectas</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 401)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var success = await _authService.LoginAsync(model);
            
            if (!success)
            {
                return Unauthorized(new ApiResponse<object>(
                    Success: false,
                    Message: "Usuario o contraseña invalido",
                    Data: null
                ));
            }
            
            return Ok(new ApiResponse<object>(
                Success: true,
                Message: "Inicio de sesión exitoso",
                Data: null
            ));
        }

        /// <summary>
        /// Verifica si la sesión está activa.
        /// </summary>
        /// <returns>200 si la sesión es válida</returns>
        [HttpGet("session")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        public async Task<IActionResult> Session()
        {
            //El middleware AuthMiddleware se encarga de validar la sesión, si pasa retornamos esta respuesta
            return Ok(new ApiResponse<object>(
                Success: true,
                Message: "Sesión activa",
                Data: null
            ));
        }

        /// <summary>
        /// Cierra la sesión actual del usuario.
        /// </summary>
        /// <returns>200 si es exitoso, 400 si hay un error</returns>
        [HttpPost("logout")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            if(!result)
            {
                return BadRequest(new ApiResponse<string>(
                    Success: false,
                    Message: "Error al cerrar sesión",
                    Data: null
                ));
            }
            return Ok(new ApiResponse<string>(
                Success: true,
                Message: "Sesión cerrada correctamente",
                Data: null
            ));
        }

        /// <summary>
        /// Endpoint de prueba que solo puede ser accedido por un usuario con el rol "SuperAdmin".
        /// </summary>
        /// <returns>200 si el usuario tiene permisos, 403 si no tiene acceso</returns>
        [HttpGet("hello")]
        [RoleAuthorize("SuperAdmin")]
        public IActionResult Hello()
        {
            return Ok(new ApiResponse<string>(
                Success: true,
                Message: "Hello World",
                Data: null
            ));
        }
    }
}
