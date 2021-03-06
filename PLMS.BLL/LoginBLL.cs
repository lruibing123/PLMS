﻿using PLMS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PLMS.BLL
{
    public class LoginBLL
    {
        private static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private static KeyValueConfigurationCollection settings = config.AppSettings.Settings;

        public static bool Login(string Id, string password, bool isRoot, out string errorMsg)
        {
            if (isRoot)
            {
                if (Id != settings["rootId"].Value)
                {
                    errorMsg = "Id不存在";
                    return false;
                }
                else if (password != settings["rootPwd"].Value)
                {
                    errorMsg = "密码错误";
                    return false;
                }
            }
            else
            {
                password = Md5Hash(password);
                Admin admin = AdminBLL.GetAdminById(Id);
                if (admin == null)
                {
                    errorMsg = "Id不存在";
                    return false;
                }
                else if (admin.ErrorNum == 3)
                {
                    errorMsg = "错误 3 次已锁定";
                    return false;
                }
                else if (admin.Password != password)
                {                    
                    if (admin.ErrorNum == 2)
                        errorMsg = "因错误 3 次锁定";
                    else                    
                        errorMsg = "密码错误";                                            
                    admin.ErrorNum++;
                    BaseBLL.SaveALL();
                    return false;
                }
            }
            errorMsg = "";
            return true;
        }


        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
