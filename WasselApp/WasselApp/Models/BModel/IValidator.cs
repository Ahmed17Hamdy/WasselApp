﻿namespace WasselApp.Models.BModel
{
    public interface IValidator
    {
        string Message { get; set; }
        bool Check(string value);
    }
}
