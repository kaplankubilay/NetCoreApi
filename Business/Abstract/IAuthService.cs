using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        /// <summary>
        /// Kullanıcı kayıt iişlemi
        /// </summary>
        /// <param name="userForRegisterDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);

        /// <summary>
        /// Kullanıcı login işlemi
        /// </summary>
        /// <param name="userForLoginDto"></param>
        /// <returns></returns>
        IDataResult<User> Login(UserForLoginDto userForLoginDto);

        /// <summary>
        /// Daha önce oluşturulan mail kontrolü/kullanıcı var mı?
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        IResult UserExists(string email);

        /// <summary>
        /// Erişim için oluşturulan token.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
