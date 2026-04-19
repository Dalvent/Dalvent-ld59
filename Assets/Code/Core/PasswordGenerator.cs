namespace Code.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PasswordGenerator
    {
        private readonly Random _random;

        public PasswordGenerator()
        {
            _random = new Random();
        }

        public PasswordGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public string GenerateUnique(int length)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Password length must be greater than 0.");

            if (length > letters.Length)
                throw new ArgumentOutOfRangeException(nameof(length), "Unique password length cannot be greater than 26.");

            List<char> pool = letters.ToList();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                int index = _random.Next(pool.Count);
                result[i] = pool[index];
                pool.RemoveAt(index);
            }

            return new string(result);
        }
    }
}