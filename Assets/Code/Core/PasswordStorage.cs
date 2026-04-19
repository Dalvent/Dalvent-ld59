using System;
using System.Linq;
using System.Text;
using Code.Core;
using UnityEngine;

namespace Code
{
    public class PasswordStorage
    {
        public const int PasswordLength = 5;
        public const char NoneSymbol = '?';

        private readonly PasswordGenerator _generator;
        private readonly string _password;
        private readonly char[] _userTakenPassword;

        public PasswordStorage(PasswordGenerator generator)
        {
            _generator = generator;
            _password = _generator.GenerateUnique(PasswordLength);

            _userTakenPassword = new char[PasswordLength];
            for (int i = 0; i < PasswordLength; i++)
                _userTakenPassword[i] = NoneSymbol;

            Reveal(Math.Min(_userTakenPassword.Length - 1, 2));
        }

        public char RevealFirst()
        {
            var index = Array.IndexOf(_userTakenPassword, NoneSymbol);
            _userTakenPassword[index] = _password[index];
            return _password[index];
        }

        
        public char Reveal(int index)
        {
            ValidateIndex(index);

            _userTakenPassword[index] = _password[index];
            return _password[index];
        }

        public bool IsRevealed(int index)
        {
            ValidateIndex(index);
            return _userTakenPassword[index] != NoneSymbol;
        }

        public char[] GetMasked()
        {
            return _userTakenPassword;
        }

        public string GetFullPassword()
        {
            return _password;
        }

        public bool IsComplete()
        {
            for (int i = 0; i < PasswordLength; i++)
            {
                if (_userTakenPassword[i] == NoneSymbol)
                    return false;
            }

            return true;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= PasswordLength)
                throw new ArgumentOutOfRangeException(nameof(index));
        }

        public int ShouldRevealCount()
        {
            return _userTakenPassword.Count(p => p == NoneSymbol);
        }
    }
}