﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Security
{
    public static class PasswordUtil
    {
        public static string RandomPassword(int pStringLen)
        {
            string guidPassword = Guid.NewGuid().ToString().Replace("-", "").Replace("{", "").Replace("}", "");

            Random random;
            int randomNumber;
            var newPassword = guidPassword.Substring(0, pStringLen);
            var specialCharacteres = "!#$%@";

            if (!newPassword.Any(c => char.IsDigit(c)))
            {
                random = new Random();
                randomNumber = random.Next(0, 9);

                newPassword = randomNumber.ToString() + newPassword.Substring(1, 7);
            }

            if (!newPassword.Any(c => char.IsUpper(c)))
            {
                random = new Random();
                randomNumber = random.Next(0, 26);
                newPassword = newPassword.Substring(0, 1) + ((char)('a' + randomNumber)).ToString().ToUpper() + newPassword.Substring(2, 6);
            }

            if (!newPassword.Any(c => char.IsLower(c)))
            {
                random = new Random();
                randomNumber = random.Next(0, 26);
                newPassword = newPassword.Substring(0, 2) + (char)('a' + randomNumber) + newPassword.Substring(3, 5);
            }

            random = new Random();
            randomNumber = random.Next(0, 4);
            newPassword = newPassword.Substring(0, 3) + specialCharacteres.Substring(randomNumber, 1) + newPassword.Substring(4, 4);
            return newPassword;
        }

        public static string CriptografarSenha(this string senha)
        {
            var bytes = Encoding.UTF8.GetBytes(senha);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}