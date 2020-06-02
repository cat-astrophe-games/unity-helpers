using UnityEngine;
using System.Collections;

namespace CatAstropheGames
{
    public class Crypto
    {
        public static string Sha512(string s)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
            System.Security.Cryptography.SHA512 sha = System.Security.Cryptography.SHA512.Create();
            byte[] hash = sha.ComputeHash(bytes);
            string result = System.Text.Encoding.ASCII.GetString(hash);
            return result;
        }
    }

}