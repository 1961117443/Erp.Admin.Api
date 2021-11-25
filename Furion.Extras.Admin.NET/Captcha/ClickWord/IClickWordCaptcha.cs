﻿namespace Furion.Extras.Admin.NET
{
    public interface IClickWordCaptcha
    {
        dynamic CheckCode(ClickWordCaptchaInput input);

        ClickWordCaptchaResult CreateCaptchaImage(string code, int width, int height);

        string RandomCode(int number);
    }
}