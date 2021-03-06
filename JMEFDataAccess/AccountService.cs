﻿using JMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using JMDataServiceInterface;

namespace JMEFDataAccess
{
   public class EFAccountService : EFDataService, IAccountDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EFAccountService()
        {

        }

         
        /// <summary>
        /// SysUser Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public SysUser Login(string username,string pwd)
        {
            var userInformation = dbConnection.SysUsers.First(u => u.UserName == username && u.Password == pwd);
            SysUser user = userInformation as SysUser;
            return user;

        }

    }
}
