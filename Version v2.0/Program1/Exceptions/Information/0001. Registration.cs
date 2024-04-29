using System;

namespace Program1.Exceptions.Information
{
    /// <summary> Класс-сообщение «Успешная регистрация» </summary>
    public class Registration : Exception
    {
        public Registration() : base($"Успешная регистрация")
            => Data.Add("Say", "Результат");
    }
}