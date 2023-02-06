namespace BusinessLayer.Services.Implementation
{
    using AutoMapper;
    using BusinessLayer.Dto;
    using BusinessLayer.Services.Interfaces;
    using BusinessLayer.Validators;
    using DataLayer.Items;
    using DataLayer.Repositories.Interfaces;
    using FoodApi.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using Utilities.ErrorHandle;
    using Utilities.Helpers;

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly InputValidator _inputValidator = new InputValidator();
        private readonly string cookieAccessTokenString = string.Empty;
        private readonly string cookieRefreshTokenString = string.Empty;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, string cookieAccessTokenString, string cookieRefreshTokenString)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            this.cookieAccessTokenString = cookieAccessTokenString;
            this.cookieRefreshTokenString = cookieRefreshTokenString;
        }

        public async Task<UserDto> RegisterAsync(RegistrationDto userDto)
        {
            var validator = new RegistrationDtoValidator(userDto, _unitOfWork);
            await validator.ValidateModel();
            var user = _mapper.Map<User>(userDto);
            (user.PasswordSalt, user.PasswordHash) = HashHelper.CreatePasswordHash(userDto.Password);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> LoginUserAsync(LoginDto login)
        {
            var validator = new LoginDtoValidator(login);
            validator.Valid();

            var user = await _unitOfWork.Users.GetUserByNameAsync(login.Username);
            _inputValidator.ValidateIsNotNull(user, "the user doesn't exist");

            validator.ValidatePassword(user.PasswordSalt, user.PasswordHash);

            string token = CreateAccessToken(user.Username);
            string refreshToken = await CreateRefreshToken(user.Id);
            SetTokensToCookies(token, refreshToken);
            return true;
        }

        public async Task<string> RefreshToken(string accessToken, string refreshToken)
        {
            var user = await GetUserByToken(accessToken);
            _inputValidator.ValidateIsNotNull(user, "can't get user by access token");
            _inputValidator.ValidateShouldBeEqual(user.RefreshToken, refreshToken, "The refreshToken is invalid", (int)ErrorCodes.NotValidRefreshToken);
            var newAccessToken = CreateAccessToken(user.Username);

            return newAccessToken;
        }

        public void DeleteTokensFromCookies()
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx.Request.Cookies[cookieAccessTokenString] != null)
                ctx.Response.Cookies.Delete(cookieAccessTokenString);

            if (ctx.Request.Cookies[cookieRefreshTokenString] != null)
                ctx.Response.Cookies.Delete(cookieRefreshTokenString);

        }

        public async Task<bool> DeleteRefreshTokenFromDb()
        {
            (var user, var refreshToken) = await GetUserByRefreshToken();
            if (user == null)
            {
                return false;
            }

            user.RefreshToken = string.Empty;
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> CheckIfLoginned(string userName)
        {
            (var user, var refreshToken) = await GetUserByRefreshToken();
            return !(user == null || user.Username != userName);
        }

        #region private methods
        private string CreateAccessToken(string userName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };

            (string? tokenKey, string? issuer, string? audience) = GetConfigurationOfTokens();

            _inputValidator.ValidateIsNotNullOrEmpty(tokenKey, "The token key in configuration is empty");
            _inputValidator.ValidateIsNotNullOrEmpty(issuer, "The issuer key in configuration is empty");
            _inputValidator.ValidateIsNotNullOrEmpty(audience, "The audience key in configuration is empty");

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey ?? string.Empty));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private async Task<string> CreateRefreshToken(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            _inputValidator.ValidateIsNotNull(user, "the user doesn't exist");

            user.RefreshToken = RandomBase64String();
            await _unitOfWork.SaveAsync();
            return user.RefreshToken;
        }

        private void SetTokensToCookies(string accessToken, string refreshToken)
        {
            var ctx = _httpContextAccessor.HttpContext;
            ctx.Response.Cookies.Append(cookieAccessTokenString, accessToken,
                       new CookieOptions
                       {
                           MaxAge = TimeSpan.FromMinutes(60),
                       });
            ctx.Response.Cookies.Append(cookieRefreshTokenString, refreshToken,
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(1),
                });
            ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            ctx.Response.Headers.Add("X-Xss-Protection", "1");
            ctx.Response.Headers.Add("X-Frame-Options", "DENY");
        }

        private async Task<User?> GetUserByToken(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            _inputValidator.ValidateIsNotNull(principal, "The principal from the token wasn't found");

            var username = principal.Identity.Name;
            return await _unitOfWork.Users.GetUserByNameAsync(username);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            (string? tokenKey, string? issuer, string? audience) = GetConfigurationOfTokens();

            _inputValidator.ValidateIsNotNullOrEmpty(tokenKey, "The token key in configuration is empty");
            _inputValidator.ValidateIsNotNullOrEmpty(issuer, "The issuer key in configuration is empty");
            _inputValidator.ValidateIsNotNullOrEmpty(audience, "The audience key in configuration is empty");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey ?? "")),
                ValidateIssuerSigningKey = true,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new BadRequestExeption((int)ErrorCodes.NoValidData, "Invalid token");

            return principal;
        }

        private string RandomBase64String()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private async Task<(User?, string?)> GetUserByRefreshToken()
        {
            var ctx = _httpContextAccessor.HttpContext;
            var refreshToken = ctx.Request.Cookies[cookieRefreshTokenString];
            if (refreshToken == null)
            {
                return (null, null);
            }

            return (await _unitOfWork.Users.GetUserByRefreshTokenAsync(refreshToken), refreshToken);
        }

        private (string?, string?, string) GetConfigurationOfTokens()
        {
            return (_configuration.GetSection("Jwt")["Key"], _configuration.GetSection("Jwt")["Issuer"], _configuration.GetSection("Jwt")["Audience"]);

        }
        #endregion
    }
}
