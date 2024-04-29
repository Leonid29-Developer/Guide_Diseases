using System;

namespace Program1.Exceptions.Errors
{
    /// <summary> Error #0003 <br/> 
    /// Класс-исключение «Не все поля были заполнены» </summary>
    public class FieldsNoFiled : Exception
    {
        public FieldsNoFiled() : base($"Не все поля были заполнены")
            => Data.Add("Kod", "Ошибка #0003");
    }
}