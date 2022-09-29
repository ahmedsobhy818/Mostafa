using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Misc
{
    public class MyPasswordHasher
    {
        public MyPasswordHasher()
        {
            //string hashstr = "";
            //string saltstr = "";
            //string hashstr;
            //string saltstr;
            //string pass0 = "abc";
            //CreatePasswordHash(pass0, out hashstr , out saltstr);

            //bool b = VerifyPassword(pass0, hashstr, saltstr);


        }
        public void CreatePasswordHash(string password , out string passwordHash , out string passwordSalt)
        {
            byte[] salt = null;
            byte[] hash = null;
            
            using (var hmac = new System.Security.Cryptography.HMACSHA512())// i used this algorithm for hashing
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//encode the plain text 


                passwordSalt = System.Convert.ToBase64String(salt);//convert created salt to string
                passwordHash = System.Convert.ToBase64String(hash);//convert created hash to string
                
                
            }
        }

        public bool VerifyPassword(string password , string Hash , string Salt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512(Convert.FromBase64String(Salt))) //use the salt string in DB to create the hash encoder
            {
                byte[] NewHashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//encode the plain text 
                byte[] HashBytes = Convert.FromBase64String(Hash);//decode the string Hash saved on the DB
                //if password is correct then the previous 2 byte[] s are identical

                for (int i = 0; i < NewHashBytes.Length; i++)
                {
                    if (NewHashBytes[i] != HashBytes[i])
                        return false;
                }
                return true;

            }
                
        }
    }
}
