using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);

        /// <summary>
        /// Email bilgisi üzerinden kullanıcı verilerinin getirilmesi için kullanılan metot.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User GetByMail(string email);
    }
}
